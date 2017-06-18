using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Core.Models;

namespace WeatherApp.Infrastructure
{
    public class WeatherForecastContext : DbContext
    {
        public WeatherForecastContext() : base("DefaultConnection")
        {
            
        }
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
