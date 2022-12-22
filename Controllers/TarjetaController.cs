using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ControlIDMvc.Controllers
{
    [Route("[controller]")]
    public class TarjetaController : Controller
    {
        private readonly ILogger<TarjetaController> _logger;

        public TarjetaController(ILogger<TarjetaController> logger)
        {
            _logger = logger;
        }
        public string demo(){
            return "hola";
        }
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}