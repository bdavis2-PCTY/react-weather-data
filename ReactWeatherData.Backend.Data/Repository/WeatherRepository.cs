using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactWeatherData.Backend.Data.Repository
{
    internal class WeatherRepository : RepositoryBase, IWeatherRepository
    {
        public WeatherRepository(IOptions<Settings> pSettings)
            : base(pSettings)
        {
        }

        #region Methods

        public IList<Backend.Data.Entites.WeatherEntry> GetWeather()
        {
            try
            {
                return (from w in weatherEntry.AsQueryable()
                        where w.TargetDateTime >= DateTime.Today
                        select w).Take(5).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AddWeather(Backend.Data.Entites.WeatherEntry pWeather)
        {
            try
            {
                await weatherEntry.InsertOneAsync(pWeather);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteAllWeather()
        {
            try
            {
                await weatherEntry.DeleteManyAsync(_ => true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion Methods

        #region Data access properties

        private IMongoCollection<Backend.Data.Entites.WeatherEntry> weatherEntry
        {
            get
            {
                return database.GetCollection<Backend.Data.Entites.WeatherEntry>("Weather");
            }
        }

        #endregion Data access properties

    }
}
