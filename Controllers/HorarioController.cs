using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ControlIDMvc.Models.DatatableModel;
using ControlIDMvc.Entities;
using ControlIDMvc.Models.ModelForm;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Horario/Lista.cshtml");
        }
        [HttpGet("create")]
        public ActionResult Create()
        {
            return View("~/Views/Horario/Create.cshtml");
        }

        /* propiedades */
        public string draw;
        public string start;
        public string length;
        public string showColumn;
        public string showColumnDir;
        public string searchValue;
        public int pageSize, skip, recordsTotal;
        [HttpPost("data-table")]
        public async Task<ActionResult<object>> Datatable()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumna = Request.Form["column[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnaDir = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            pageSize = length != null ? Convert.ToInt32(length) : 0;
            skip = start != null ? Convert.ToInt32(start) : 0;
            recordsTotal = 0;

            List<DatatableHorario> horarios = new List<DatatableHorario>();
            var horarios_aux = await _dbContext.Horario.Include(h => h.Dias).ToListAsync();
            foreach (var item in horarios_aux)
            {
                DatatableHorario horario = new DatatableHorario();
                horario.nombre = item.Nombre;
                horario.id = item.Id;
                foreach (var dia in item.Dias)
                {
                    switch (dia.Nombre)
                    {
                        case "lunes":
                            horario.lunes = $"{dia.HoraInicio.ToString("HH:mm")} - {dia.HoraFin.ToString("HH:mm")}";
                            break;
                        case "martes":
                            horario.martes = $"{dia.HoraInicio.ToString("HH:mm")} - {dia.HoraFin.ToString("HH:mm")}";
                            break;
                        case "miercoles":
                            horario.miercoles = $"{dia.HoraInicio.ToString("HH:mm")} - {dia.HoraFin.ToString("HH:mm")}";
                            break;
                        case "jueves":
                            horario.jueves = $"{dia.HoraInicio.ToString("HH:mm")} - {dia.HoraFin.ToString("HH:mm")}";
                            break;
                        case "viernes":
                            horario.viernes = $"{dia.HoraInicio.ToString("HH:mm")} - {dia.HoraFin.ToString("HH:mm")}";
                            break;
                        case "sabado":
                            horario.sabado = $"{dia.HoraInicio.ToString("HH:mm")} - {dia.HoraFin.ToString("HH:mm")}";
                            break;
                        case "domingo":
                            horario.domingo = $"{dia.HoraInicio.ToString("HH:mm")} - {dia.HoraFin.ToString("HH:mm")}";
                            break;
                        default:

                            break;
                    }
                    horarios.Add(horario);
                }
            }

            /* using (_dbContext)
            {
                
                horarios = (from d in _dbContext.Horario
                            select new DatatableHorario
                            {
                                id = d.id,
                                nombre = d.nombre,
                                lunes = "00:00 - 00:00",
                                martes = "00:00 - 00:00",
                                miercoles = "00:00 - 00:00",
                                jueves = "00:00 - 00:00",
                                viernes = "00:00 - 00:00",
                                sabado = "00:00 - 00:00",
                                domingo = "00:00 - 00:00"
                            }).ToList();
                recordsTotal = horarios.Count();
                horarios = horarios.Skip(skip).Take(pageSize).ToList();
                System.Console.WriteLine($"el total de registros es : {horarios.Count()}");
            } */
            recordsTotal = horarios.Count();
            horarios = horarios.Skip(skip).Take(pageSize).ToList();
            return Json(new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = horarios
            });
        }
        [HttpPost("store")]
        [ValidateAntiForgeryToken]
        public ActionResult Store([FromForm] HorarioForm horarioForm)
        {
            var request = horarioForm.hora_fin;
            List<Dia> dia = new List<Dia>();
            for (int i = 0; i < horarioForm.dia.Count; i++)
            {
                dia.Add(
                    new Dia()
                    {
                        Nombre = horarioForm.dia[i],
                        HoraFin = horarioForm.hora_fin[i],
                        HoraInicio = horarioForm.hora_inicio[i],
                    });
            }
            Horario horario = new Horario();
            horario.Nombre = horarioForm.nombre;
            horario.Descripcion = horarioForm.descripcion;
            horario.Dias = dia;
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