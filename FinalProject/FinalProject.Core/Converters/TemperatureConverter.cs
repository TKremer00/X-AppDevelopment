using FinalProject.Core.Enums;
using FinalProject.Core.Helpers;
using System.Globalization;

namespace FinalProject.Core.Converters
{
    public class TemperatureConverter : BaseValueConverter<int, int>
    {
        public static readonly TemperatureConverter temperatureConverter = new TemperatureConverter();

        public override int Convert(int value, Type targetType, object parameter, CultureInfo culture)
        {
            var doubleValue = (double)value;
            return SettingsHelper.GetTemperatureUnitSetting() switch
            {
                TemperatureUnits.Celsius => value,
                TemperatureUnits.Fahrenheit => (int)Math.Round(doubleValue * 1.8 + 32),
                TemperatureUnits.Kelvin => (int)Math.Round(doubleValue + 273.15),
                _ => throw new NotImplementedException(),
            };
        }

        public override int ConvertBack(int value, Type targetType, object parameter, CultureInfo culture)
        {
            var doubleValue = (double)value;
            return SettingsHelper.GetTemperatureUnitSetting() switch
            {
                TemperatureUnits.Celsius => value,
                TemperatureUnits.Fahrenheit => (int)Math.Round((doubleValue - 32) * .5556),
                TemperatureUnits.Kelvin => (int)Math.Round(doubleValue - 273.15),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
