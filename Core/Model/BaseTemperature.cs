using System;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Core.Model
{
    public abstract class BaseTemperature : ITemperature
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }


        protected BaseTemperature()
        {
        }

        protected BaseTemperature(ITemperature temperature)
        {
            if(temperature == null) throw new ArgumentNullException("Temperature is null");

            Date = temperature.Date;
            Value = temperature.Value;
        }
    }
}