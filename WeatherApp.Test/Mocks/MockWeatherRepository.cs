using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Core.Models;
using WeatherApp.Core.Repositories;

namespace WeatherApp.Test.Mocks
{
    public class MockWeatherRepository : IWeatherForecastRepository
    {
        private readonly List<WeatherForecast> _list = new List<WeatherForecast>();
        public async Task<WeatherForecast> AddAsync(WeatherForecast forecast)
        {
            _list.Add(forecast);
            return forecast;
        }

        public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
        {
            return _list;
        }

        public async Task<WeatherForecast> GetAsync(int id)
        {
            return _list.FirstOrDefault(x => x.Id == id);
        }

        public async Task<WeatherForecast> UpdateAsync(WeatherForecast forecast)
        {
            var elem = _list.FirstOrDefault(x => x.Id == forecast.Id);
            if (elem != null)
            {
                _list.Remove(elem);
            }
            _list.Add(forecast);
            return forecast;
        }

        public async Task DeleteAsync(int id)
        {
            var elem = _list.FirstOrDefault(x => x.Id == id);
            if (elem != null)
            {
                _list.Remove(elem);
            }
        }
    }
}
