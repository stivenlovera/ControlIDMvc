using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ControlIDMvc.Models.DatatableModel;
using ControlIDMvc.Entities;
using ControlIDMvc.Models.ModelForm;

namespace ControlIDMvc.Controllers
{
    [Route("horario")]
    public class HorarioController : Controller
    {
        private readonly DBContext _dbContext;
        public HorarioController(DBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet("")]
        public ActionResult Index()
        {
            return View("~/Views/Horario/Lista.cshtml");
        }
        [HttpGet("create")]
        public ActionResult Create()
        {
            return View("~/Views/Horario/Create.cshtml");
        }
        [HttpPost("store")]
        [ValidateAntiForgeryToken]
        public ActionResult Store([FromForm] HorarioForm horarioForm)
        {
            System.Console.WriteLine("test");
            var request = horarioForm.hora_fin;
            List<Dia> dia = new List<Dia>();
            for (int i = 0; i < horarioForm.dia.Count; i++)
            {
                dia.Add(
                    new Dia()
                    {
                        nombre = horarioForm.dia[i],
                        hora_fin = horarioForm.hora_fin[i],
                        hora_inicio = horarioForm.hora_inicio[i],
                    });
            }
            Horario horario = new Horario();
            horario.nombre = horarioForm.nombre;
            horario.descripcion = horarioForm.descripcion;
            horario.dias = dia;
            _dbContext.Add(horario);
            _dbContext.SaveChanges();
            System.Console.WriteLine(_dbContext.SaveChanges());
            return RedirectToAction(nameof(Index));
        }
        [HttpGet("editar/{id:int}")]
        public ActionResult Edit(int id)
        {
            var persona = _dbContext.Persona.Find(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View("~/Views/Persona/Edit.cshtml", persona);
        }
    }
}