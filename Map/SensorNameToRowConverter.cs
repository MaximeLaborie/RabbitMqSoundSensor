using Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Map
{
    public class SensorNameToRowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var name = value as SensorNames?;

            switch (name)
            {
                case SensorNames.D:
                case SensorNames.E:
                case SensorNames.F:
                    return 1;

                case SensorNames.G:
                case SensorNames.H:
                case SensorNames.I:
                    return 2;

                case SensorNames.A:
                case SensorNames.B:
                case SensorNames.C:
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