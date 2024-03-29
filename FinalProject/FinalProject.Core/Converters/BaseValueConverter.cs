﻿using FinalProject.Data.Interfaces;
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
        protected readonly IPreferencesWrapper _preferences;

        public BaseValueConverter()
        {
            _preferences = null;
        }

        public BaseValueConverter(IPreferencesWrapper preferences)
        {
            _preferences = preferences;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not OriginalType tValue)
            {
                throw new ArgumentException($"Argument could not be converter because the argument is not of type {typeof(OriginalType).Name}");
            }

            return Convert(tValue, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not NewType tValue)
            {
                throw new ArgumentException($"Argument could not be converter back because the argument is not of type {typeof(OriginalType).Name}");
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
