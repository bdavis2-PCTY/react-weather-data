using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReactWeatherData.App.Web.DB;
using ReactWeatherData.App.Web.Model;

namespace ReactWeatherData.App.Web.Data
{
    public class WeatherContext : ContextBase
    {
        public WeatherContext(IOptions<Settings> pSettings)
            : base(pSettings) { }

        public IMongoCollection<Weather> Weather
        {
            get
            {
                return database.GetCollection<Weather>("Weather");
            }
        }
    }
}
