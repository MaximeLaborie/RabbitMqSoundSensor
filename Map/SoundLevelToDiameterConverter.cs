using System;
using System.Globalization;
using System.Windows.Data;

namespace Map
{
    public class SoundLevelToDiameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var soundLevel = (short)value;
            return 50 + 150 * soundLevel / short.MaxValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}