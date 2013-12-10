// FFXIVAPP.Plugin.Widgets
// Japanese.cs
// 
// © 2013 ZAM Network LLC

using System.Windows;

namespace FFXIVAPP.Plugin.Widgets.Localization
{
    public abstract class Japanese
    {
        private static readonly ResourceDictionary Dictionary = new ResourceDictionary();

        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public static ResourceDictionary Context()
        {
            Dictionary.Clear();
            Dictionary.Add("sample_", "*PH*");
            Dictionary.Add("sample_ChatLogTabHeader", "Chat");
            Dictionary.Add("sample_ClearChatLogMessage", "Clear ChatLogFD");
            Dictionary.Add("sample_ClearChatLogToolTip", "Clear Chat");
            return Dictionary;
        }
    }
}
