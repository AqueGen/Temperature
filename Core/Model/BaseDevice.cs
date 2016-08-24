using System;
using System.Collections.Generic;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Core.Model
{
    public abstract class BaseDevice : IDevice
    {
        [JsonIgnore]
        public Guid Guid { get;  set; }

        [JsonProperty("device_name")]
        public string Name { get; set; }

        public BaseDevice()
        {
            Guid = Guid.NewGuid();
        }

        public BaseDevice(IDevice device)
        {
            Name = device.Name;
            Guid = device.Guid;
        }
    }
}