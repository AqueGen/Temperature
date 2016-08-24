using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Services.Temperature.Providers;
using Web.Client.ViewModels;

namespace Web.Client.Controllers
{
    public class TemperatureController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> GetTemperature(PeriodViewModel viewModel)
        {
            if (Request.IsAjaxRequest())
            {
                using (var provider = new TemperatureProvider())
                {
                    var items = await provider.GetTemperature(viewModel.ToDTO());
                    var list = items.Select(item => new TemperatureViewModel(item)).ToList();

                    return PartialView(list);
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult PageNotFound()
        {
            return View("Error");
        }
    }
}