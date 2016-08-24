using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Core.Model.Interfaces;
using Services.Temperature.Providers;
using Web.Client.ViewModels;

namespace Web.Client.Controllers
{
    public class TemperatureController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            using (var provider = new TemperatureProvider())
            {
                var devices = await provider.GetDevices();

                IndexViewModel viewModel = new IndexViewModel
                {
                    Filter = {Devices = devices.Select(m => new DeviceViewModel(m)).ToList()}
                };

                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetTemperature(FilterViewModel viewModel)
        {
            if (Request.IsAjaxRequest())
            {
                using (var provider = new TemperatureProvider())
                {
                    var deviceGuid = viewModel.DeviceGuid;
                    var startDateTime = viewModel.Start;
                    var endDateTime = viewModel.End;

                    var temperatures = await provider.GetTemperature(deviceGuid, startDateTime, endDateTime);
                    var list = temperatures.Select(m => new TemperatureViewModel(m)).ToList();

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