using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Web.Mvc;
using Services.Temperature.DTO;

namespace Web.Client.ViewModels
{
    [ModelBinder(typeof(FilterModelBinder))]
    public class FilterViewModel
    {
        public Guid DeviceGuid { get; set; }
        public List<DeviceViewModel> Devices { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public FilterViewModel()
        {
            Devices = new List<DeviceViewModel>();
        }

        public PeriodDTO ToDTO()
        {
            return new PeriodDTO
            {
                Start = Start,
                End = End
            };
        }
    }

    public class FilterModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            NameValueCollection request = controllerContext.HttpContext.Request.HttpMethod == "GET"
                ? controllerContext.HttpContext.Request.QueryString
                : controllerContext.HttpContext.Request.Form;

            FilterViewModel viewModel = new FilterViewModel();

            var start = request.Get(nameof(viewModel.Start));
            var end = request.Get(nameof(viewModel.End));
            var guid = request.Get(nameof(viewModel.DeviceGuid));
            if (!string.IsNullOrWhiteSpace(guid))
            {
                try
                {
                    viewModel.DeviceGuid = new Guid(guid);
                }
                catch (OverflowException ex)
                {
                    Trace.TraceError($"Overflow exception. {ex}");
                }
            }
         

            var dateTimeFormat = "dd-MM-yyyy hh:mm";
            try
            {
                if (!string.IsNullOrWhiteSpace(start))
                {
                    viewModel.Start = DateTime.ParseExact(start, dateTimeFormat, CultureInfo.InvariantCulture);
                }
                if (!string.IsNullOrWhiteSpace(end))
                {
                    viewModel.End = DateTime.ParseExact(end, dateTimeFormat, CultureInfo.InvariantCulture);
                }
            }
            catch (FormatException ex)
            {
                Trace.TraceError($"Incorrect date format from web. Format should be {dateTimeFormat}.");
            }
            return viewModel;
        }
    }
}