using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Szofttech_WPF.Converters
{
    public class ColorConverter : IValueConverter
    {
        Color Light_Blue = Color.FromRgb(50, 105, 168);
        Color Dark_Blue = Color.FromRgb(37, 57, 66);
        Color Blue = Color.FromRgb(186, 217, 232);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color val = (Color)value;

            if (val == Light_Blue)
                return "Light-Blue";
            else if (val == Dark_Blue)
                return "Dark-Blue";
            else if (val == Blue)
                return "Blue";
            else
                return "Blue";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "Light-Blue":
                    return Light_Blue;
                case "Dark-Blue":
                    return Dark_Blue;
                case "Blue":
                    return Blue;
                default:
                    return Blue;
            }
        }
    }
}
