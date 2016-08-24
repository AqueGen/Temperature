using System;

namespace Core.Model.Interfaces
{
    public interface ITemperature
    {
        DateTime Date { get; set; }
        double Value { get; set; }
    }
}