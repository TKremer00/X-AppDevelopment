using FinalProject.Core.Converters;
using FinalProject.Data.Enums;
using FinalProject.Data.Interfaces;

namespace FinalProject.Core.Extensions
{
    public static class TemperatureRangesExtensions
    {
        public static (int Min, int Max) GetTemperatures(this TemperatureRanges temperatureRanges)
        {
            return temperatureRanges switch
            {
                TemperatureRanges._0_till_10 => (0, 10),
                TemperatureRanges._10_till_20 => (10, 20),
                TemperatureRanges._20_till_30 => (20, 30),
                TemperatureRanges._30_till_40 => (30, 40),
                _ => throw new NotImplementedException(),
            };
        }

        public static (int Min, int Max) GetPreferredTemperatures(this TemperatureRanges temperatureRanges, IPreferencesWrapper preferences)
        {
            var (min, max) = GetTemperatures(temperatureRanges);
            var temperatureConverter = new TemperatureConverter(preferences);
            return (temperatureConverter.Convert(min), temperatureConverter.Convert(max));
        }
    }
}
