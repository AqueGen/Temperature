using System;
using Core.Model;
using Core.Model.Interfaces;

namespace Data.Store.Models
{
    public class Temperature : BaseTemperature
    {
        public int Id { get; set; }

        public override DateTime Date { get; set; }
        public override double Value { get; set; }

        public Temperature() : base()
        {
        }

        public Temperature(ITemperature temperature) : base(temperature)
        {
        }



    }
}