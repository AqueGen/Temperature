using System.Collections.Generic;

namespace Web.Client.ViewModels
{
    public class IndexViewModel
    {
        public FilterViewModel Filter { get; set; }

        public List<TemperatureViewModel> Temperatures { get; set; }

        public IndexViewModel()
        {
            Filter = new FilterViewModel();
            Temperatures = new List<TemperatureViewModel>();
        }
    }
}