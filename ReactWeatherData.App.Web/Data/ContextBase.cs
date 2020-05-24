using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReactWeatherData.App.Web.Model;

namespace ReactWeatherData.App.Web.Data
{
    public abstract class ContextBase
    {
        protected IMongoDatabase database { get; }

        public ContextBase(IOptions<Settings> pSettings)
        {
            MongoClient dbClient = new MongoClient(pSettings.Value.ConnectionString);
            if (dbClient != null)
            {
                database = dbClient.GetDatabase(pSettings.Value.Database);
            }
        }
    }
}
