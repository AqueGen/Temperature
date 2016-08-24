using System;
using Core.Model;
using Core.Model.Interfaces;

namespace Services.Temperature.DTO
{
    public class PeriodDTO : BasePeriod
    {
        public override DateTime? Start { get; set; }

        public override DateTime? End { get; set; }

        public PeriodDTO()
        {
        }

        public PeriodDTO(IPeriod period) : base(period)
        {
        }


    }
}