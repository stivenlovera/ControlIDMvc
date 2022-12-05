using Microsoft.Extensions.Logging;
using ControlIDMvc.Dtos.Caja;
using ControlIDMvc.Dtos.Egreso;
using ControlIDMvc.Entities;
using ControlIDMvc.Querys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ControlIDMvc.Controllers
{
     [Authorize]
    [Route("gasto")]
    public class GastosController : Controller
    {
        private readonly ILogger<LoginControllers> _logger;
        private readonly EgresoQuery _egresoQuery;
        private readonly CajaQuery _cajaQuery;

        public GastosController(ILogger<LoginControllers> logger, EgresoQuery egresoQuery, CajaQuery cajaQuery)
        {
            this._logger = logger;
            this._egresoQuery = egresoQuery;
            this._cajaQuery = cajaQuery;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Gastos/Lista.cshtml");
        }
        [HttpGet("create")]
        public ActionResult Create()
        {
            ViewData["usuario"] = "stivenlovera";
            ViewData["usuarioId"] = 1;
            DateTime fechaRecibo = DateTime.Now;
            fechaRecibo.ToString("yyyyMMdd");
            ViewData["numeroRecibo"] = fechaRecibo.ToString("MMddHHmmss");
            return View("~/Views/Gastos/Create.cshtml");
        }

        [HttpPost("data-table")]
        public ActionResult DataTable()
        {
            var dataTable = this._egresoQuery.DataTable(Request);
            return Json(dataTable);
        }

        [HttpPost("store")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Store(EgresoCreateDto egresoCreateDto)
        {
            if (!ModelState.IsValid)
            {
                ViewData["usuario"] = "stivenlovera";
                ViewData["usuarioId"] = 1;
                DateTime fechaRecibo = DateTime.Now;
                ViewData["numeroRecibo"] = fechaRecibo.ToString("MMddHHmmss");
                return View("~/Views/Gastos/Create.cshtml", egresoCreateDto);
            }
            var egreso = await this._egresoQuery.Store(egresoCreateDto);
            await this.addCash(egreso);
            return RedirectToAction("Index", new { message = "Nueva Egreso registrado" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        /*
            *Modelo de negocio
        */
        private async Task<bool> addCash(Egreso egreso)
        {
            var egresoCaja = new CajaCreateDto
            {
                Concepto = egreso.Concepto,
                Fecha = egreso.FechaCreacion,
                NumeroRecibo = egreso.NumeroRecibo,
                Persona = egreso.Persona,
                Tipo = "egreso",
                Valor = egreso.Monto
            };
            var caja = await this._cajaQuery.Store(egresoCaja);
            if (caja != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}