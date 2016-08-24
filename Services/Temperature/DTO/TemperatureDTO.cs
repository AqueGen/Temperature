using System;
using Core.Model;
using Core.Model.Interfaces;

namespace Services.Temperature.DTO
{
    public class TemperatureDTO : BaseTemperature
    {
        public override DateTime Date { get; set; }
        public override double Value { get; set; }

        public TemperatureDTO() : base()
        {
        }

        public TemperatureDTO(ITemperature temperature) : base(temperature)
        {
        }
    }
}