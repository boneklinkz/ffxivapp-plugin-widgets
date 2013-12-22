// FFXIVAPP.Plugin.Widgets
// EntityHelper.cs
// 
// © 2013 ZAM Network LLC

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
                switch (parseType)
                {
                    case ParseType.DPS:
                        DPSWidgetViewModel.Instance.ParseEntity = target;
                        break;
                    case ParseType.DTPS:
                        DTPSWidgetViewModel.Instance.ParseEntity = target;
                        break;
                    case ParseType.HPS:
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
