namespace FinalProject.Core.Enums
{
    public enum TemperatureUnits
    {
        Celsius,
        Fahrenheit,
        Kelvin
    }

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
