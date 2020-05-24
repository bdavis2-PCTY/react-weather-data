using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReactWeatherData.App.Web.Data;
using ReactWeatherData.App.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactWeatherData.App.Web.Repository
{
    public class WeatherRepository : IWeatherRepository
    {
        private WeatherContext _weatherContext = null;
        public WeatherRepository(IOptions<Settings> pSettings)
        {
            _weatherContext = new WeatherContext(pSettings);
        }

        public async Task<IEnumerable<DB.Weather>> GetWeather()
        {
            try
            {
                return (await _weatherContext.Weather.Find(_ => true).ToListAsync());
            }catch(Exception ex)
            {
                throw;
            }
        }

        public async Task AddWeather(DB.Weather pWeather)
        {
            try
            {
                await _weatherContext.Weather.InsertOneAsync(pWeather);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
