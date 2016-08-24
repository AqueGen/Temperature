using System;
using System.Collections.Generic;

namespace Core.Model.Interfaces
{
    public interface IDevice
    {
        Guid Guid { get; set; }
        string Name { get; set; }
    }
}