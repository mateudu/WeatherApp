using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core.Exceptions
{
    public class WeatherServiceException : Exception
    {
        public WeatherServiceException(string message, ServiceException type) : base(message)
        {
            
        }
        public ServiceException Type { get; set; }
        public enum ServiceException
        {
            NotFound,
            CityNameNullOrEmpty
        }
    }
}
