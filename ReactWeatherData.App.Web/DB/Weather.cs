using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactWeatherData.App.Web.DB
{
    public class Weather
    {
        [BsonId]
        public Guid Id { get; set; }

        public DateTime TargetDateTime { get; set; }
        public int TempMaxCel { get; set; }
        public string Summary { get; set; }
    }
}
