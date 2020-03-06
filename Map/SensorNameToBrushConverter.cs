using Model;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Map
{
    public class SensorNameToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var name = value as SensorNames?;

            switch (name)
            {
                case SensorNames.A:
                    return new SolidColorBrush(Colors.RosyBrown);

                case SensorNames.B:
                    return new SolidColorBrush(Colors.DarkOrchid);

                case SensorNames.C:
                    return new SolidColorBrush(Colors.SkyBlue);

                case SensorNames.D:
                    return new SolidColorBrush(Colors.LightSeaGreen);

                case SensorNames.F:
                    return new SolidColorBrush(Colors.DarkOrange);

                case SensorNames.G:
                    return new SolidColorBrush(Colors.Gray);

                case SensorNames.H:
                    return new SolidColorBrush(Colors.Crimson);

                case SensorNames.I:
                    return new SolidColorBrush(Colors.YellowGreen);

                case SensorNames.E:
                default:
                    return new SolidColorBrush(Colors.Gold);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}