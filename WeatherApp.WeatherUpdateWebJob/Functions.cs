using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using WeatherApp.Core.Repositories;
using WeatherApp.Core.Services;
using WeatherApp.Infrastructure;
using WeatherApp.Infrastructure.Repositories;
using WeatherApp.Infrastructure.Services;

namespace WeatherApp.WeatherUpdateWebJob
{
    public class Functions
    {
        private static readonly IWeatherForecastRepository _repo;
        private static readonly IWeatherService _weatherSvc;
        static Functions()
        {
            var ctx = new WeatherForecastContext();
            _repo = new WeatherForecastRepository(ctx);
            _weatherSvc = new AccuweatherWeatherSerivce(CloudConfigurationManager.GetSetting("AccuweatherWeatherService:ApiKey"));
        }

        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("queue")] string message, TextWriter log)
        {

            var msg = JsonConvert.DeserializeObject<BackgroundWeatherUpdateService.Message>(message);

            log.WriteLine(message);
        }
    }
}
