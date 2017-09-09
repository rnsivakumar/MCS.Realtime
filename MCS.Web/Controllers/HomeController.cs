using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MCS.Web.Models;
using MCS.Web.Core;
using MCS.Infrastructure;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MCS.Web.ViewModels;

namespace MCS.Web.Controllers
{
   
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;
        public HomeController(IUnitOfWork unitOfWork, ILogger<HomeController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public IActionResult Index()
        {
            MCSConfig.siteUrl = $"{Request.Scheme}://{Request.Host}";
            _logger.LogCritical("Loading the Devices Info");
            MCSConfig.lstDevice = Mapper.Map<IEnumerable<DeviceInfo>>( _unitOfWork.EventHistory.GetDevicesInfo().ToList()).ToList();
            _logger.LogCritical($"Total Number of Devices : {MCSConfig.lstDevice.Count}");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
