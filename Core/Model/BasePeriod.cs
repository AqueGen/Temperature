using System;
using Core.Model.Interfaces;

namespace Core.Model
{
    public abstract class BasePeriod : IPeriod
    {
        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        protected BasePeriod()
        {
        }

        protected BasePeriod(IPeriod period)
        {
            if (period == null) throw new ArgumentNullException("Period is null");
            Start = period.Start;
            End = period.End;
        }
    }
}