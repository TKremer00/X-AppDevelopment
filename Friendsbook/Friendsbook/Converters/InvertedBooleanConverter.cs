using System.Globalization;

namespace Friendsbook.Converters
{
    public class InvertedBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool boolValue)
            {
                throw new ArgumentException("Argument value must be of type boolean");
            }

            return !boolValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool boolValue)
            {
                throw new ArgumentException("Argument value must be of type boolean");
            }

            return !boolValue;
        }
    }
}
