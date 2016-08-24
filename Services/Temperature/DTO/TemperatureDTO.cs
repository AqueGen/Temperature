using System;
using Core.Model;
using Core.Model.Interfaces;

namespace Services.Temperature.DTO
{
    public class TemperatureDTO : BaseTemperature
    {
        public TemperatureDTO() : base()
        {
        }

        public TemperatureDTO(ITemperature temperature) : base(temperature)
        {
        }
    }
}