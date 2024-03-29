using LintCoder.Identity.API.Infrastructure.Attributes;
using LintCoder.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LintCoder.Identity.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiLog(LogLevel.Information, ApiLogEvent.WriteToConsole, ApiLogEvent.WriteToFile)]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [LCRoleAuthorize(Policy = "LintCoderRole", Roles = LCRoleConstants.Basic)]
        [HttpGet(Name = "GetWeatherForecast")]
        //[IgnoreApiLog]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}