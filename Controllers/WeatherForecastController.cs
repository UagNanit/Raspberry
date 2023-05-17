using Iot.Device.DHTxx;
using Microsoft.AspNetCore.Mvc;
using Raspberry.Models;
using System.Device.Gpio;
using System.Net;

namespace Raspberry.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly GpioClient _gpioClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, GpioClient gpioClient)
        {
            _logger = logger;
            _gpioClient = gpioClient;
        }





        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}