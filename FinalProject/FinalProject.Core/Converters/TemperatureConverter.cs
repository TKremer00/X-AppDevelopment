using FinalProject.Core.Extensions;
using FinalProject.Data.Enums;
using FinalProject.Data.Interfaces;
using System.Globalization;

namespace FinalProject.Core.Converters
{
    public class TemperatureConverter : BaseValueConverter<int, int>
    {
        public TemperatureConverter(IPreferencesWrapper preferences) : base(preferences)
        {

        }

        public override int Convert(int value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is not Func<TemperatureUnits> temperatureUnit)
            {
                temperatureUnit = _preferences.GetTemperatureUnit;
            }

            var doubleValue = (double)value;
            return temperatureUnit.Invoke() switch
            {
                TemperatureUnits.Celsius => value,
                TemperatureUnits.Fahrenheit => (int)Math.Round(doubleValue * 1.8 + 32),
                TemperatureUnits.Kelvin => (int)Math.Round(doubleValue + 273.15),
                _ => throw new NotImplementedException(),
            };
        }

        public override int ConvertBack(int value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is not Func<TemperatureUnits> temperatureUnit)
            {
                temperatureUnit = _preferences.GetTemperatureUnit;
            }

            var doubleValue = (double)value;
            return temperatureUnit.Invoke() switch
            {
                TemperatureUnits.Celsius => value,
                TemperatureUnits.Fahrenheit => (int)Math.Round((doubleValue - 32) * .5556),
                TemperatureUnits.Kelvin => (int)Math.Round(doubleValue - 273.15),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
