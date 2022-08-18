using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ControlIDMvc.Models;
using Microsoft.EntityFrameworkCore;
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
    [HttpGet]
    public IActionResult datatable()
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var customerData = (from tempcustomer in this._dbContext.Persona select tempcustomer);
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {

            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(m => m.nombre.Contains(searchValue)
                                            || m.apellido.Contains(searchValue)
                                            || m.celular.Contains(searchValue)
                                            || m.email.Contains(searchValue));
            }
            recordsTotal = customerData.Count();
            var data = customerData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return Ok(jsonData);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
            throw;
        }
    }
}
