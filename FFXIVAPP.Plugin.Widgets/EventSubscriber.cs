// FFXIVAPP.Plugin.Widgets
// EventSubscriber.cs
// 
// © 2013 ZAM Network LLC

using System;
using FFXIVAPP.IPluginInterface.Events;
using FFXIVAPP.Plugin.Widgets.Helpers;
using FFXIVAPP.Plugin.Widgets.Utilities;
using FFXIVAPP.Plugin.Widgets.Windows;

namespace FFXIVAPP.Plugin.Widgets
{
    public static class EventSubscriber
    {
        public static void Subscribe()
        {
            Plugin.PHost.NewConstantsEntity += OnNewConstantsEntity;
            Plugin.PHost.NewChatLogEntry += OnNewChatLogEntry;
            Plugin.PHost.NewMonsterEntries += OnNewMonsterEntries;
            Plugin.PHost.NewNPCEntries += OnNewNPCEntries;
            Plugin.PHost.NewPCEntries += OnNewPCEntries;
            Plugin.PHost.NewPlayerEntity += OnNewPlayerEntity;
            Plugin.PHost.NewTargetEntity += OnNewTargetEntity;
            Plugin.PHost.NewParseEntity += OnNewParseEntity;
        }

        public static void UnSubscribe()
        {
            Plugin.PHost.NewConstantsEntity -= OnNewConstantsEntity;
            Plugin.PHost.NewChatLogEntry -= OnNewChatLogEntry;
            Plugin.PHost.NewMonsterEntries -= OnNewMonsterEntries;
            Plugin.PHost.NewNPCEntries -= OnNewNPCEntries;
            Plugin.PHost.NewPCEntries -= OnNewPCEntries;
            Plugin.PHost.NewPlayerEntity -= OnNewPlayerEntity;
            Plugin.PHost.NewTargetEntity -= OnNewTargetEntity;
            Plugin.PHost.NewParseEntity -= OnNewParseEntity;
        }

        #region Subscriptions

        private static void OnNewConstantsEntity(object sender, ConstantsEntityEvent constantsEntityEvent)
        {
            // delegate event from constants, not required to subsribe, but recommended as it gives you app settings
            if (sender == null)
            {
                return;
            }
            var constantsEntity = constantsEntityEvent.ConstantsEntity;
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

        private static void OnNewChatLogEntry(object sender, ChatLogEntryEvent chatLogEntryEvent)
        {
            // delegate event from chat log, not required to subsribe
            // this updates 100 times a second and only sends a line when it gets a new one
            if (sender == null)
            {
                return;
            }
            var chatLogEntry = chatLogEntryEvent.ChatLogEntry;
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
            }
        }

        private static void OnNewMonsterEntries(object sender, ActorEntitiesEvent actorEntitiesEvent)
        {
            // delegate event from monster entities from ram, not required to subsribe
            // this updates 10x a second and only sends data if the items are found in ram
            // currently there no change/new/removed event handling (looking into it)
            if (sender == null)
            {
                return;
            }
            var monsterEntities = actorEntitiesEvent.ActorEntities;
        }

        private static void OnNewNPCEntries(object sender, ActorEntitiesEvent actorEntitiesEvent)
        {
            // delegate event from npc entities from ram, not required to subsribe
            // this list includes anything that is not a player or monster
            // this updates 10x a second and only sends data if the items are found in ram
            // currently there no change/new/removed event handling (looking into it)
            if (sender == null)
            {
                return;
            }
            var npcEntities = actorEntitiesEvent.ActorEntities;
        }

        private static void OnNewPCEntries(object sender, ActorEntitiesEvent actorEntitiesEvent)
        {
            // delegate event from player entities from ram, not required to subsribe
            // this updates 10x a second and only sends data if the items are found in ram
            // currently there no change/new/removed event handling (looking into it)
            if (sender == null)
            {
                return;
            }
            var pcEntities = actorEntitiesEvent.ActorEntities;
        }

        private static void OnNewPlayerEntity(object sender, PlayerEntityEvent playerEntityEvent)
        {
            // delegate event from player info from ram, not required to subsribe
            // this is for YOU and includes all your stats and your agro list with hate values as %
            // this updates 10x a second and only sends data when the newly read data is differen than what was previously sent
            if (sender == null)
            {
                return;
            }
            var playerEntity = playerEntityEvent.PlayerEntity;
        }

        private static void OnNewTargetEntity(object sender, TargetEntityEvent targetEntityEvent)
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
            var targetEntity = targetEntityEvent.TargetEntity;
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

        private static void OnNewParseEntity(object sender, ParseEntityEvent parseEntityEvent)
        {
            // delegate event from data work; which right now has basic parsing stats for widgets.
            // includes global total stats for damage, healing, damage taken
            // include player list with name, hps, dps, dtps, total stats like the global and percent of each total stat
            if (sender == null)
            {
                return;
            }
            var parseEntity = parseEntityEvent.ParseEntity;
            EntityHelper.Parse.CleanAndCopy(parseEntity, EntityHelper.Parse.ParseType.DPS);
            EntityHelper.Parse.CleanAndCopy(parseEntity, EntityHelper.Parse.ParseType.DTPS);
            EntityHelper.Parse.CleanAndCopy(parseEntity, EntityHelper.Parse.ParseType.HPS);
        }

        #endregion
    }
}
