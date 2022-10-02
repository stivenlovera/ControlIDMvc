
using ControlIDMvc.Dtos.Inscripcion;
using ControlIDMvc.Dtos.Paquete;
using ControlIDMvc.Dtos.Utils;
using ControlIDMvc.Querys;
using Microsoft.AspNetCore.Mvc;

namespace ControlIDMvc.Controllers
{
    [Route("inscripcion")]
    public class InscripcionController : Controller
    {
        private readonly PaqueteQuery _paqueteQuery;
        private readonly PersonaQuery _personaQuery;
        private readonly InscripcionQuery _inscripcionQuery;

        public InscripcionController(
            PaqueteQuery PaqueteQuery,
            PersonaQuery personaQuery,
            InscripcionQuery inscripcionQuery
        )
        {
            this._paqueteQuery = PaqueteQuery;
            this._personaQuery = personaQuery;
            this._inscripcionQuery = inscripcionQuery;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Inscripcion/Lista.cshtml");
        }

        [HttpPost("data-table")]
        public ActionResult DataTable()
        {
            var dataTable = this._inscripcionQuery.DataTable(Request);
            return Json(dataTable);
        }

        [HttpGet("create")]
        public async Task<ActionResult> Create()
        {
            var paquetes = await this._paqueteQuery.GetAll();
            ViewData["paquetes"] = paquetes;
            var personas = await this._personaQuery.GetAll();
            ViewData["personas"] = personas;
            return View("~/Views/Inscripcion/Create.cshtml");
        }
        [HttpPost("store")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Store(InscripcionCreateDto InscripcionCreateDto)
        {
            if (!ModelState.IsValid)
            {
                System.Console.WriteLine(ModelState.ErrorCount);
                var paquetes = await this._paqueteQuery.GetAll();
                ViewData["paquetes"] = paquetes;
                var personas = await this._personaQuery.GetAll();
                ViewData["personas"] = personas;
                return View("~/Views/Inscripcion/Create.cshtml");
            }
            System.Console.WriteLine("añadiendo");
            var inscripcion = await this._inscripcionQuery.Store(InscripcionCreateDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("select-paquetes")]
        public async Task<List<PaqueteDto>> Select(SelectDto selectDto)
        {
            List<PaqueteDto> paquetes = new List<PaqueteDto>();
            System.Console.WriteLine(String.IsNullOrEmpty(selectDto.searchTerm));
            if (String.IsNullOrEmpty(selectDto.searchTerm))
            {
                paquetes = await this._paqueteQuery.GetAll();
            }
            else
            {
                paquetes = await this._paqueteQuery.GetAllLike(selectDto.searchTerm);

            }
            return paquetes;
        }
    }
}