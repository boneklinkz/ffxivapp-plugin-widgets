// FFXIVAPP.Plugin.Widgets
// StringToBrushConverter.cs
// 
// © 2013 ZAM Network LLC

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using FFXIVAPP.Common.Core.Memory.Enums;
using FFXIVAPP.Plugin.Widgets.Properties;

namespace FFXIVAPP.Plugin.Widgets.Converters
{
    public class StringToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var param = "DEFAULT";
            try
            {
                param = ((Actor.Job) value).ToString()
                                           .ToUpperInvariant();
            }
            catch (Exception ex)
            {
            }
            var brush = ColorStringToBrush(Settings.Default.DefaultProgressBarForeground);
            switch (param)
            {
                case "PLD":
                    brush = ColorStringToBrush(Settings.Default.PLDProgressBarForeground);
                    break;
                case "DRG":
                    brush = ColorStringToBrush(Settings.Default.DRGProgressBarForeground);
                    break;
                case "BLM":
                    brush = ColorStringToBrush(Settings.Default.BLMProgressBarForeground);
                    break;
                case "WAR":
                    brush = ColorStringToBrush(Settings.Default.WARProgressBarForeground);
                    break;
                case "WHM":
                    brush = ColorStringToBrush(Settings.Default.WHMProgressBarForeground);
                    break;
                case "SCH":
                    brush = ColorStringToBrush(Settings.Default.SCHProgressBarForeground);
                    break;
                case "MNK":
                    brush = ColorStringToBrush(Settings.Default.MNKProgressBarForeground);
                    break;
                case "BRD":
                    brush = ColorStringToBrush(Settings.Default.BRDProgressBarForeground);
                    break;
                case "SMN":
                    brush = ColorStringToBrush(Settings.Default.SMNProgressBarForeground);
                    break;
            }
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static SolidColorBrush ColorStringToBrush(string color)
        {
            color = color.Replace("#", "");
            try
            {
                return (SolidColorBrush) (new BrushConverter().ConvertFrom(color));
            }
            catch (Exception ex)
            {
            }
            try
            {
                switch (color.Length)
                {
                    case 8:
                        return new SolidColorBrush(Color.FromArgb(Byte.Parse(color.Substring(0, 2), NumberStyles.HexNumber), Byte.Parse(color.Substring(2, 2), NumberStyles.HexNumber), Byte.Parse(color.Substring(4, 2), NumberStyles.HexNumber), Byte.Parse(color.Substring(6, 2), NumberStyles.HexNumber)));
                    case 6:
                        return new SolidColorBrush(Color.FromRgb(Byte.Parse(color.Substring(0, 2), NumberStyles.HexNumber), Byte.Parse(color.Substring(2, 2), NumberStyles.HexNumber), Byte.Parse(color.Substring(4, 2), NumberStyles.HexNumber)));
                    default:
                        return Brushes.Green;
                }
            }
            catch (Exception ex)
            {
                return Brushes.Green;
            }
        }
    }
}
