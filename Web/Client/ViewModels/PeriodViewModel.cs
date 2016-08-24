using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Web.Mvc;
using Services.Temperature.DTO;

namespace Web.Client.ViewModels
{
    [ModelBinder(typeof(PeriodModelBinder))]
    public class PeriodViewModel
    {
        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public PeriodDTO ToDTO()
        {
            return new PeriodDTO
            {
                Start = Start,
                End = End
            };
        }
    }

    public class PeriodModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            NameValueCollection request = controllerContext.HttpContext.Request.HttpMethod == "GET"
                ? controllerContext.HttpContext.Request.QueryString
                : controllerContext.HttpContext.Request.Form;

            PeriodViewModel viewModel = new PeriodViewModel();

            var start = request.Get(nameof(viewModel.Start));
            var end = request.Get(nameof(viewModel.End));

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