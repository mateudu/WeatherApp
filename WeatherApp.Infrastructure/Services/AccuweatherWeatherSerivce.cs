using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WeatherApp.Core.Exceptions;
using WeatherApp.Core.Models;
using WeatherApp.Core.Repositories;
using WeatherApp.Core.Services;

namespace WeatherApp.Infrastructure.Services
{
    public class AccuweatherWeatherSerivce : IWeatherService
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;

        private string GetSearchRequestUrl(string city) =>
            $"http://apidev.accuweather.com/locations/v1/search?q={city}&apikey={_apiKey}";
        private string GetForcastRequestUrl(string cityId) =>
            $"http://apidev.accuweather.com/currentconditions/v1/{cityId}.json?language=en&apikey={_apiKey}";

        public AccuweatherWeatherSerivce(string apiKey)
        {
            _client = new HttpClient();
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        }

        public async Task<WeatherForecast> GetWeatherForecastAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new WeatherServiceException("City name is empty",
                    WeatherServiceException.ServiceException.CityNameNullOrEmpty);
            }

            // Get suggestions
            var searchReq = await _client.GetAsync(GetSearchRequestUrl(city));
            var searchResp = await searchReq.Content.ReadAsStringAsync();
            var searchObj = JArray.Parse(searchResp);

            if (searchObj == null || searchObj.Count == 0)
            {
                throw new WeatherServiceException("City not found", 
                    WeatherServiceException.ServiceException.NotFound);
            }

            // Parse city Id & name
            string cityId = searchObj[0]["Key"].ToString();
            string cityName = searchObj[0]["EnglishName"].ToString();

            var forecastReq = await _client.GetAsync(GetForcastRequestUrl(cityId));
            var forecastResp = await forecastReq.Content.ReadAsStringAsync();
            var forecastObj = JArray.Parse(forecastResp);

            var result = new WeatherForecast()
            {
                City = cityName,
                Description = forecastObj[0]["WeatherText"].ToString(),
                TemperatureCelsius = forecastObj[0]["Temperature"]["Metric"]["Value"].ToObject<decimal>()
            };
            return result;
        }
    }
}
