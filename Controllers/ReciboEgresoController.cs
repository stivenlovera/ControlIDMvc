using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ControlIDMvc.Controllers
{
    [Route("Recibo-Egreso")]
    public class ReciboEgresoController : Controller
    {
        private readonly ILogger<ReciboEgresoController> _logger;

        public ReciboEgresoController(ILogger<ReciboEgresoController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/ReciboEgresos/Recibo.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}