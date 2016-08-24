using System;
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

        public TemperatureViewModel(TemperatureDTO temperatureDTO)
        {
            Date = temperatureDTO.Date;
            Value = temperatureDTO.Value;
        }
    }
}