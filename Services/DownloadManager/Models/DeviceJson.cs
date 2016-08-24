using System.Collections.Generic;
using Core.Model;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Services.DownloadManager.Models
{
    public class DeviceJson : BaseDevice
    {
        [JsonProperty("values")]
        public List<TemperatureJson> Temperatures { get; set; }

        public DeviceJson() : base()
        {
        }

        public DeviceJson(IDevice device) : base(device)
        {
        }
    }
}