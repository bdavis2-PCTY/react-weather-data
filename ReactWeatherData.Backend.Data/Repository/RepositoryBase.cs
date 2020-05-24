using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ReactWeatherData.Backend.Data.Repository
{
    internal abstract class RepositoryBase
    {
        protected IMongoDatabase database { get; }

        public RepositoryBase(IOptions<Settings> pSettings)
        {
            MongoClient dbClient = new MongoClient(pSettings.Value.ConnectionString);
            if (dbClient != null)
            {
                database = dbClient.GetDatabase(pSettings.Value.Database);
            }
        }
    }
}
