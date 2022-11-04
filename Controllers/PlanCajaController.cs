using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ControlIDMvc.Controllers
{
    [Route("planes-caja")]
    public class PlanCajaController : Controller
    {
        private readonly ILogger<PlanCajaController> _logger;

        public PlanCajaController(ILogger<PlanCajaController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/PlanesCaja/Lista.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        
    }
}