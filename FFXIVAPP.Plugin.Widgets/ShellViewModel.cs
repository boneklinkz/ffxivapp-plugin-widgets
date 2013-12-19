// FFXIVAPP.Plugin.Widgets
// ShellViewModel.cs
// 
// © 2013 ZAM Network LLC

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FFXIVAPP.Plugin.Widgets.Interop;
using FFXIVAPP.Plugin.Widgets.Properties;

namespace FFXIVAPP.Plugin.Widgets
{
    public sealed class ShellViewModel : INotifyPropertyChanged
    {
        #region Property Bindings

        private static ShellViewModel _instance;

        public static ShellViewModel Instance
        {
            get { return _instance ?? (_instance = new ShellViewModel()); }
        }

        #endregion

        #region Declarations

        #endregion

        public ShellViewModel()
        {
            Initializer.LoadSettings();
            Settings.Default.PropertyChanged += DefaultOnPropertyChanged;
        }

        internal static void Loaded(object sender, RoutedEventArgs e)
        {
            ShellView.View.Loaded -= Loaded;
            if (Settings.Default.ShowDPSWidgetOnLoad)
            {
                Widgets.Instance.ShowDPSWidget();
            }
            if (Settings.Default.ShowDTPSWidgetOnLoad)
            {
                Widgets.Instance.ShowDTPSWidget();
            }
            if (Settings.Default.ShowHPSWidgetOnLoad)
            {
                Widgets.Instance.ShowHPSWidget();
            }
            if (Settings.Default.ShowEnmityWidgetOnLoad)
            {
                Widgets.Instance.ShowEnmityWidget();
            }
            if (Settings.Default.ShowFocusTargetWidgetOnLoad)
            {
                Widgets.Instance.ShowFocusTargetWidget();
            }
            if (Settings.Default.ShowCurrentTargetWidgetOnLoad)
            {
                Widgets.Instance.ShowCurrentTargetWidget();
            }
        }

        private static void DefaultOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var propertyName = propertyChangedEventArgs.PropertyName;
            switch (propertyName)
            {
                case "WidgetClickThroughEnabled":
                    WinAPI.ToggleClickThrough(Widgets.Instance.CurrentTargetWidget);
                    WinAPI.ToggleClickThrough(Widgets.Instance.DPSWidget);
                    WinAPI.ToggleClickThrough(Widgets.Instance.DTPSWidget);
                    WinAPI.ToggleClickThrough(Widgets.Instance.EnmityWidget);
                    WinAPI.ToggleClickThrough(Widgets.Instance.FocusTargetWidget);
                    WinAPI.ToggleClickThrough(Widgets.Instance.HPSWidget);
                    break;
            }
        }

        #region Loading Functions

        #endregion

        #region Utility Functions

        #endregion

        #region Command Bindings

        #endregion

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }

        #endregion
    }
}
