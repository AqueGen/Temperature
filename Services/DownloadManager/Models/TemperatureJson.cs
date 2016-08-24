using System;
using Core.Model;
using Newtonsoft.Json;

namespace Services.DownloadManager.Models
{
    public class TemperatureJson : BaseTemperature
    {
        [JsonProperty("date")]
        public override DateTime Date { get; set; }
        [JsonProperty("value")]
        public override double Value { get; set; }
    }
}