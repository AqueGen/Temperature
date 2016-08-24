using System;
using Core.Model.Interfaces;
using Services.Temperature.DTO;

namespace Web.Client.ViewModels
{
    public class TemperatureViewModel
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }

        public TemperatureViewModel()
        {
        }

        public TemperatureViewModel(ITemperature temperature)
        {
            Date = temperature.Date;
            Value = temperature.Value;
        }
    }
}