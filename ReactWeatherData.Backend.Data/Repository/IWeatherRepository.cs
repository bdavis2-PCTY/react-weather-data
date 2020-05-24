using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactWeatherData.Backend.Data.Repository
{
    public interface IWeatherRepository
    {
        IList<Backend.Data.Entites.WeatherEntry> GetWeather();

        Task AddWeather(Backend.Data.Entites.WeatherEntry pWeather);

        Task DeleteAllWeather();
    }
}
