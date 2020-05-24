using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReactWeatherData.Backend.Data.Repository;

namespace ReactWeatherData.Backend.Data
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure Mongo
            services.Configure<Settings>(options =>
            {
                options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database = Configuration.GetSection("MongoConnection:Database").Value;
            });

            services.AddTransient<IWeatherRepository, WeatherRepository>();
        }
    }
}
