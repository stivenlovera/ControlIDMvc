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
            Response login = await this._httpClientService.LoginRun(this.controlador, this._apiRutas.ApiUrlLogin, cuerpo);
            this._httpClientService.session = login.data;
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
        public async Task<ActionResult> Store(ReglaAccesoCreateDto reglaAccesoCreateDto)
        {
            if (ModelState.IsValid)
            {
                if (await this.loginControlId())
                {
                    reglaAccesoCreateDto.ControlIdPriority = 0;
                    reglaAccesoCreateDto.ControlIdType = 1;
                    /*registrar regla acceso*/
                    var responseAccessRules = await this.SaveAccessRulesControlId(reglaAccesoCreateDto.Nombre, reglaAccesoCreateDto.ControlIdType, reglaAccesoCreateDto.ControlIdPriority);
                    if (responseAccessRules.estado)
                    {
                        responseApiAccessRulesCreateDto responseApiAccessRules = JsonConvert.DeserializeObject<responseApiAccessRulesCreateDto>(responseAccessRules.data);
                        /*save rules acceso*/
                        reglaAccesoCreateDto.ControlId = responseApiAccessRules.ids[0].ToString();
                        var new_reglaAcceso = await this._reglaAccesoQuery.Store(reglaAccesoCreateDto);

                        /*registrar usuario rules accesso*/
                        var usuarios = await this._personaQuery.GetAllByID(reglaAccesoCreateDto.PersonasSelecionadas.Select(int.Parse).ToList());//conversion 
                        var responseUserAccessRules = await this.SaveUsersAccessRulesControlId(usuarios, responseApiAccessRules.ids[0]);
                        if (responseUserAccessRules.estado)
                        {
                            responseUserAccessRulesCreateDto responseApiUserAccessRules = JsonConvert.DeserializeObject<responseUserAccessRulesCreateDto>(responseUserAccessRules.data);
                            int i = 0;
                            foreach (var id in responseApiUserAccessRules.ids)
                            {
                                PersonaAccesoReglasCreateDto personaAccesoReglasCreateDto = new PersonaAccesoReglasCreateDto()
                                {
                                    ControlIdAccessRulesId = responseApiAccessRules.ids[0],
                                    ControlIdUserId = Convert.ToInt32(usuarios[i].ControlId),
                                    PersonaId = usuarios[i].Id,
                                    ReglaAccesoId = new_reglaAcceso.Id
                                };
                                await this._personaReglaAccesoQuery.Store(personaAccesoReglasCreateDto);
                                i++;
                            }
                            //responseApiUserAccessRules.ids;
                        }

                        /*registrar horarios*/
                        var listHorarios = await this._horarioQuery.GetAllByID(reglaAccesoCreateDto.HorarioSelecionados.Select(int.Parse).ToList());//conversion 
                        var responseHorarioAccessRules = await this.SaveHorarioAccessRulesControlId(listHorarios, responseApiAccessRules.ids[0]);
                        if (responseHorarioAccessRules.estado)
                        {
                            responseUserAccessRulesCreateDto responseApiHorarioAccessRule = JsonConvert.DeserializeObject<responseUserAccessRulesCreateDto>(responseHorarioAccessRules.data);
                            //responseApiUserAccessRules.ids;
                            int i = 0;
                            foreach (var id in responseApiHorarioAccessRule.ids)
                            {
                                HorarioReglaAccesoCreateDto horarioCreateDto = new HorarioReglaAccesoCreateDto()
                                {
                                    ControlIdAccessRulesId = responseApiAccessRules.ids[0],
                                    ControlIdTimeZoneId = Convert.ToInt32(listHorarios[i].ControlId),
                                    HorarioId = listHorarios[i].Id,
                                    ReglasAccesoId = new_reglaAcceso.Id
                                };
                                await this._horarioReglaAccesoQuery.Store(horarioCreateDto);
                                i++;
                            }
                        }
                        /*registrar area*/
                        var listArea = await this._areaQuery.GetAllByID(reglaAccesoCreateDto.AreaSelecionadas.Select(int.Parse).ToList());//conversion 
                        var responseAreaAccessRules = await this.SaveAreaAccessRulesControlId(reglaAccesoCreateDto.HorarioSelecionados, responseApiAccessRules.ids[0]);
                        if (responseHorarioAccessRules.estado)
                        {
                            responseUserAccessRulesCreateDto responseApiHorarioAccessRule = JsonConvert.DeserializeObject<responseUserAccessRulesCreateDto>(responseHorarioAccessRules.data);
                            //responseApiUserAccessRules.ids;
                            int i = 0;
                            foreach (var id in responseApiHorarioAccessRule.ids)
                            {
                                AreaReglaAccesoCreateDto areaReglaAccesoCreateDto = new AreaReglaAccesoCreateDto()
                                {
                                    AreaId = listArea[i].Id,
                                    ControlIdAccesoRulesId = responseApiAccessRules.ids[0],
                                    ControlIdAreaId = Convert.ToInt32(listArea[i].ControlId),
                                    ReglaAccesoId = new_reglaAcceso.Id
                                };
                                var area = await this._areaReglaAccesoQuery.store(areaReglaAccesoCreateDto);
                                /*portal*/
                                var listPortal = await this._portalQuery.GetAllArea(area.ControlIdAreaId);//conversion 
                                var responsePortalAccessRules = await this.SavePortalAccessRulesControlId(listPortal, responseApiAccessRules.ids[0]);
                                if (responsePortalAccessRules.estado)
                                {
                                    responseUserAccessRulesCreateDto responseApiportalAccessRule = JsonConvert.DeserializeObject<responseUserAccessRulesCreateDto>(responsePortalAccessRules.data);
                                    int j = 0;
                                    foreach (var aux in responseApiportalAccessRule.ids)
                                    {
                                        PortalReglaAccesoCreateDto horarioCreateDto = new PortalReglaAccesoCreateDto()
                                        {
                                            PortalId = listPortal[j].Id,
                                            ControlIdPortalId = Convert.ToInt32(listHorarios[i].ControlId),
                                            ControlIdRulesId = responseApiAccessRules.ids[0],
                                            ReglaAccesoId = new_reglaAcceso.Id
                                        };
                                        await this._portalReglasAccesoQuery.store(horarioCreateDto);
                                        j++;
                                    }
                                }

                                i++;
                            }
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/ReglasAcceso/Create.cshtml");
        }

        private async Task<Response> SaveAreaAccessRulesControlId(List<string> area, int regla_acceso_id)
        {
            List<time_zones_access_rulesDto> usuarioAcceso = new List<time_zones_access_rulesDto>();
            foreach (var horario in area)
            {
                usuarioAcceso.Add(
                    new time_zones_access_rulesDto()
                    {
                        access_rule_id = regla_acceso_id,
                        time_zone_id = Convert.ToInt32(horario)
                    }
                );
            }

            var AddUsuarioReglaAcceso = this._horarioAccessRulesControlIdQuery.CreateTimeZonesAccessRules(usuarioAcceso);
            Response responseAddHorario = await this._httpClientService.Run(controlador, this._apiRutas.ApiUrlCreate, AddUsuarioReglaAcceso);
            return responseAddHorario;
        }

        private async Task<Response> SaveHorarioAccessRulesControlId(List<Horario> horarios, int regla_acceso_id)
        {
            List<time_zones_access_rulesDto> usuarioAcceso = new List<time_zones_access_rulesDto>();

            foreach (var horario in horarios)
            {
                usuarioAcceso.Add(
                    new time_zones_access_rulesDto()
                    {
                        access_rule_id = regla_acceso_id,//añadir controlid id de controlador
                        time_zone_id = Convert.ToInt32(horario.ControlId)
                    }
                );
            }

            var AddUsuarioReglaAcceso = this._horarioAccessRulesControlIdQuery.CreateTimeZonesAccessRules(usuarioAcceso);
            Response responseAddHorario = await this._httpClientService.Run(controlador, this._apiRutas.ApiUrlCreate, AddUsuarioReglaAcceso);
            return responseAddHorario;
        }

        private async Task<Response> SaveUsersAccessRulesControlId(List<Persona> personas, int regla_acceso_id)
        {
            List<userAccessRulesCreateDto> usuarioAcceso = new List<userAccessRulesCreateDto>();
            foreach (var persona in personas)
            {
                usuarioAcceso.Add(
                    new userAccessRulesCreateDto()
                    {
                        access_rule_id = regla_acceso_id,//añadir controlid id de controlador
                        user_id = Convert.ToInt32(persona.ControlId)
                    }
                );
            }

            var AddUsuarioReglaAcceso = this._usuarioRulesAccessControlIdQuery.CreateUserReglaAcceso(usuarioAcceso);
            Response responseAddHorario = await this._httpClientService.Run(controlador, this._apiRutas.ApiUrlCreate, AddUsuarioReglaAcceso);
            return responseAddHorario;
        }

        private async Task<Response> SaveAccessRulesControlId(string name, int type, int priority)
        {
            List<accessRulesCreateDto> accessRules = new List<accessRulesCreateDto>();
            accessRules.Add(
                new accessRulesCreateDto()
                {
                    name = name,
                    type = type,
                    priority = priority
                }
            );
            var AddUsuarioReglaAcceso = this._accessRulesControlIdQuery.CreateAccessRules(accessRules);
            Response responseAddHorario = await this._httpClientService.Run(controlador, this._apiRutas.ApiUrlCreate, AddUsuarioReglaAcceso);
            return responseAddHorario;
        }
        private async Task<Response> SavePortalAccessRulesControlId(List<Portal> portales, int access_rule_id)
        {
            List<portalsAccessRulesCreateDto> portalsAccessRulesCreateDto = new List<portalsAccessRulesCreateDto>();
            foreach (var portal in portales)
            {
                portalsAccessRulesCreateDto.Add(
                    new portalsAccessRulesCreateDto()
                    {
                        access_rule_id = access_rule_id,
                        portal_id = Convert.ToInt32(portal.ControlId)
                    }
                );
            }

            var AddPortalsAccessRules = this._portalsAccessRulesControlIdQuery.CreatePortalAccessRules(portalsAccessRulesCreateDto);
            Response responseAddPortalsAccessRules = await this._httpClientService.Run(controlador, this._apiRutas.ApiUrlCreate, AddPortalsAccessRules);
            return responseAddPortalsAccessRules;
        }
    }
}