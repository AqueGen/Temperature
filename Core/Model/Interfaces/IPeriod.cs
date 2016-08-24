using System;

namespace Core.Model.Interfaces
{
    public interface IPeriod
    {
        DateTime? Start { get; set; }

        DateTime? End { get; set; }
    }
}