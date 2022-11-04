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
        public async Task<ActionResult> Store(ReglaAccesoCreateDto reglaAccesoCreateDto)
        {
            if (ModelState.IsValid)
            {
                //validate nombre
                if (await this._reglaAccesoQuery.ValidarNombre(reglaAccesoCreateDto.Nombre))
                {
                    //regla acceso
                    var reglaAcceso = new ReglaAcceso
                    {
                        Nombre = reglaAccesoCreateDto.Nombre,
                        Descripcion = reglaAccesoCreateDto.Descripcion,
                    };
                    var insert = await this._reglaAccesoQuery.Store(reglaAcceso);
                    //regla acceso y persona
                    var reglaAccesoPersona = new List<PersonaReglasAcceso>();
                    foreach (var persona in reglaAccesoCreateDto.PersonasSelecionadas)
                    {
                        reglaAccesoPersona.Add(
                            new PersonaReglasAcceso
                            {
                                PersonaId = Convert.ToInt32(persona),
                                ReglaAccesoId = insert.Id
                            }
                        );
                    }
                    var reglasPersona = await this._personaReglaAccesoQuery.StoreAll(reglaAccesoPersona);
                    //regla acceso y horario
                    var horarioReglaAccesos = new List<HorarioReglaAcceso>();
                    foreach (var horario in reglaAccesoCreateDto.HorarioSelecionados)
                    {
                        horarioReglaAccesos.Add(
                            new HorarioReglaAcceso
                            {
                                HorarioId = Convert.ToInt32(horario),
                                ReglasAccesoId = insert.Id
                            }
                        );
                    }
                    var reglasHorario = await this._horarioReglaAccesoQuery.StoreAll(horarioReglaAccesos);
                    //regla acceso y area
                    var AreaReglaAccesos = new List<AreaReglaAcceso>();
                    foreach (var area in reglaAccesoCreateDto.AreaSelecionadas)
                    {
                        AreaReglaAccesos.Add(
                            new AreaReglaAcceso
                            {
                                AreaId = Convert.ToInt32(area),
                                ReglaAccesoId = insert.Id
                            }
                        );
                    }
                    var reglasArea = await this._areaReglaAccesoQuery.storeAll(AreaReglaAccesos);
                }
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/ReglasAcceso/Create.cshtml");
        }
        [HttpGet("edit/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            var reglasAcceso = await this._reglaAccesoQuery.GetOne(id);
            //await this._reglaAccesoQuery.ValidarNombre(reglaAccesoDto.Nombre);
            var edit = new ReglaAccesoDto
            {
                Id = reglasAcceso.Id,
                Nombre = reglasAcceso.Nombre,
                Descripcion = reglasAcceso.Descripcion,
                personasOcupadas = await this._reglaAccesoQuery.GetOcupadasPersonaReglaAccesoId(id),
                horariosOcupadas = await this._reglaAccesoQuery.GetOcupadasHorarioReglaAccesoId(id),
                areasOcupadas = await this._reglaAccesoQuery.GetOcupadasAreaReglaAccesoId(id),
                personasDisponibles = await this._reglaAccesoQuery.GetDisponiblePersonaReglaAccesoId(id),
                horariosDisponibles = await this._reglaAccesoQuery.GetDisponibleHorarioReglaAccesoId(id),
                areasDisponibles = await this._reglaAccesoQuery.GetDisponibleAreaReglaAccesoId(id)
            };
            return View("~/Views/ReglasAcceso/Edit.cshtml", edit);
        }
        [HttpPost("update/{id:int}")]
        public async Task<ActionResult> Update(int id, ReglaAccesoCreateDto reglaAccesoCreateDto)
        {
            await this._reglaAccesoQuery.ValidarNombre(reglaAccesoCreateDto.Nombre);
            return View("~/Views/ReglasAcceso/Edit.cshtml");
        }
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await this._reglaAccesoQuery.ValidarNombre("demo");
            return View("~/Views/ReglasAcceso/Edit.cshtml");
        }
    }
}