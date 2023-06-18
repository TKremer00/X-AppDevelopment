using System.Globalization;

namespace FinalProject.Core.Converters
{
    public class BooleanInverterConverter : BaseValueConverter<bool, bool>
    {
        public override bool Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return !value;
        }

        public override bool ConvertBack(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return !value;
        }
    }
}
