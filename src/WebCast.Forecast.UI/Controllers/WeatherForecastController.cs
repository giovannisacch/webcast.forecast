using Microsoft.AspNetCore.Mvc;
using WebCast.Forecast.Application.Interfaces;

namespace WebCast.Forecast.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastApplicationService _weatherForecastApplicationService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastApplicationService weatherForecastApplicationService)
        {
            _logger = logger;
            _weatherForecastApplicationService = weatherForecastApplicationService;
        }

        [HttpPost("")]
        public async Task<ActionResult> Post(double temperature, DateTime date)
        {
            await _weatherForecastApplicationService.AddWeatherForecast(temperature, date);
            return Ok();
        }

        [HttpGet("WeekForecast")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _weatherForecastApplicationService.GetNextSevenDaysForecast());
        }
    }
}