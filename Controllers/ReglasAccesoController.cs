using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using ControlIDMvc.Dtos.ReglaAcceso;
using ControlIDMvc.ServicesCI.QueryCI;
using ControlIDMvc.ServicesCI.Dtos.user_access_rules;
using ControlIDMvc.ServicesCI;
using Newtonsoft.Json;
using ControlIDMvc.ServicesCI.Dtos.access_rulesDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using ControlIDMvc.Querys;
using ControlIDMvc.ServicesCI.Dtos.time_zones_access_rulesDto;
using ControlIDMvc.Entities;
using ControlIDMvc.Dtos.PersonaReglasAcceso;
using ControlIDMvc.Dtos.Horario;
using ControlIDMvc.Dtos.HorarioReglaAcceso;
using ControlIDMvc.Dtos.AreaReglaAccesoCreateDto;
using ControlIDMvc.ServicesCI.portalsAccessRulesDto;
using ControlIDMvc.Dtos.PortalReglaAcceso;

namespace ControlIDMvc.Controllers
{
    [Route("regla-acceso")]
    public class ReglasAccesoController : Controller
    {

        /* propiedades */
        public string controlador = "192.168.88.129";
        public string user = "admin";
        public string password = "admin";
        public int port { get; set; }
        private readonly DBContext _dbContext;
        private readonly LoginControlIdQuery _loginControlIdQuery;
        private readonly UsuarioRulesAccessControlIdQuery _usuarioRulesAccessControlIdQuery;
        private readonly AccessRulesControlIdQuery _accessRulesControlIdQuery;
        private readonly HttpClientService _httpClientService;
        private readonly PersonaQuery _personaQuery;
        private readonly HorarioReglaAccesoQuery _horarioReglaAccesoQuery;
        private readonly PersonaReglaAccesoQuery _personaReglaAccesoQuery;
        private readonly ReglaAccesoQuery _reglaAccesoQuery;
        private readonly HorarioQuery _horarioQuery;
        private readonly HorarioAccessRulesControlIdQuery _horarioAccessRulesControlIdQuery;
        private readonly AreaReglasAccesoQuery _areaReglaAccesoQuery;
        private readonly PortalQuery _portalQuery;
        private readonly PortalReglasAccesoQuery _portalReglasAccesoQuery;
        private readonly PortalsAccessRulesControlIdQuery _portalsAccessRulesControlIdQuery;
        private readonly AreaQuery _areaQuery;
        ApiRutas _apiRutas;

        public ReglasAccesoController(
            DBContext dbContext,
             HttpClientService httpClientService,
            /*SISTEMA*/
            PersonaQuery personaQuery,
            HorarioReglaAccesoQuery horarioReglaAccesoQuery,
            PersonaReglaAccesoQuery personaReglaAccesoQuery,
            ReglaAccesoQuery reglaAccesoQuery,
            HorarioQuery horarioQuery,
            AreaReglasAccesoQuery areaReglaAccesoQuery,
            PortalQuery portalQuery,
            PortalReglasAccesoQuery portalReglasAccesoQuery,
            /*API*/
            LoginControlIdQuery loginControlIdQuery,
            UsuarioRulesAccessControlIdQuery UsuarioReglasAccesoControlIdQuery,
            AccessRulesControlIdQuery accessRulesControlIdQuery,
            HorarioAccessRulesControlIdQuery HorarioAccessRulesControlIdQuery,
            PortalsAccessRulesControlIdQuery PortalsAccessRulesControlIdQuery,
            AreaQuery areaQuery

        )
        {
            this._dbContext = dbContext;
            this._loginControlIdQuery = loginControlIdQuery;
            this._usuarioRulesAccessControlIdQuery = UsuarioReglasAccesoControlIdQuery;
            this._accessRulesControlIdQuery = accessRulesControlIdQuery;
            this._httpClientService = httpClientService;
            this._personaQuery = personaQuery;
            this._horarioReglaAccesoQuery = horarioReglaAccesoQuery;
            this._personaReglaAccesoQuery = personaReglaAccesoQuery;
            this._reglaAccesoQuery = reglaAccesoQuery;
            this._horarioQuery = horarioQuery;
            this._horarioAccessRulesControlIdQuery = HorarioAccessRulesControlIdQuery;
            this._areaReglaAccesoQuery = areaReglaAccesoQuery;
            this._portalQuery = portalQuery;
            this._portalReglasAccesoQuery = portalReglasAccesoQuery;
            this._portalsAccessRulesControlIdQuery = PortalsAccessRulesControlIdQuery;
            this._areaQuery = areaQuery;

            this._apiRutas = new ApiRutas();
        }
        private async Task<Boolean> loginControlId()
        {
            BodyLogin cuerpo = _loginControlIdQuery.Login(this.user, this.password);
            Response login = await this._httpClientService.LoginRun(this.controlador,this.port, this._apiRutas.ApiUrlLogin, cuerpo,"");
            return login.estado;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/ReglasAcceso/Lista.cshtml");
        }

        [HttpPost("data-table")]
        public async Task<ActionResult> DataTable()
        {
            var dataTable = await this._reglaAccesoQuery.DataTable(Request);
            return Json(dataTable);
        }

        [HttpGet("create")]
        public ActionResult Create()
        {
            return View("~/Views/ReglasAcceso/Create.cshtml");
        }

        [HttpPost("store")]
        public ActionResult Store(ReglaAccesoCreateDto reglaAccesoCreateDto)
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/ReglasAcceso/Create.cshtml");
        }
    }
}