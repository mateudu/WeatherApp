using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Core.Models
{
    public class WeatherForecast
    {
        public int Id { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public decimal TemperatureCelsius { get; set; }
        
        public string Description { get; set; }
    }
}
