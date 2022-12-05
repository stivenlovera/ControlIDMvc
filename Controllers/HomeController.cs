using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ControlIDMvc.Models;
using Microsoft.AspNetCore.Authorization;
using ControlIDMvc.Querys;
using ControlIDMvc.Dtos.HomeDto;
using System.Security.Claims;
using ControlIDMvc.Entities;

namespace ControlIDMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly PlanCuentaSubCuentaQuery _planCuentaSubCuentaQuery;
    private readonly MovimientoAsientoQuery _movimientoAsientoQuery;
    private readonly MetodoPagoQuery _metodoPagoQuery;
    private readonly InscripcionQuery _inscripcionQuery;
    private readonly PlanAsientoQuery _planAsientoQuery;

    public HomeController(
        ILogger<HomeController> logger,
        PlanCuentaSubCuentaQuery planCuentaSubCuentaQuery,
        MovimientoAsientoQuery movimientoAsientoQuery,
        MetodoPagoQuery metodoPagoQuery,
        InscripcionQuery inscripcionQuery,
        PlanAsientoQuery planAsientoQuery
        )
    {
        _logger = logger;
        this._planCuentaSubCuentaQuery = planCuentaSubCuentaQuery;
        this._movimientoAsientoQuery = movimientoAsientoQuery;
        this._metodoPagoQuery = metodoPagoQuery;
        this._inscripcionQuery = inscripcionQuery;
        this._planAsientoQuery = planAsientoQuery;
    }

    [Authorize]
    public async Task<IActionResult> Index(HomeDto homeDto)
    {
        var auth = HttpContext.User.Claims.Where(x => x.Type == "Role").ToList();
        if (User.IsInRole("Admin"))
        {
            homeDto = await this.PlanAsientoByAdmin(homeDto);
        }
        else
        {
            homeDto = await this.PlanAsientoByUsuario(homeDto);
        }
        return View(homeDto);
    }
    private Totales SearchPerPlanId(List<PlanAsiento> PlanAsientos, string PlanId, string PlanCuenta)
    {
        PlanAsientos = PlanAsientos.Where(p => p.PlanId == PlanId).ToList();
        decimal haberTotal = 0.00m;
        foreach (var PlanAsiento in PlanAsientos)
        {
            haberTotal += PlanAsiento.Haber;
        }
        var result = new Totales
        {
            total = haberTotal,
            PlanId = PlanId,
            Nombre = PlanCuenta,

        };
        return result;
    }
    public async Task<HomeDto> PlanAsientoByUsuario(HomeDto homeDto)
    {
        var buscarMovimientos = await this._planAsientoQuery.PlanAsientosByPersonaId(Convert.ToInt32(User.FindFirst("UsuarioId").Value));
        var buscarIncripciones = await this._inscripcionQuery.GetAllByPersonaIPerDay(Convert.ToInt32(User.FindFirst("UsuarioId").Value));
        var buscarhabilitados = await this._inscripcionQuery.GetAllByPersonaIPerDay(Convert.ToInt32(User.FindFirst("UsuarioId").Value));
        var plan1 = this.SearchPerPlanId(buscarMovimientos, "11101M01", "Efectivo");
        var plan2 = this.SearchPerPlanId(buscarMovimientos, "11101M02", "Bancos");
        var plan3 = this.SearchPerPlanId(buscarMovimientos, "11301M01", "Cuentas por Cobrar Comerciales");
        homeDto.Totales = new List<Totales>();
        homeDto.Totales.Add(
           plan1
        );
        homeDto.Totales.Add(
          plan2
        );
        homeDto.Totales.Add(
            plan3
        );
        homeDto.Totales.Add(
            new Totales
            {
                Nombre = "Total Nuevas Inscripciones",
                PlanId = "",
                total = buscarIncripciones.Count
            }
        );
        homeDto.Totales.Add(
            new Totales
            {
                Nombre = "Usuarios Activos",
                PlanId = "",
                total = 2
            }
        );
        return homeDto;
    }
    public async Task<HomeDto> PlanAsientoByAdmin(HomeDto homeDto)
    {
        var buscarMovimientos = await this._planAsientoQuery.PlanAsientosAll();
        var buscarIncripciones = await this._inscripcionQuery.GetAllPerDay();
        var buscarhabilitados = await this._inscripcionQuery.GetAllPerDay();
        var plan1 = this.SearchPerPlanId(buscarMovimientos, "11101M01", "Efectivo");
        var plan2 = this.SearchPerPlanId(buscarMovimientos, "11101M02", "Bancos");
        var plan3 = this.SearchPerPlanId(buscarMovimientos, "11301M01", "Cuentas por Cobrar Comerciales");
        homeDto.Totales = new List<Totales>();
        homeDto.Totales.Add(
           plan1
        );
        homeDto.Totales.Add(
          plan2
        );
        homeDto.Totales.Add(
            plan3
        );
        homeDto.Totales.Add(
            new Totales
            {
                Nombre = "Total Nuevas Inscripciones",
                PlanId = "",
                total = buscarIncripciones.Count
            }
        );
        homeDto.Totales.Add(
            new Totales
            {
                Nombre = "Usuarios Activos",
                PlanId = "",
                total = 2
            }
        );
        return homeDto;
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Personas()
    {
        return View();
    }
    [Authorize(Roles = "Admin")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
