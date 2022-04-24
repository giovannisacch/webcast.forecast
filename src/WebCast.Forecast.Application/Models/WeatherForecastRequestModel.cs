using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCast.Forecast.Application.Models
{
    public class WeatherForecastRequestModel
    {
        public double Temperature { get; set; }
        public DateTime Date { get; set; }
    }
}
