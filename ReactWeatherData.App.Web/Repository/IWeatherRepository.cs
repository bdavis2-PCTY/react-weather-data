using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactWeatherData.App.Web.Repository
{
    public interface IWeatherRepository
    {
        Task<IEnumerable<DB.Weather>> GetWeather();
        Task AddWeather(DB.Weather pWeather);
    }
}
