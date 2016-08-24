using System;
using Core.Model;
using Core.Model.Interfaces;
using Newtonsoft.Json;

namespace Services.DownloadManager.Models
{
    public class TemperatureJson : BaseTemperature
    {
        protected TemperatureJson()
        {
        }

        protected TemperatureJson(ITemperature temperature) : base(temperature)
        {
        }
    }
}