
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
        public ActionResult Index(string message)
        {
            ViewData["message"] = message;
            return View("~/Views/Inscripcion/Lista.cshtml");
        }

        [HttpPost("data-table")]
        public ActionResult DataTable()
        {
            var dataTable = this._inscripcionQuery.DataTable(Request);
            return Json(dataTable);
        }

        [HttpGet("factura-recibo/{id:int}")]
        public ActionResult PreviewRecibo(int id)
        {
            return View("~/Views/Inscripcion/PriviewInscripcion.cshtml");
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
            var inscripcion = await this._inscripcionQuery.Store(InscripcionCreateDto);
            return RedirectToAction("PreviewRecibo", new { id = 1 });
        }

        [HttpPost("store-recibo")]
        [ValidateAntiForgeryToken]
        public ActionResult StoreInscripcion()
        {
            return RedirectToAction("Index", new { message = "Nueva inscripcion registrada" });
        }

        [HttpGet("show/{id:int}")]
        public async Task<ActionResult> Show(int id)
        {
            var inscripcion = await this._inscripcionQuery.Edit(id);
            return View("~/Views/Inscripcion/Create.cshtml");
        }

        [HttpGet("edit/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            var paquetes = await this._paqueteQuery.GetAll();
            ViewData["paquetes"] = paquetes;
            var personas = await this._personaQuery.GetAll();
            ViewData["personas"] = personas;
            var inscripcion = await this._inscripcionQuery.Edit(id);
            InscripcionUpdateDto inscripcionUpdateDto = new InscripcionUpdateDto
            {
                Id=inscripcion.Id,
                Nombres = inscripcion.Persona.Nombre,
                Apellidos = inscripcion.Persona.Apellido,
                CI = inscripcion.Persona.Ci,
                Costo = inscripcion.Paquete.Costo,
                Dias = inscripcion.Paquete.Dias,
                FechaCreacion = inscripcion.FechaCreacion,
                FechaFin = inscripcion.FechaFin,
                FechaInicio = inscripcion.FechaInicio,
                PaqueteId = inscripcion.PaqueteId,
                PaqueteNombre = inscripcion.Paquete.Nombre,
                PersonaId = inscripcion.PersonaId
            };
            return View("~/Views/Inscripcion/Edit.cshtml", inscripcionUpdateDto);
        }

        [HttpPost("update/{id:int}")]
        public async Task<ActionResult> Update(int id, InscripcionCreateDto inscripcionCreateDto)
        {
            var inscripcion = await this._inscripcionQuery.Update(inscripcionCreateDto,id);
            return RedirectToAction("PreviewRecibo", new { id = 1 });
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> Destroy(int id)
        {
            var inscripcion = await this._inscripcionQuery.Delete(id);
            if (inscripcion)
            {
                return Json(
                    new
                    {
                        status = "success",
                        message = "Eliminado correctamente",
                    }
                );
            }
            else
            {
                return Json(
                    new
                    {
                        status = "error",
                        message = "ocurrio un error",
                    }
                );
            }
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