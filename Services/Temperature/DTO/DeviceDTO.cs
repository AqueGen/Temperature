using System;
using System.Collections.Generic;
using Core.Model;
using Core.Model.Interfaces;

namespace Services.Temperature.DTO
{
    public class DeviceDTO : BaseDevice
    {
        public IEnumerable<ITemperature> Temperatures { get; set; }

        public DeviceDTO() : base()
        {
        }

        public DeviceDTO(IDevice device) : base(device)
        {
        }
    }
}