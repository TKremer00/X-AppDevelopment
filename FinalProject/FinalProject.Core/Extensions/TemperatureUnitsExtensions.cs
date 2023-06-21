using FinalProject.Data.Enums;

namespace FinalProject.Core.Extensions
{
    public static class TemperatureUnitsExtensions
    {
        public static string GetSymbol(this TemperatureUnits temperatureUnits)
        {
            return temperatureUnits switch
            {
                TemperatureUnits.Celsius => "°C",
                TemperatureUnits.Fahrenheit => "°F",
                TemperatureUnits.Kelvin => "K",
                _ => throw new NotImplementedException(),
            };
        }
    }
}
