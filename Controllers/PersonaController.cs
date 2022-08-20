
using Microsoft.AspNetCore.Mvc;
using ControlIDMvc.Models.DatatableModel;
using Microsoft.EntityFrameworkCore;
using ControlIDMvc.Entities;

namespace ControlIDMvc.Controllers;

[Route("persona")]
public class PersonaController : Controller
{
     private readonly ILogger<HomeController> _logger;

    private readonly DBContext _dbContext;
    public PersonaController(ILogger<HomeController> logger,DBContext dbContext)
    {
        this._dbContext = dbContext;
        this._logger=logger;
    }

    [HttpGet("")]
    public ActionResult Index()
    {
        var personas = this._dbContext.Persona.ToList();
        foreach (var persona in personas)
        {
          
        }
        return View("~/Views/Persona/Lista.cshtml");
    }
    [HttpGet("/create")]
    public ActionResult Create()
    {
        return View("~/Views/Persona/Create.cshtml");
    }
    // POST: HomeController1/Create
    [HttpPost]
     [HttpGet("store")]
    public ActionResult Post(IFormFile postedFile)
    {

        Persona persona = new Persona();
        persona.nombre = Request.Form["nombre"];
        persona.ci = Request.Form["ci"];
        persona.apellido = Request.Form["apellido"];
        persona.usuario = Request.Form["usuario"];
        persona.celular = Request.Form["celular"];
        persona.email = Request.Form["email"];
        persona.contraseña = Request.Form["contraseña"];
        persona.dirrecion = Request.Form["observaciones"];
        persona.observaciones = Request.Form["observaciones"];
        _dbContext.Persona.Add(persona);
        _dbContext.SaveChanges();
        System.Console.WriteLine(_dbContext.SaveChanges());
        return RedirectToAction(nameof(Index));

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
    public ActionResult Json()
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

        List<PersonaModel> personas = new List<PersonaModel>();
        using (_dbContext)
        {
            /*  var draw=Request.Form.GET*/
            personas = (from d in _dbContext.Persona
                        select new PersonaModel
                        {
                            id = d.id,
                            ci = d.ci,
                            nombre = d.nombre,
                            apellido = d.apellido,
                            celular = d.celular,
                            observaciones = d.observaciones
                        }).ToList();
            recordsTotal = personas.Count();
            personas = personas.Skip(skip).Take(pageSize).ToList();
            System.Console.WriteLine($"el total de registros es : {personas.Count()}");
        }
        return Json(new
        {
            draw = draw,
            recordsFiltered = recordsTotal,
            recordsTotal = recordsTotal,
            data = personas
        });
    }
    [HttpGet("editar/{id:int}")]
    public ActionResult Edit(int id)
    {
        var persona = _dbContext.Persona.Find(id);
        if (persona==null)
        {
            return NotFound();
        }

        return View("~/Views/Persona/Edit.cshtml",persona);
    }
}

