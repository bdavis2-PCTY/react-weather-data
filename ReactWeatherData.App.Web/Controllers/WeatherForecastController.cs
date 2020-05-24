using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.IO;
using ReactWeatherData.App.Web.Repository;

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

            var weather = await _weatherRepo.GetWeather();
            List<WeatherForecast> weatherItems = new List<WeatherForecast>();

            // Add dummy weather
            var rng = new Random();
            for (int i = 0; i < 5; i++)
            {
                WeatherForecast dummyWeather = new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(i),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                };

                weatherItems.Add(dummyWeather);
            }

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
            DB.Weather weather = new DB.Weather()
            {
                Summary = $"Randomly generated at {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}",
                TargetDateTime = DateTime.Today,
                TempMaxCel = new Random().Next()
            };

            await _weatherRepo.AddWeather(weather);
        }
    }
}
