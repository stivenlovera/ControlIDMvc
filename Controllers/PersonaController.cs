
using Microsoft.AspNetCore.Mvc;
using ControlIDMvc.Models.DatatableModel;
using Microsoft.EntityFrameworkCore;
using ControlIDMvc.Entities;
using Microsoft.AspNetCore.Cors;
using ControlIDMvc.Services;
using ControlIDMvc.Querys;
using ControlIDMvc.Services.QueryControlId;

namespace ControlIDMvc.Controllers;

[Route("persona")]
public class PersonaController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClientService _httpClientService;
    private PersonaQuery _personaQuery;
    private readonly LoginControlId _LoginControlId;

    public PersonaController(
        ILogger<HomeController> logger,
        HttpClientService httpClientService,
        PersonaQuery personaQuery,
        LoginControlId loginControlId
        )
    {
        this._httpClientService = httpClientService;
        this._personaQuery = personaQuery;
        this._LoginControlId = loginControlId;
        this._logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var personas = await this._personaQuery.GetAll();
        return View("~/Views/Persona/Lista.cshtml");
    }

    [HttpGet("create")]
    public ActionResult Create()
    {
        return View("~/Views/Persona/Create.cshtml");
    }

    [HttpPost("store")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Post(Persona PersonaCreate)
    {
        var personas = await this._personaQuery.Store(PersonaCreate);
        /*proveedor  controlador */
        string controlador = "192.168.88.129";
        string uri = "login.fcgi";

        object cuerpo = _LoginControlId.Login("admin", "admin");
        await this._httpClientService.Run(controlador, uri, cuerpo);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("data-table")]
    public ActionResult DataTable()
    {
        var dataTable = this._personaQuery.DataTable(Request);
        return Json(dataTable);
    }

    [HttpGet("editar/{id:int}")]
    public async Task<ActionResult> Edit(int id)
    {
        var persona = await this._personaQuery.GetOne(id);
        if (persona == null)
        {
            return NotFound();
        }
        return View("~/Views/Persona/Edit.cshtml", persona);
    }
}

