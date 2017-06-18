using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Core.Models;

namespace WeatherApp.Core.Services
{
    public interface IWeatherService
    {
        Task<WeatherForecast> GetWeatherForecastAsync(string city);
    }
}
