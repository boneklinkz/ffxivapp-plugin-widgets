﻿// FFXIVAPP.Plugin.Widgets
// French.cs
// 
// © 2013 ZAM Network LLC

#region Usings

using System.Windows;

#endregion

namespace FFXIVAPP.Plugin.Widgets.Localization
{
    public abstract class French
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
