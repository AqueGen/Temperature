using System.Collections.Generic;

namespace Web.Client.ViewModels
{
    public class IndexViewModel
    {
        public PeriodViewModel Period { get; set; }

        public List<TemperatureViewModel> Temperatures { get; set; }
    }
}