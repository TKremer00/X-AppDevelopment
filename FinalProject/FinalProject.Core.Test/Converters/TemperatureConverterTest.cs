using FinalProject.Core.Converters;
using FinalProject.Data.Enums;
using FinalProject.Data.Interfaces;
using Moq;
using NUnit.Framework;

namespace FinalProject.Core.Test.Converters
{
    public class TemperatureConverterTest
    {
        [Test]
        [TestCase(10, TemperatureUnits.Kelvin, 283)]
        [TestCase(10, TemperatureUnits.Fahrenheit, 50)]
        [TestCase(12, TemperatureUnits.Fahrenheit, 54)]
        public void ConvertTemperature(int tempInCelsius, TemperatureUnits temperatureUnit, int expectedOutput)
        {
            var preferences = new Mock<IPreferencesWrapper>();
            preferences.Setup(p => p.Get(It.IsAny<string>(), It.IsAny<int>())).Returns((int)temperatureUnit);

            var converter = new TemperatureConverter(preferences.Object);
            var output = converter.Convert(tempInCelsius);


            Assert.That(output, Is.EqualTo(expectedOutput));
        }

        [Test]
        [TestCase(283, TemperatureUnits.Kelvin, 10)]
        [TestCase(50, TemperatureUnits.Fahrenheit, 10)]
        [TestCase(54, TemperatureUnits.Fahrenheit, 12)]
        public void ConvertTemperatureBack(int temp, TemperatureUnits tempUnit, int expectedCelsius)
        {
            var preferences = new Mock<IPreferencesWrapper>();
            preferences.Setup(p => p.Get(It.IsAny<string>(), It.IsAny<int>())).Returns((int)tempUnit);

            var converter = new TemperatureConverter(preferences.Object);
            var output = converter.ConvertBack(temp);


            Assert.That(output, Is.EqualTo(expectedCelsius));
        }
    }
}
