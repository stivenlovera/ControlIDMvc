using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Dtos.Paquete;
using ControlIDMvc.Querys;
using Microsoft.AspNetCore.Mvc;

namespace ControlIDMvc.Controllers
{
    [Route("paquete")]
    public class PaqueteController : Controller
    {
        private readonly PaqueteQuery _paqueteQuery;

        public PaqueteController(
            PaqueteQuery PaqueteQuery
        )
        {
            this._paqueteQuery = PaqueteQuery;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Paquetes/Lista.cshtml");
        }

        [HttpPost("data-table")]
        public ActionResult DataTable()
        {
            var dataTable = this._paqueteQuery.DataTable(Request);
            return Json(dataTable);
        }

        [HttpGet("create")]
        public ActionResult Create()
        {

            return View("~/Views/Paquetes/Create.cshtml");
        }

        [HttpPost("store")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Store(PaqueteCreateDto paqueteCreateDto)
        {
            if (!ModelState.IsValid)
            {
                System.Console.WriteLine(ModelState.ErrorCount);
                return View("~/Views/Paquetes/Create.cshtml");
            }
            System.Console.WriteLine("añadiendo");
            var inscripcion = await this._paqueteQuery.Store(paqueteCreateDto);
            return RedirectToAction(nameof(Index));
        }
    }
}