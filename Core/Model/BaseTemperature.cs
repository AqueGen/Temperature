using System;
using Core.Model.Interfaces;

namespace Core.Model
{
    public abstract class BaseTemperature : ITemperature
    {
        public abstract DateTime Date { get; set; }
        public abstract double Value { get; set; }


        protected BaseTemperature()
        {
        }

        protected BaseTemperature(ITemperature temperature)
        {
            Date = temperature.Date;
            Value = temperature.Value;
        }
    }
}
