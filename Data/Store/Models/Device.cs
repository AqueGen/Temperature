using System;
using System.Collections.Generic;
using Core.Model;
using Core.Model.Interfaces;

namespace Data.Store.Models
{
    public class Device : BaseDevice
    {
        public int Id { get; set; }

        public virtual ICollection<Temperature> Temperatures { get; set; }

        public Device() : base()
        {
        }

        public Device(IDevice device) : base(device)
        {
        }
    }
}