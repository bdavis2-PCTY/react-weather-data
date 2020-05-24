using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ReactWeatherData.Backend.Data.Entites
{
    public class WeatherEntry
    {
        [BsonId]
        public Guid Id { get; set; }

        public DateTime TargetDateTime { get; set; }
        public int TempMaxCel { get; set; }
        public string Summary { get; set; }
    }
}
