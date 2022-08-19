
using Microsoft.AspNetCore.Mvc;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;
using ControlIDMvc.Models.utils;

using Newtonsoft.Json;
using ControlIDMvc.Controllers.extenciones;

namespace ControlIDMvc.Controllers;

public class PersonaController : Controller
{
    private readonly DBContext _dbContext;
    public PersonaController(DBContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public ActionResult Index()
    {
        var personas = _dbContext.Persona.Include(p => p.creado_por)
        .ToList();
        foreach (var persona in personas)
        {
            System.Console.WriteLine($"persona {persona.creado_por.nombre}");
        }
        return View("~/Views/Persona/Lista.cshtml");
    }
    public ActionResult Create()
    {
        /*  var usuario = _dbContext.Usuario.First();
         System.Console.WriteLine("respuesta"+usuario.grupo_id);  */
        return View("~/Views/Persona/Create.cshtml");
    }
    // POST: HomeController1/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormFile postedFile)
    {
        System.Console.WriteLine("store");
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
    [HttpPost]
    public async Task<IActionResult> Datatable([FromBody] DtParameters dtParameters)
    {
        var searchBy = dtParameters.Search?.Value;

        // if we have an empty search then just order the results by Id ascending
        var orderCriteria = "Id";
        var orderAscendingDirection = true;

        if (dtParameters.Order != null)
        {
            // in this example we just default sort on the 1st column
            orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
            orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
        }

        var result = _dbContext.Persona.AsQueryable();

        if (!string.IsNullOrEmpty(searchBy))
        {
            result = result.Where(r => r.ci != null && r.ci.ToUpper().Contains(searchBy.ToUpper()) ||
                                       r.nombre != null && r.nombre.ToUpper().Contains(searchBy.ToUpper()) ||
                                       r.apellido != null && r.apellido.ToUpper().Contains(searchBy.ToUpper()) ||
                                       r.celular != null && r.celular.ToUpper().Contains(searchBy.ToUpper()) ||
                                       r.dirrecion != null && r.dirrecion.ToUpper().Contains(searchBy.ToUpper()) ||
                                       r.email != null && r.email.ToUpper().Contains(searchBy.ToUpper()) ||
                                       r.observaciones != null && r.observaciones.ToUpper().Contains(searchBy.ToUpper()));
        }

        result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.OrderByDynamic(orderCriteria, DtOrderDir.Desc);

        // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
        var filteredResultsCount = await result.CountAsync();
        var totalResultsCount = await _dbContext.Persona.CountAsync();

        return Json(new DtResult<Persona>
        {
            Draw = dtParameters.Draw,
            RecordsTotal = totalResultsCount,
            RecordsFiltered = filteredResultsCount,
            Data = (IEnumerable<Persona>)await result
                .Skip(dtParameters.Start)
                .Take(dtParameters.Length)
                .ToListAsync()
        });
    }
}

