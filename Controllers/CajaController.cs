using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Querys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ControlIDMvc.Controllers
{
    [Authorize]
    [Route("caja")]
    public class CajaController : Controller
    {
        private readonly ILogger<CajaController> _logger;
        private readonly CajaQuery _cajaQuery;
        private readonly UsuarioQuery _usuarioQuery;

        public CajaController(
            ILogger<CajaController> logger,
            CajaQuery cajaQuery,
            UsuarioQuery usuarioQuery
        )
        {
            _logger = logger;
            this._cajaQuery = cajaQuery;
            this._usuarioQuery = usuarioQuery;
        }

        [HttpPost("data-table")]
        public ActionResult DataTable()
        {
            var dataTable = this._cajaQuery.DataTable(Request);
            return Json(dataTable);
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var usuarios = await this._usuarioQuery.GetAll();
            ViewData["usuarios"] = usuarios;
            return View("~/Views/Caja/Index.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}