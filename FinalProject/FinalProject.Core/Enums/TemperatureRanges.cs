﻿using FinalProject.Core.Converters;

namespace FinalProject.Core.Enums
{
    public enum TemperatureRanges
    {
        _0_till_10,
        _10_till_20,
        _20_till_30,
    }

    public static class TemperatureRangesExtensions
    {
        public static (int Min, int Max) GetTemperatures(this TemperatureRanges temperatureRanges)
        {
            return temperatureRanges switch
            {
                TemperatureRanges._0_till_10 => (0, 10),
                TemperatureRanges._10_till_20 => (10, 20),
                TemperatureRanges._20_till_30 => (20, 30),
                _ => throw new NotImplementedException(),
            };
        }

        public static (int Min, int Max) GetPreferredTemperatures(this TemperatureRanges temperatureRanges)
        {
            var (min, max) = GetTemperatures(temperatureRanges);
            return (TemperatureConverter.temperatureConverter.Convert(min), TemperatureConverter.temperatureConverter.Convert(max));
        }
    }
}
