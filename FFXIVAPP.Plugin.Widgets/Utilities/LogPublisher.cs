// FFXIVAPP.Plugin.Widgets
// LogPublisher.cs
// 
// © 2013 ZAM Network LLC

using System;
using FFXIVAPP.Common.Core.Memory;
using FFXIVAPP.Common.Utilities;
using NLog;

namespace FFXIVAPP.Plugin.Widgets.Utilities
{
    public static class LogPublisher
    {
        public static void Process(ChatLogEntry chatLogEntry)
        {
            try
            {
            }
            catch (Exception ex)
            {
                Logging.Log(LogManager.GetCurrentClassLogger(), "", ex);
            }
        }

        public static void HandleCommands(ChatLogEntry chatLogEntry)
        {
            // process commands
            if (chatLogEntry.Code == "0038")
            {
                var commandsRegEx = CommandBuilder.CommandsRegEx.Match(chatLogEntry.Line.Trim());
                if (commandsRegEx.Success)
                {
                    var widget = commandsRegEx.Groups["widget"].Success ? commandsRegEx.Groups["widget"].Value : "";
                    switch (widget)
                    {
                        case "dps":
                            Widgets.Instance.ShowDPSWidget();
                            break;
                    }
                }
            }
        }
    }
}
