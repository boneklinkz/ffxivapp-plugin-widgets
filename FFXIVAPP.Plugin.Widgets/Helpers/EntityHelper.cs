// FFXIVAPP.Plugin.Widgets
// EntityHelper.cs
// 
// © 2013 ZAM Network LLC

using System.Collections.Generic;
using System.Linq;
using FFXIVAPP.Common.Core.Parse;
using FFXIVAPP.Plugin.Widgets.Properties;
using FFXIVAPP.Plugin.Widgets.Windows;

namespace FFXIVAPP.Plugin.Widgets.Helpers
{
    public static class EntityHelper
    {
        public static class Parse
        {
            public enum ParseType
            {
                DPS,
                DTPS,
                HPS
            }

            public static void CleanAndCopy(ParseEntity source, ParseType parseType)
            {
                var target = new ParseEntity
                {
                    DPS = source.DPS,
                    DTPS = source.DTPS,
                    HPS = source.HPS,
                    TotalOverallDamage = source.TotalOverallDamage,
                    TotalOverallDamageTaken = source.TotalOverallDamageTaken,
                    TotalOverallHealing = source.TotalOverallHealing
                };
                foreach (var playerEntity in source.Players)
                {
                    switch (parseType)
                    {
                        case ParseType.DPS:
                            decimal dps;
                            decimal.TryParse(Settings.Default.DPSVisibility, out dps);
                            if (playerEntity.DPS <= dps)
                            {
                                continue;
                            }
                            break;
                        case ParseType.DTPS:
                            decimal dtps;
                            decimal.TryParse(Settings.Default.DTPSVisibility, out dtps);
                            if (playerEntity.DTPS <= dtps)
                            {
                                continue;
                            }
                            break;
                        case ParseType.HPS:
                            decimal hps;
                            decimal.TryParse(Settings.Default.HPSVisibility, out hps);
                            if (playerEntity.HPS <= hps)
                            {
                                continue;
                            }
                            break;
                    }
                    var entity = new PlayerEntity
                    {
                        DPS = playerEntity.DPS,
                        DTPS = playerEntity.DTPS,
                        HPS = playerEntity.HPS,
                        Name = playerEntity.Name,
                        Job = playerEntity.Job,
                        TotalOverallDamage = playerEntity.TotalOverallDamage,
                        TotalOverallDamageTaken = playerEntity.TotalOverallDamageTaken,
                        TotalOverallHealing = playerEntity.TotalOverallHealing,
                        PercentOfTotalOverallDamage = playerEntity.PercentOfTotalOverallDamage,
                        PercentOfTotalOverallDamageTaken = playerEntity.PercentOfTotalOverallDamageTaken,
                        PercentOfTotalOverallHealing = playerEntity.PercentOfTotalOverallHealing
                    };
                    target.Players.Add(entity);
                }
                // sort entity based on settings
                switch (parseType)
                {
                    case ParseType.DPS:
                        switch (Settings.Default.DPSWidgetSortDirection)
                        {
                            case "Descending":
                                switch (Settings.Default.DPSWidgetSortProperty)
                                {
                                    case "Name":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderByDescending(p => p.Name));
                                        break;
                                    case "Job":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderByDescending(p => p.Job));
                                        break;
                                    case "DPS":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderByDescending(p => p.DPS));
                                        break;
                                    case "TotalOverallDamage":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderByDescending(p => p.TotalOverallDamage));
                                        break;
                                    case "PercentOfTotalOverallDamage":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderByDescending(p => p.PercentOfTotalOverallDamage));
                                        break;
                                }
                                break;
                            default:
                                switch (Settings.Default.DPSWidgetSortProperty)
                                {
                                    case "Name":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderBy(p => p.Name));
                                        break;
                                    case "Job":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderBy(p => p.Job));
                                        break;
                                    case "DPS":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderBy(p => p.DPS));
                                        break;
                                    case "TotalOverallDamage":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderBy(p => p.TotalOverallDamage));
                                        break;
                                    case "PercentOfTotalOverallDamage":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderBy(p => p.PercentOfTotalOverallDamage));
                                        break;
                                }
                                break;
                        }
                        DPSWidgetViewModel.Instance.ParseEntity = target;
                        break;
                    case ParseType.DTPS:
                        switch (Settings.Default.DTPSWidgetSortDirection)
                        {
                            case "Descending":
                                switch (Settings.Default.DTPSWidgetSortProperty)
                                {
                                    case "Name":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderByDescending(p => p.Name));
                                        break;
                                    case "Job":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderByDescending(p => p.Job));
                                        break;
                                    case "DTPS":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderByDescending(p => p.DTPS));
                                        break;
                                    case "TotalOverallDamageTaken":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderByDescending(p => p.TotalOverallDamageTaken));
                                        break;
                                    case "PercentOfTotalOverallDamageTaken":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderByDescending(p => p.PercentOfTotalOverallDamageTaken));
                                        break;
                                }
                                break;
                            default:
                                switch (Settings.Default.DTPSWidgetSortProperty)
                                {
                                    case "Name":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderBy(p => p.Name));
                                        break;
                                    case "Job":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderBy(p => p.Job));
                                        break;
                                    case "DTPS":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderBy(p => p.DTPS));
                                        break;
                                    case "TotalOverallDamageTaken":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderBy(p => p.TotalOverallDamageTaken));
                                        break;
                                    case "PercentOfTotalOverallDamageTaken":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderBy(p => p.PercentOfTotalOverallDamageTaken));
                                        break;
                                }
                                break;
                        }
                        DTPSWidgetViewModel.Instance.ParseEntity = target;
                        break;
                    case ParseType.HPS:
                        switch (Settings.Default.HPSWidgetSortDirection)
                        {
                            case "Descending":
                                switch (Settings.Default.HPSWidgetSortProperty)
                                {
                                    case "Name":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderByDescending(p => p.Name));
                                        break;
                                    case "Job":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderByDescending(p => p.Job));
                                        break;
                                    case "HPS":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderByDescending(p => p.HPS));
                                        break;
                                    case "TotalOverallHealing":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderByDescending(p => p.TotalOverallHealing));
                                        break;
                                    case "PercentOfTotalOverallHealing":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderByDescending(p => p.PercentOfTotalOverallHealing));
                                        break;
                                }
                                break;
                            default:
                                switch (Settings.Default.HPSWidgetSortProperty)
                                {
                                    case "Name":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderBy(p => p.Name));
                                        break;
                                    case "Job":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderBy(p => p.Job));
                                        break;
                                    case "HPS":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderBy(p => p.HPS));
                                        break;
                                    case "TotalOverallHealing":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderBy(p => p.TotalOverallHealing));
                                        break;
                                    case "PercentOfTotalOverallHealing":
                                        target.Players = new List<PlayerEntity>(target.Players.OrderBy(p => p.PercentOfTotalOverallHealing));
                                        break;
                                }
                                break;
                        }
                        HPSWidgetViewModel.Instance.ParseEntity = target;
                        break;
                }
            }
        }

        public static class Target
        {
        }
    }
}
