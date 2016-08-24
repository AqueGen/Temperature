using System;
using Core.Model;
using Core.Model.Interfaces;

namespace Data.Store.Models
{
    public class Temperature : BaseTemperature
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }
        public virtual Device Device{ get; set;}

        public Temperature() : base()
        {
        }

        public Temperature(ITemperature temperature) : base(temperature)
        {
        }



    }
}