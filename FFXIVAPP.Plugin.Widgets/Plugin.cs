// FFXIVAPP.Plugin.Widgets
// Plugin.cs
// 
// © 2013 ZAM Network LLC

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Threading;
using FFXIVAPP.Common.Core.Constant;
using FFXIVAPP.Common.Core.Memory;
using FFXIVAPP.Common.Core.Parse;
using FFXIVAPP.Common.Events;
using FFXIVAPP.Common.Helpers;
using FFXIVAPP.Common.Utilities;
using FFXIVAPP.IPluginInterface;
using FFXIVAPP.IPluginInterface.Events;
using FFXIVAPP.Plugin.Widgets.Helpers;
using FFXIVAPP.Plugin.Widgets.Interop;
using FFXIVAPP.Plugin.Widgets.Properties;
using FFXIVAPP.Plugin.Widgets.Utilities;
using FFXIVAPP.Plugin.Widgets.Windows;
using NLog;
using PlayerEntity = FFXIVAPP.Common.Core.Memory.PlayerEntity;

namespace FFXIVAPP.Plugin.Widgets
{
    [Export(typeof (IPlugin))]
    public class Plugin : IPlugin, INotifyPropertyChanged
    {
        #region Property Bindings

        public static IPluginHost PHost { get; private set; }
        public static string PName { get; private set; }

        #endregion

        #region Declarations

        #endregion

        private static bool entered = false;
        private IPluginHost _host;
        private Dictionary<string, string> _locale;
        private string _name;
        private MessageBoxResult _popupResult;
        private WinAPI.WinEventDelegate dele;
        private IntPtr m_hhook;

        private Timer setWindowTimer;

        public IPluginHost Host
        {
            get { return _host; }
            set { PHost = _host = value; }
        }

        public MessageBoxResult PopupResult
        {
            get { return _popupResult; }
            set
            {
                _popupResult = value;
                PluginViewModel.Instance.OnPopupResultChanged(new PopupResultEvent(value));
            }
        }

        public Dictionary<string, string> Locale
        {
            get { return _locale ?? (_locale = new Dictionary<string, string>()); }
            set
            {
                _locale = value;
                Dictionary<string, string> locale = LocaleHelper.Update(Constants.CultureInfo);
                foreach (var resource in locale)
                {
                    try
                    {
                        _locale.Add(resource.Key, resource.Value);
                    }
                    catch (Exception ex)
                    {
                        Logging.Log(LogManager.GetCurrentClassLogger(), "", ex);
                    }
                }
                PluginViewModel.Instance.Locale = _locale;
                RaisePropertyChanged();
            }
        }

        public string FriendlyName { get; set; }

        public string Name
        {
            get { return _name; }
            private set { PName = _name = value; }
        }

        public string Icon { get; private set; }

        public string Description { get; private set; }

        public string Copyright { get; private set; }

        public string Version { get; private set; }

        public string Notice { get; private set; }

        public Exception Trace { get; private set; }

        public void Initialize(IPluginHost pluginHost)
        {
            Host = pluginHost;
            pluginHost.NewConstantsEntity += PluginHostOnNewConstantsEntity;
            pluginHost.NewChatLogEntry += PluginHostOnNewChatLogEntry;
            pluginHost.NewMonsterEntries += PluginHostOnNewMonsterEntries;
            pluginHost.NewNPCEntries += PluginHostOnNewNPCEntries;
            pluginHost.NewPCEntries += PluginHostOnNewPCEntries;
            pluginHost.NewPlayerEntity += PluginHostOnNewPlayerEntity;
            pluginHost.NewTargetEntity += PluginHostOnNewTargetEntity;
            pluginHost.NewParseEntity += PluginHostOnNewParseEntity;
            FriendlyName = "Widgets";
            Name = AssemblyHelper.Name;
            Icon = "Logo.png";
            Description = AssemblyHelper.Description;
            Copyright = AssemblyHelper.Copyright;
            Version = AssemblyHelper.Version.ToString();
            Notice = "";


            try
            {
                dele = BringWidgetsIntoFocus;
                m_hhook = WinAPI.SetWinEventHook(WinAPI.EVENT_SYSTEM_FOREGROUND, WinAPI.EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, dele, 0, 0, WinAPI.WINEVENT_OUTOFCONTEXT);
            }
            catch (Exception e)
            {
                //MessageBox.Show("Initialize exception: " + e.Message);
            }

            setWindowTimer = new Timer(1000);
            setWindowTimer.Elapsed += setWindowTimer_Elapsed;
            setWindowTimer.Enabled = true;
        }

        public void Dispose(bool isUpdating = false)
        {
            /*
             * If the isUpdating is true it means the application will be force closing/killed.
             * You wil have to choose what you want to do in this case.
             * By default the settings class clears the settings object and recreates it; but if killed untimely it will not save.
             * 
             * Suggested use is to not save settings if updating. Other disposing events could happen based on your needs.
             */
            Settings.Default.Save();
        }

        public TabItem CreateTab()
        {
            var content = new ShellView();
            content.Loaded += ShellViewModel.Loaded;
            var tabItem = new TabItem
            {
                Header = Name,
                Content = content
            };
            //do your gui stuff here
            //content gives you access to the base xaml
            return tabItem;
        }

        public UserControl CreateControl()
        {
            var content = new ShellView();
            content.Loaded += ShellViewModel.Loaded;
            //do your gui stuff here
            //content gives you access to the base xaml
            return content;
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }

        #endregion

        private void setWindowTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate { BringWidgetsIntoFocus(); }));
        }

        private void PluginHostOnNewConstantsEntity(object sender, ConstantsEntityEvent constantsEntityEvent)
        {
            // delegate event from constants, not required to subsribe, but recommended as it gives you app settings
            if (sender == null)
            {
                return;
            }
            ConstantsEntity constantsEntity = constantsEntityEvent.ConstantsEntity;
            Constants.AutoTranslate = constantsEntity.AutoTranslate;
            Constants.ChatCodes = constantsEntity.ChatCodes;
            Constants.Colors = constantsEntity.Colors;
            Constants.CultureInfo = constantsEntity.CultureInfo;
            Constants.CharacterName = constantsEntity.CharacterName;
            Constants.ServerName = constantsEntity.ServerName;
            Constants.GameLanguage = constantsEntity.GameLanguage;
            Constants.EnableHelpLabels = constantsEntity.EnableHelpLabels;
            PluginViewModel.Instance.EnableHelpLabels = Constants.EnableHelpLabels;
        }

        private void PluginHostOnNewChatLogEntry(object sender, ChatLogEntryEvent chatLogEntryEvent)
        {
            // delegate event from chat log, not required to subsribe
            // this updates 100 times a second and only sends a line when it gets a new one
            if (sender == null)
            {
                return;
            }
            ChatLogEntry chatLogEntry = chatLogEntryEvent.ChatLogEntry;
            try
            {
                if (chatLogEntry.Line.ToLower()
                                .StartsWith("com:"))
                {
                    LogPublisher.HandleCommands(chatLogEntry);
                }
                LogPublisher.Process(chatLogEntry);
            }
            catch (Exception ex)
            {
                Notice = ex.Message;
            }
        }

        private void PluginHostOnNewMonsterEntries(object sender, ActorEntitiesEvent actorEntitiesEvent)
        {
            // delegate event from monster entities from ram, not required to subsribe
            // this updates 10x a second and only sends data if the items are found in ram
            // currently there no change/new/removed event handling (looking into it)
            if (sender == null)
            {
                return;
            }
            List<ActorEntity> monsterEntities = actorEntitiesEvent.ActorEntities;
        }

        private void PluginHostOnNewNPCEntries(object sender, ActorEntitiesEvent actorEntitiesEvent)
        {
            // delegate event from npc entities from ram, not required to subsribe
            // this list includes anything that is not a player or monster
            // this updates 10x a second and only sends data if the items are found in ram
            // currently there no change/new/removed event handling (looking into it)
            if (sender == null)
            {
                return;
            }
            List<ActorEntity> npcEntities = actorEntitiesEvent.ActorEntities;
        }

        private void PluginHostOnNewPCEntries(object sender, ActorEntitiesEvent actorEntitiesEvent)
        {
            // delegate event from player entities from ram, not required to subsribe
            // this updates 10x a second and only sends data if the items are found in ram
            // currently there no change/new/removed event handling (looking into it)
            if (sender == null)
            {
                return;
            }
            List<ActorEntity> pcEntities = actorEntitiesEvent.ActorEntities;
        }

        private void PluginHostOnNewPlayerEntity(object sender, PlayerEntityEvent playerEntityEvent)
        {
            // delegate event from player info from ram, not required to subsribe
            // this is for YOU and includes all your stats and your agro list with hate values as %
            // this updates 10x a second and only sends data when the newly read data is differen than what was previously sent
            if (sender == null)
            {
                return;
            }
            PlayerEntity playerEntity = playerEntityEvent.PlayerEntity;
        }

        private void PluginHostOnNewTargetEntity(object sender, TargetEntityEvent targetEntityEvent)
        {
            // delegate event from target info from ram, not required to subsribe
            // this includes the full entities for current, previous, mouseover, focus targets (if 0+ are found)
            // it also includes a list of upto 16 targets that currently have hate on the currently targeted monster
            // these hate values are realtime and change based on the action used
            // this updates 10x a second and only sends data when the newly read data is differen than what was previously sent
            if (sender == null)
            {
                return;
            }
            TargetEntity targetEntity = targetEntityEvent.TargetEntity;
            // assign entity
            EnmityWidgetViewModel.Instance.TargetEntity = targetEntity;
            FocusTargetWidgetViewModel.Instance.TargetEntity = targetEntity;
            CurrentTargetWidgetViewModel.Instance.TargetEntity = targetEntity;
            // assign default current/focus target info
            EnmityWidgetViewModel.Instance.CurrentTargetIsValid = false;
            EnmityWidgetViewModel.Instance.CurrentTargetHPPercent = 0;
            FocusTargetWidgetViewModel.Instance.FocusTargetIsValid = false;
            FocusTargetWidgetViewModel.Instance.FocusTargetHPPercent = 0;
            CurrentTargetWidgetViewModel.Instance.CurrentTargetIsValid = false;
            CurrentTargetWidgetViewModel.Instance.CurrentTargetHPPercent = 0;
            // if valid assign actual current target info
            if (targetEntity.CurrentTarget != null && targetEntity.CurrentTarget.IsValid)
            {
                EnmityWidgetViewModel.Instance.CurrentTargetIsValid = true;
                EnmityWidgetViewModel.Instance.CurrentTargetHPPercent = (double) targetEntity.CurrentTarget.HPPercent;
                CurrentTargetWidgetViewModel.Instance.CurrentTargetIsValid = true;
                CurrentTargetWidgetViewModel.Instance.CurrentTargetHPPercent = (double) targetEntity.CurrentTarget.HPPercent;
            }
            // if valid assign actual focus target info
            if (targetEntity.FocusTarget != null && targetEntity.FocusTarget.IsValid)
            {
                FocusTargetWidgetViewModel.Instance.FocusTargetIsValid = true;
                FocusTargetWidgetViewModel.Instance.FocusTargetHPPercent = (double) targetEntity.FocusTarget.HPPercent;
            }
        }

        private void PluginHostOnNewParseEntity(object sender, ParseEntityEvent parseEntityEvent)
        {
            // delegate event from data work; which right now has basic parsing stats for widgets.
            // includes global total stats for damage, healing, damage taken
            // include player list with name, hps, dps, dtps, total stats like the global and percent of each total stat
            if (sender == null)
            {
                return;
            }
            ParseEntity parseEntity = parseEntityEvent.ParseEntity;
            DPSWidgetViewModel.Instance.ParseEntity = EntityHelper.Parse.CleanAndCopy(parseEntity, EntityHelper.Parse.ParseType.DPS);
            DTPSWidgetViewModel.Instance.ParseEntity = EntityHelper.Parse.CleanAndCopy(parseEntity, EntityHelper.Parse.ParseType.DTPS);
            HPSWidgetViewModel.Instance.ParseEntity = EntityHelper.Parse.CleanAndCopy(parseEntity, EntityHelper.Parse.ParseType.HPS);
        }

        private static void BringWidgetsIntoFocus(IntPtr hwineventhook, uint eventtype, IntPtr hwnd, int idobject, int idchild, uint dweventthread, uint dwmseventtime)
        {
            BringWidgetsIntoFocus(true);
        }


        private static void BringWidgetsIntoFocus(bool force = false)
        {
            try
            {

                IntPtr handle = WinAPI.GetForegroundWindow();
                string name = WinAPI.GetActiveWindowTitle()
                             .ToUpper();
                //MessageBox.Show("name" + name);
                bool stayOnTop = WinAPI.GetActiveWindowTitle()
                                  .ToUpper()
                                  .StartsWith("FINAL FANTASY XIV");


                // If any of the widgets are focused, don't try to hide any of them, or it'll prevent us from moving/closing them
                if (handle == new WindowInteropHelper(Widgets.Instance.DPSWidget).Handle)
                {
                    return;
                }
                if (handle == new WindowInteropHelper(Widgets.Instance.EnmityWidget).Handle)
                {
                    return;
                }
                if (handle == new WindowInteropHelper(Widgets.Instance.CurrentTargetWidget).Handle)
                {
                    return;
                }
                if (handle == new WindowInteropHelper(Widgets.Instance.DTPSWidget).Handle)
                {
                    return;
                }
                if (handle == new WindowInteropHelper(Widgets.Instance.FocusTargetWidget).Handle)
                {
                    return;
                }

                if (Settings.Default.ShowDPSWidgetOnLoad)
                {
                    ToggleTopMost(Widgets.Instance.DPSWidget, stayOnTop, force);
                }
                if (Settings.Default.ShowEnmityWidgetOnLoad)
                {
                    ToggleTopMost(Widgets.Instance.EnmityWidget, stayOnTop, force);
                }
                if (Settings.Default.ShowCurrentTargetWidgetOnLoad)
                {
                    ToggleTopMost(Widgets.Instance.CurrentTargetWidget, stayOnTop, force);
                }
                if (Settings.Default.ShowDTPSWidgetOnLoad)
                {
                    ToggleTopMost(Widgets.Instance.DTPSWidget, stayOnTop, force);
                }
                if (Settings.Default.ShowFocusTargetWidgetOnLoad)
                {
                    ToggleTopMost(Widgets.Instance.FocusTargetWidget, stayOnTop, force);
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("Exception: " + e.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="window"></param>
        /// <param name="stayOnTop"></param>
        private static void ToggleTopMost(Window window, bool stayOnTop, bool force)
        {
            if (window.Topmost && stayOnTop && !force)
            {
                return;
            }

            window.Topmost = false;
            if (!stayOnTop)
            {
                if (window.IsVisible)
                {
                    window.Hide();
                }
                return;
            }

            window.Topmost = true;
            if (!window.IsVisible)
            {
                window.Show();
            }
        }
    }
}
