using System;
using Core.Model.Interfaces;

namespace Core.Model
{
    public abstract class BasePeriod : IPeriod
    {
        public abstract DateTime? Start { get; set; }

        public abstract DateTime? End { get; set; }

        protected BasePeriod()
        {
        }

        protected BasePeriod(IPeriod period)
        {
            Start = period.Start;
            End = period.End;
        }
    }
}