using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReactWeatherData.Backend.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactWeatherData.App.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherRepository _weatherRepo;

        public WeatherForecastController(ILogger<WeatherForecastController> pLogger,
            IWeatherRepository pWeatherRepository)
        {
            _logger = pLogger;
            _weatherRepo = pWeatherRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecast()
        {
            var weather = _weatherRepo.GetWeather();
            List<WeatherForecast> weatherItems = new List<WeatherForecast>();

            // Add real data
            weatherItems.AddRange(weather.Select(x => new WeatherForecast()
            {
                Date = x.TargetDateTime,
                TemperatureC = x.TempMaxCel,
                Summary = x.Summary
            }));

            return weatherItems;
        }

        [HttpGet]
        public async Task AddRandomWeather()
        {
            Backend.Data.Entites.WeatherEntry weather = new Backend.Data.Entites.WeatherEntry()
            {
                Summary = $"Randomly generated at {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}",
                TargetDateTime = DateTime.Today,
                TempMaxCel = new Random().Next()
            };

            await _weatherRepo.AddWeather(weather);
        }

        [HttpGet]
        public async Task DeleteAllWeather()
        {
            await _weatherRepo.DeleteAllWeather();
        }
    }
}
