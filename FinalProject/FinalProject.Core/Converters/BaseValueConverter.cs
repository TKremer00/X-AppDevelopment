using System.Globalization;

namespace FinalProject.Core.Converters
{
    public interface IValueConverter<OriginalType, NewType> : IValueConverter
    {
        NewType Convert(OriginalType value, Type targetType, object parameter, CultureInfo culture);

        OriginalType ConvertBack(NewType value, Type targetType, object parameter, CultureInfo culture);
    }

    public abstract class BaseValueConverter<OriginalType, NewType> : IValueConverter<OriginalType, NewType>
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not OriginalType tValue)
            {
                throw new ArgumentException($"Argument is not of type {nameof(OriginalType)}");
            }

            return Convert(tValue, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not NewType tValue)
            {
                throw new ArgumentException($"Argument is not of type {nameof(OriginalType)}");
            }

            return ConvertBack(tValue, targetType, parameter, culture);
        }

        public NewType Convert(OriginalType value)
        {
            return Convert(value, null, null, null);
        }

        public OriginalType ConvertBack(NewType value)
        {
            return ConvertBack(value, null, null, null);
        }

        public abstract NewType Convert(OriginalType value, Type targetType, object parameter, CultureInfo culture);


        public abstract OriginalType ConvertBack(NewType value, Type targetType, object parameter, CultureInfo culture);
    }
}
