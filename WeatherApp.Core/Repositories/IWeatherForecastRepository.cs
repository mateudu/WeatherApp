using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Core.Models;

namespace WeatherApp.Core.Repositories
{
    public interface IWeatherForecastRepository
    {
        Task<WeatherForecast> AddAsync(WeatherForecast forecast);
        Task<IEnumerable<WeatherForecast>> GetAllAsync();
        Task<WeatherForecast> GetAsync(int id);
        Task<WeatherForecast> UpdateAsync(WeatherForecast forecast);
        Task DeleteAsync(int id);
    }
}
