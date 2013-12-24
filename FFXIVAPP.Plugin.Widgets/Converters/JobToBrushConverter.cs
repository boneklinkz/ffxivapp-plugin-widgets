// FFXIVAPP.Plugin.Widgets
// JobToBrushConverter.cs
// 
// © 2013 ZAM Network LLC

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using FFXIVAPP.Common.Core.Memory;

namespace FFXIVAPP.Plugin.Widgets.Converters
{
    public class JobToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var job = ((Enums.Actor.Job) value).ToString()
                                               .ToUpperInvariant();
            switch (job)
            {
                case "GLD":
                case "PLD":
                    return Brushes.SkyBlue;
                case "LNC":
                case "DRG":
                    return Brushes.DarkSlateBlue;
                case "THM":
                case "BLM":
                    return Brushes.Purple;
                case "MRD":
                case "WAR":
                    return Brushes.Red;
                case "CNJ":
                case "WHM":
                    return Brushes.DarkGray;
                case "ACN":
                    return Brushes.NavajoWhite;
                case "SCH":
                    return Brushes.MediumPurple;
                case "PGL":
                case "MNK":
                    return Brushes.Gold;
                case "ARC":
                case "BRD":
                    return Brushes.GreenYellow;
                case "SMN":
                    return Brushes.LimeGreen;
                default:
                    return Brushes.RosyBrown;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
