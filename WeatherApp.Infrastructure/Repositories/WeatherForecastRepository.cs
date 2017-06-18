using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Core.Models;
using WeatherApp.Core.Repositories;

namespace WeatherApp.Infrastructure.Repositories
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly WeatherForecastContext _ctx;
        public WeatherForecastRepository(WeatherForecastContext ctx)
        {
            this._ctx = ctx;
        }
        public async Task<WeatherForecast> AddAsync(WeatherForecast forecast)
        {
            _ctx.WeatherForecasts.Add(forecast);
            await _ctx.SaveChangesAsync();
            return forecast;
        }

        public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
        {
            return await _ctx.WeatherForecasts.ToListAsync();
        }

        public async Task<WeatherForecast> GetAsync(int id)
        {
            var forecast = await _ctx.WeatherForecasts.FirstOrDefaultAsync(x => x.Id == id);
            return forecast;
        }

        public async Task<WeatherForecast> UpdateAsync(WeatherForecast forecast)
        {
            _ctx.WeatherForecasts.AddOrUpdate(forecast);
            await _ctx.SaveChangesAsync();
            return forecast;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _ctx.WeatherForecasts.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                _ctx.WeatherForecasts.Remove(entity);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}
