// FFXIVAPP.Plugin.Widgets
// Settings.cs
// 
// © 2013 ZAM Network LLC

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using FFXIVAPP.Common.Helpers;
using FFXIVAPP.Common.Models;
using FFXIVAPP.Common.Utilities;
using NLog;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;
using FontFamily = System.Drawing.FontFamily;

namespace FFXIVAPP.Plugin.Widgets.Properties
{
    internal class Settings : ApplicationSettingsBase, INotifyPropertyChanged
    {
        private static Settings _default;

        public static Settings Default
        {
            get { return _default ?? (_default = ((Settings) (Synchronized(new Settings())))); }
        }

        public override void Save()
        {
            XmlHelper.DeleteXmlNode(Constants.XSettings, "Setting");
            if (Constants.Settings.Count == 0)
            {
            }
            DefaultSettings();
            foreach (var item in Constants.Settings)
            {
                try
                {
                    var xKey = item;
                    var xValue = Default[xKey].ToString();
                    var keyPairList = new List<XValuePair>
                    {
                        new XValuePair
                        {
                            Key = "Value",
                            Value = xValue
                        }
                    };
                    XmlHelper.SaveXmlNode(Constants.XSettings, "Settings", "Setting", xKey, keyPairList);
                }
                catch (Exception ex)
                {
                    Logging.Log(LogManager.GetCurrentClassLogger(), "", ex);
                }
            }
            Constants.XSettings.Save(Constants.BaseDirectory + "Settings.xml");
        }

        private void DefaultSettings()
        {
            Constants.Settings.Clear();
            Constants.Settings.Add("DPSWidgetWidth");
            Constants.Settings.Add("DPSWidgetHeight");
            Constants.Settings.Add("HPSWidgetWidth");
            Constants.Settings.Add("HPSWidgetHeight");
            Constants.Settings.Add("DTPSWidgetWidth");
            Constants.Settings.Add("DTPSWidgetHeight");
            Constants.Settings.Add("CurrentTargetWidgetWidth");
            Constants.Settings.Add("CurrentTargetWidgetHeight");
            Constants.Settings.Add("FocusTargetWidgetWidth");
            Constants.Settings.Add("FocusTargetWidgetHeight");
            Constants.Settings.Add("EnmityWidgetWidth");
            Constants.Settings.Add("EnmityWidgetHeight");
            Constants.Settings.Add("DPSWidgetSortDirection");
            Constants.Settings.Add("DPSWidgetSortProperty");
            Constants.Settings.Add("HPSWidgetSortDirection");
            Constants.Settings.Add("HPSWidgetSortProperty");
            Constants.Settings.Add("DTPSWidgetSortDirection");
            Constants.Settings.Add("DTPSWidgetSortProperty");
            Constants.Settings.Add("DPSWidgetUIScale");
            Constants.Settings.Add("HPSWidgetUIScale");
            Constants.Settings.Add("DTPSWidgetUIScale");
            Constants.Settings.Add("CurrentTargetWidgetUIScale");
            Constants.Settings.Add("FocusTargetWidgetUIScale");
            Constants.Settings.Add("EnmityWidgetUIScale");
            Constants.Settings.Add("WidgetOpenCloseModifiesShowOnLoad");
            Constants.Settings.Add("WidgetClickThroughEnabled");
            Constants.Settings.Add("WidgetOpacity");
            Constants.Settings.Add("ShowDPSWidgetOnLoad");
            Constants.Settings.Add("DPSWidgetTop");
            Constants.Settings.Add("DPSWidgetLeft");
            Constants.Settings.Add("DPSVisibility");
            Constants.Settings.Add("ShowDTPSWidgetOnLoad");
            Constants.Settings.Add("DTPSWidgetTop");
            Constants.Settings.Add("DTPSWidgetLeft");
            Constants.Settings.Add("DTPSVisibility");
            Constants.Settings.Add("ShowHPSWidgetOnLoad");
            Constants.Settings.Add("HPSWidgetTop");
            Constants.Settings.Add("HPSWidgetLeft");
            Constants.Settings.Add("HPSVisibility");
            Constants.Settings.Add("ShowEnmityWidgetOnLoad");
            Constants.Settings.Add("EnmityWidgetTop");
            Constants.Settings.Add("EnmityWidgetLeft");
            Constants.Settings.Add("ShowEnmityWidgetCurrentTargetInfo");
            Constants.Settings.Add("ShowFocusTargetWidgetOnLoad");
            Constants.Settings.Add("FocusTargetWidgetTop");
            Constants.Settings.Add("FocusTargetWidgetLeft");
            Constants.Settings.Add("ShowCurrentTargetWidgetOnLoad");
            Constants.Settings.Add("CurrentTargetWidgetTop");
            Constants.Settings.Add("CurrentTargetWidgetLeft");
        }

        public new void Reset()
        {
            DefaultSettings();
            foreach (var key in Constants.Settings)
            {
                var settingsProperty = Default.Properties[key];
                if (settingsProperty == null)
                {
                    continue;
                }
                var value = settingsProperty.DefaultValue.ToString();
                SetValue(key, value);
            }
        }

        public static void SetValue(string key, string value)
        {
            try
            {
                var type = Default[key].GetType()
                                       .Name;
                switch (type)
                {
                    case "Boolean":
                        Default[key] = Convert.ToBoolean(value);
                        break;
                    case "Color":
                        var cc = new ColorConverter();
                        var color = cc.ConvertFrom(value);
                        Default[key] = color ?? Colors.Black;
                        break;
                    case "Double":
                        Default[key] = Convert.ToDouble(value);
                        break;
                    case "Font":
                        var fc = new FontConverter();
                        var font = fc.ConvertFromString(value);
                        Default[key] = font ?? new Font(new FontFamily("Microsoft Sans Serif"), 12);
                        break;
                    case "Int32":
                        Default[key] = Convert.ToInt32(value);
                        break;
                    default:
                        Default[key] = value;
                        break;
                }
            }
            catch (SettingsPropertyNotFoundException ex)
            {
                Logging.Log(LogManager.GetCurrentClassLogger(), "", ex);
            }
            catch (SettingsPropertyWrongTypeException ex)
            {
                Logging.Log(LogManager.GetCurrentClassLogger(), "", ex);
            }
        }

        #region Property Bindings (Settings)

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("#FF000000")]
        public Color ChatBackgroundColor
        {
            get { return ((Color) (this["ChatBackgroundColor"])); }
            set
            {
                this["ChatBackgroundColor"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("#FF800080")]
        public Color TimeStampColor
        {
            get { return ((Color) (this["TimeStampColor"])); }
            set
            {
                this["TimeStampColor"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("Microsoft Sans Serif, 12pt")]
        public Font ChatFont
        {
            get { return ((Font) (this["ChatFont"])); }
            set
            {
                this["ChatFont"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("100")]
        public Double Zoom
        {
            get { return ((Double) (this["Zoom"])); }
            set
            {
                this["Zoom"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("250")]
        public int DPSWidgetWidth
        {
            get { return ((int) (this["DPSWidgetWidth"])); }
            set
            {
                this["DPSWidgetWidth"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("450")]
        public int DPSWidgetHeight
        {
            get { return ((int) (this["DPSWidgetHeight"])); }
            set
            {
                this["DPSWidgetHeight"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("250")]
        public int HPSWidgetWidth
        {
            get { return ((int) (this["HPSWidgetWidth"])); }
            set
            {
                this["HPSWidgetWidth"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("450")]
        public int HPSWidgetHeight
        {
            get { return ((int) (this["HPSWidgetHeight"])); }
            set
            {
                this["HPSWidgetHeight"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("250")]
        public int DTPSWidgetWidth
        {
            get { return ((int) (this["DTPSWidgetWidth"])); }
            set
            {
                this["DTPSWidgetWidth"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("450")]
        public int DTPSWidgetHeight
        {
            get { return ((int) (this["DTPSWidgetHeight"])); }
            set
            {
                this["DTPSWidgetHeight"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("250")]
        public int CurrentTargetWidgetWidth
        {
            get { return ((int) (this["CurrentTargetWidgetWidth"])); }
            set
            {
                this["CurrentTargetWidgetWidth"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("450")]
        public int CurrentTargetWidgetHeight
        {
            get { return ((int) (this["CurrentTargetWidgetHeight"])); }
            set
            {
                this["CurrentTargetWidgetHeight"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("250")]
        public int FocusTargetWidgetWidth
        {
            get { return ((int) (this["FocusTargetWidgetWidth"])); }
            set
            {
                this["FocusTargetWidgetWidth"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("450")]
        public int FocusTargetWidgetHeight
        {
            get { return ((int) (this["FocusTargetWidgetHeight"])); }
            set
            {
                this["FocusTargetWidgetHeight"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("250")]
        public int EnmityWidgetWidth
        {
            get { return ((int) (this["EnmityWidgetWidth"])); }
            set
            {
                this["EnmityWidgetWidth"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("450")]
        public int EnmityWidgetHeight
        {
            get { return ((int) (this["EnmityWidgetHeight"])); }
            set
            {
                this["EnmityWidgetHeight"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("Descending")]
        public string DPSWidgetSortDirection
        {
            get { return ((string) (this["DPSWidgetSortDirection"])); }
            set
            {
                this["DPSWidgetSortDirection"] = value;
                RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>Ascending</string>
  <string>Descending</string>
</ArrayOfString>")]
        public StringCollection DPSWidgetSortDirectionList
        {
            get { return ((StringCollection) (this["DPSWidgetSortDirectionList"])); }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("DPS")]
        public string DPSWidgetSortProperty
        {
            get { return ((string) (this["DPSWidgetSortProperty"])); }
            set
            {
                this["DPSWidgetSortProperty"] = value;
                RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>Name</string>
  <string>Job</string>
  <string>DPS</string>
  <string>TotalOverallDamage</string>
  <string>PercentOfOverallDamage</string>
</ArrayOfString>")]
        public StringCollection DPSWidgetSortPropertyList
        {
            get { return ((StringCollection) (this["DPSWidgetSortPropertyList"])); }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("Descending")]
        public string HPSWidgetSortDirection
        {
            get { return ((string) (this["HPSWidgetSortDirection"])); }
            set
            {
                this["HPSWidgetSortDirection"] = value;
                RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>Ascending</string>
  <string>Descending</string>
</ArrayOfString>")]
        public StringCollection HPSWidgetSortDirectionList
        {
            get { return ((StringCollection) (this["HPSWidgetSortDirectionList"])); }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("HPS")]
        public string HPSWidgetSortProperty
        {
            get { return ((string) (this["HPSWidgetSortProperty"])); }
            set
            {
                this["HPSWidgetSortProperty"] = value;
                RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>Name</string>
  <string>Job</string>
  <string>HPS</string>
  <string>TotalOverallHealing</string>
  <string>PercentOfOverallHealing</string>
</ArrayOfString>")]
        public StringCollection HPSWidgetSortPropertyList
        {
            get { return ((StringCollection) (this["HPSWidgetSortPropertyList"])); }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("Descending")]
        public string DTPSWidgetSortDirection
        {
            get { return ((string) (this["DTPSWidgetSortDirection"])); }
            set
            {
                this["DTPSWidgetSortDirection"] = value;
                RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>Ascending</string>
  <string>Descending</string>
</ArrayOfString>")]
        public StringCollection DTPSWidgetSortDirectionList
        {
            get { return ((StringCollection) (this["DTPSWidgetSortDirectionList"])); }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("DTPS")]
        public string DTPSWidgetSortProperty
        {
            get { return ((string) (this["DTPSWidgetSortProperty"])); }
            set
            {
                this["DTPSWidgetSortProperty"] = value;
                RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>Name</string>
  <string>Job</string>
  <string>DTPS</string>
  <string>TotalOverallDamageTaken</string>
  <string>PercentOfOverallDamageTaken</string>
</ArrayOfString>")]
        public StringCollection DTPSWidgetSortPropertyList
        {
            get { return ((StringCollection) (this["DTPSWidgetSortPropertyList"])); }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("1.0")]
        public string DPSWidgetUIScale
        {
            get { return ((string) (this["DPSWidgetUIScale"])); }
            set
            {
                this["DPSWidgetUIScale"] = value;
                RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>0.8</string>
  <string>0.9</string>
  <string>1.0</string>
  <string>1.1</string>
  <string>1.2</string>
  <string>1.3</string>
  <string>1.4</string>
  <string>1.5</string>
</ArrayOfString>")]
        public StringCollection DPSWidgetUIScaleList
        {
            get { return ((StringCollection) (this["DPSWidgetUIScaleList"])); }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("1.0")]
        public string DTPSWidgetUIScale
        {
            get { return ((string) (this["DTPSWidgetUIScale"])); }
            set
            {
                this["DTPSWidgetUIScale"] = value;
                RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>0.8</string>
  <string>0.9</string>
  <string>1.0</string>
  <string>1.1</string>
  <string>1.2</string>
  <string>1.3</string>
  <string>1.4</string>
  <string>1.5</string>
</ArrayOfString>")]
        public StringCollection DTPSWidgetUIScaleList
        {
            get { return ((StringCollection) (this["DTPSWidgetUIScaleList"])); }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("1.0")]
        public string HPSWidgetUIScale
        {
            get { return ((string) (this["HPSWidgetUIScale"])); }
            set
            {
                this["HPSWidgetUIScale"] = value;
                RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>0.8</string>
  <string>0.9</string>
  <string>1.0</string>
  <string>1.1</string>
  <string>1.2</string>
  <string>1.3</string>
  <string>1.4</string>
  <string>1.5</string>
</ArrayOfString>")]
        public StringCollection HPSWidgetUIScaleList
        {
            get { return ((StringCollection) (this["HPSWidgetUIScaleList"])); }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("1.0")]
        public string CurrentTargetWidgetUIScale
        {
            get { return ((string) (this["CurrentTargetWidgetUIScale"])); }
            set
            {
                this["CurrentTargetWidgetUIScale"] = value;
                RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>0.8</string>
  <string>0.9</string>
  <string>1.0</string>
  <string>1.1</string>
  <string>1.2</string>
  <string>1.3</string>
  <string>1.4</string>
  <string>1.5</string>
</ArrayOfString>")]
        public StringCollection CurrentTargetWidgetUIScaleList
        {
            get { return ((StringCollection) (this["CurrentTargetWidgetUIScaleList"])); }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("1.0")]
        public string FocusTargetWidgetUIScale
        {
            get { return ((string) (this["FocusTargetWidgetUIScale"])); }
            set
            {
                this["FocusTargetWidgetUIScale"] = value;
                RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>0.8</string>
  <string>0.9</string>
  <string>1.0</string>
  <string>1.1</string>
  <string>1.2</string>
  <string>1.3</string>
  <string>1.4</string>
  <string>1.5</string>
</ArrayOfString>")]
        public StringCollection FocusTargetWidgetUIScaleList
        {
            get { return ((StringCollection) (this["FocusTargetWidgetUIScaleList"])); }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("1.0")]
        public string EnmityWidgetUIScale
        {
            get { return ((string) (this["EnmityWidgetUIScale"])); }
            set
            {
                this["EnmityWidgetUIScale"] = value;
                RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>0.8</string>
  <string>0.9</string>
  <string>1.0</string>
  <string>1.1</string>
  <string>1.2</string>
  <string>1.3</string>
  <string>1.4</string>
  <string>1.5</string>
</ArrayOfString>")]
        public StringCollection EnmityWidgetUIScaleList
        {
            get { return ((StringCollection) (this["EnmityWidgetUIScaleList"])); }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool WidgetOpenCloseModifiesShowOnLoad
        {
            get { return ((bool) (this["WidgetOpenCloseModifiesShowOnLoad"])); }
            set
            {
                this["WidgetOpenCloseModifiesShowOnLoad"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("0.7")]
        public string WidgetOpacity
        {
            get { return ((string) (this["WidgetOpacity"])); }
            set
            {
                this["WidgetOpacity"] = value;
                RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>0.5</string>
  <string>0.6</string>
  <string>0.7</string>
  <string>0.8</string>
  <string>0.9</string>
  <string>1.0</string>
</ArrayOfString>")]
        public StringCollection WidgetOpacityList
        {
            get { return ((StringCollection) (this["WidgetOpacityList"])); }
            set
            {
                this["WidgetOpacityList"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool WidgetClickThroughEnabled
        {
            get { return ((bool) (this["WidgetClickThroughEnabled"])); }
            set
            {
                this["WidgetClickThroughEnabled"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool ShowDPSWidgetOnLoad
        {
            get { return ((bool) (this["ShowDPSWidgetOnLoad"])); }
            set
            {
                this["ShowDPSWidgetOnLoad"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("100")]
        public int DPSWidgetTop
        {
            get { return ((int) (this["DPSWidgetTop"])); }
            set
            {
                this["DPSWidgetTop"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("100")]
        public int DPSWidgetLeft
        {
            get { return ((int) (this["DPSWidgetLeft"])); }
            set
            {
                this["DPSWidgetLeft"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("0")]
        public string DPSVisibility
        {
            get { return ((string) (this["DPSVisibility"])); }
            set
            {
                this["DPSVisibility"] = value;
                RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>0</string>
  <string>50</string>
  <string>100</string>
  <string>150</string>
  <string>200</string>
  <string>250</string>
  <string>300</string>
</ArrayOfString>")]
        public StringCollection DPSVisibilityList
        {
            get { return ((StringCollection) (this["DPSVisibilityList"])); }
            set
            {
                this["DPSVisibilityList"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool ShowDTPSWidgetOnLoad
        {
            get { return ((bool) (this["ShowDTPSWidgetOnLoad"])); }
            set
            {
                this["ShowDTPSWidgetOnLoad"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("200")]
        public int DTPSWidgetTop
        {
            get { return ((int) (this["DTPSWidgetTop"])); }
            set
            {
                this["DTPSWidgetTop"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("100")]
        public int DTPSWidgetLeft
        {
            get { return ((int) (this["DTPSWidgetLeft"])); }
            set
            {
                this["DTPSWidgetLeft"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("0")]
        public string DTPSVisibility
        {
            get { return ((string) (this["DTPSVisibility"])); }
            set
            {
                this["DTPSVisibility"] = value;
                RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>0</string>
  <string>50</string>
  <string>100</string>
  <string>150</string>
  <string>200</string>
  <string>250</string>
  <string>300</string>
</ArrayOfString>")]
        public StringCollection DTPSVisibilityList
        {
            get { return ((StringCollection) (this["DTPSVisibilityList"])); }
            set
            {
                this["DTPSVisibilityList"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool ShowHPSWidgetOnLoad
        {
            get { return ((bool) (this["ShowHPSWidgetOnLoad"])); }
            set
            {
                this["ShowHPSWidgetOnLoad"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("300")]
        public int HPSWidgetTop
        {
            get { return ((int) (this["HPSWidgetTop"])); }
            set
            {
                this["HPSWidgetTop"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("100")]
        public int HPSWidgetLeft
        {
            get { return ((int) (this["HPSWidgetLeft"])); }
            set
            {
                this["HPSWidgetLeft"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("0")]
        public string HPSVisibility
        {
            get { return ((string) (this["HPSVisibility"])); }
            set
            {
                this["HPSVisibility"] = value;
                RaisePropertyChanged();
            }
        }

        [ApplicationScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>0</string>
  <string>50</string>
  <string>100</string>
  <string>150</string>
  <string>200</string>
  <string>250</string>
  <string>300</string>
</ArrayOfString>")]
        public StringCollection HPSVisibilityList
        {
            get { return ((StringCollection) (this["HPSVisibilityList"])); }
            set
            {
                this["HPSVisibilityList"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool ShowEnmityWidgetOnLoad
        {
            get { return ((bool) (this["ShowEnmityWidgetOnLoad"])); }
            set
            {
                this["ShowEnmityWidgetOnLoad"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("100")]
        public int EnmityWidgetTop
        {
            get { return ((int) (this["EnmityWidgetTop"])); }
            set
            {
                this["EnmityWidgetTop"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("400")]
        public int EnmityWidgetLeft
        {
            get { return ((int) (this["EnmityWidgetLeft"])); }
            set
            {
                this["EnmityWidgetLeft"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool ShowEnmityWidgetCurrentTargetInfo
        {
            get { return ((bool) (this["ShowEnmityWidgetCurrentTargetInfo"])); }
            set
            {
                this["ShowEnmityWidgetCurrentTargetInfo"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool ShowFocusTargetWidgetOnLoad
        {
            get { return ((bool) (this["ShowFocusTargetWidgetOnLoad"])); }
            set
            {
                this["ShowFocusTargetWidgetOnLoad"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("200")]
        public int FocusTargetWidgetTop
        {
            get { return ((int) (this["FocusTargetWidgetTop"])); }
            set
            {
                this["FocusTargetWidgetTop"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("400")]
        public int FocusTargetWidgetLeft
        {
            get { return ((int) (this["FocusTargetWidgetLeft"])); }
            set
            {
                this["FocusTargetWidgetLeft"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool ShowCurrentTargetWidgetOnLoad
        {
            get { return ((bool) (this["ShowCurrentTargetWidgetOnLoad"])); }
            set
            {
                this["ShowCurrentTargetWidgetOnLoad"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("300")]
        public int CurrentTargetWidgetTop
        {
            get { return ((int) (this["CurrentTargetWidgetTop"])); }
            set
            {
                this["CurrentTargetWidgetTop"] = value;
                RaisePropertyChanged();
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("400")]
        public int CurrentTargetWidgetLeft
        {
            get { return ((int) (this["CurrentTargetWidgetLeft"])); }
            set
            {
                this["CurrentTargetWidgetLeft"] = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Implementation of INotifyPropertyChanged

        public new event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }

        #endregion
    }
}
