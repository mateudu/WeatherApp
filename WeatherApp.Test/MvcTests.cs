using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApp.Core.Models;
using WeatherApp.Test.Mocks;
using WeatherApp.Web.Controllers;

namespace WeatherApp.Test
{
    [TestClass]
    public class MvcTests
    {
        [TestMethod]
        public async Task DisplayAllForecasts()
        {
            var repo = new MockWeatherRepository();
            var controller = new WeatherForecastsController(repo, null);
            
            var result = await controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var expected = (await repo.GetAllAsync()).ToList();
            var model = (result as ViewResult).ViewData.Model;
            Assert.IsInstanceOfType(model, typeof(List<WeatherForecast>));
            var actual = (List<WeatherForecast>)model;
            Assert.IsTrue(actual.SequenceEqual(expected));
        }
    }
}
