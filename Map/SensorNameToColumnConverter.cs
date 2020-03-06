using Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Map
{
    public class SensorNameToColumnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var name = value as SensorNames?;

            switch (name)
            {
                case SensorNames.B:
                case SensorNames.E:
                case SensorNames.H:
                    return 1;

                case SensorNames.C:
                case SensorNames.F:
                case SensorNames.I:
                    return 2;

                case SensorNames.A:
                case SensorNames.D:
                case SensorNames.G:
                default:
                    return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}