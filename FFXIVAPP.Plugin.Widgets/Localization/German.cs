// FFXIVAPP.Plugin.Widgets
// German.cs
// 
// © 2013 ZAM Network LLC

using System.Windows;

namespace FFXIVAPP.Plugin.Widgets.Localization
{
    public abstract class German
    {
        private static readonly ResourceDictionary Dictionary = new ResourceDictionary();

        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public static ResourceDictionary Context()
        {
            Dictionary.Clear();
            Dictionary.Add("widgets_PLACEHOLDER", "*PH*");
            Dictionary.Add("widgets_OpenNowButtonText", "Open Now");
            Dictionary.Add("widgets_ResetPositionButtonText", "Reset Settings");
            Dictionary.Add("widgets_DPSTitleBar", "[DPS]");
            Dictionary.Add("widgets_HPSTitleBar", "[HPS]");
            Dictionary.Add("widgets_DTPSTitleBar", "[DTPS]");
            Dictionary.Add("widgets_EnmityTitleBar", "[Enmity]");
            Dictionary.Add("widgets_FocusTitleBar", "[Focus]");
            Dictionary.Add("widgets_CurrentTitleBar", "[Current]");
            Dictionary.Add("widgets_DPSWidgetHeader", "DPS Widget");
            Dictionary.Add("widgets_HPSWidgetHeader", "HPS Widget");
            Dictionary.Add("widgets_DTPSWidgetHeader", "DTPS Widget");
            Dictionary.Add("widgets_EnmityWidgetHeader", "Enmity Widget");
            Dictionary.Add("widgets_FocusWidgetHeader", "Focus Target Widget");
            Dictionary.Add("widgets_CurrentWidgetHeader", "Current Target Widget");
            Dictionary.Add("widgets_EnableClickThroughHeader", "Enabled Click-Through On Widgets");
            Dictionary.Add("widgets_WidgetOpacityHeader", "Widget Opacity");
            return Dictionary;
        }
    }
}
