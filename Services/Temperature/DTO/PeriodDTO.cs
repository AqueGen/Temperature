using System;
using Core.Model;
using Core.Model.Interfaces;

namespace Services.Temperature.DTO
{
    public class PeriodDTO : BasePeriod
    {
        public PeriodDTO()
        {
        }

        public PeriodDTO(IPeriod period) : base(period)
        {
        }


    }
}