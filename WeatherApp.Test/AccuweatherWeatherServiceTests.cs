using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApp.Core.Exceptions;
using WeatherApp.Infrastructure.Services;

namespace WeatherApp.Test
{
    [TestClass]
    public class AccuweatherWeatherServiceTests
    {
        public const string TEST_API_KEY = @"hoArfRosT1215";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidApiKey()
        {
            var accuweather = new AccuweatherWeatherSerivce(null);
        }

        [TestMethod]
        public async Task FindWarsawTest()
        {
            var accuweather = new AccuweatherWeatherSerivce(TEST_API_KEY);
            var result = await accuweather.GetWeatherForecastAsync("Warsaw");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(WeatherServiceException), AllowDerivedTypes = true)]
        public async Task InvalidCityNameTest()
        {
            var accuweather = new AccuweatherWeatherSerivce(TEST_API_KEY);
            var result = await accuweather.GetWeatherForecastAsync("W");
        }
    }
}
