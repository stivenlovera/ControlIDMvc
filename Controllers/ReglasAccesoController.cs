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
        private readonly DispositivoQuery _dispositivoQuery;
        private readonly PortalsAccessRulesControlIdQuery _portalsAccessRulesControlIdQuery;
        private readonly AreaAccesRuleControlIdQuery _areaAccesRuleControlIdQuery;
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
            AreaQuery areaQuery,
            /*API*/
            DispositivoQuery dispositivoQuery,
            LoginControlIdQuery loginControlIdQuery,
            UsuarioRulesAccessControlIdQuery UsuarioReglasAccesoControlIdQuery,
            AccessRulesControlIdQuery accessRulesControlIdQuery,
            HorarioAccessRulesControlIdQuery HorarioAccessRulesControlIdQuery,
            PortalsAccessRulesControlIdQuery PortalsAccessRulesControlIdQuery,
            AreaAccesRuleControlIdQuery areaAccesRuleControlIdQuery

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
            this._dispositivoQuery = dispositivoQuery;
            this._portalsAccessRulesControlIdQuery = PortalsAccessRulesControlIdQuery;
            this._areaAccesRuleControlIdQuery = areaAccesRuleControlIdQuery;
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
                    var listaAreas = new List<int>();
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
                        listaAreas.Add(area);
                    }
                    var reglasArea = await this._areaReglaAccesoQuery.storeAll(AreaReglaAccesos);
                    /*analizando portal deacuerdo a areas*/
                    var portales = await this._portalQuery.GetAllAreaID(listaAreas);
                    /*adicionando portal*/
                    var insertPortalReglaAcceso = new List<PortalReglaAcceso>();
                    foreach (var portal in portales)
                    {
                        insertPortalReglaAcceso.Add(new PortalReglaAcceso
                        {
                            ReglaAccesoId = insert.Id,
                            PortalId = portal.Id
                        });
                    }
                    var reglasPortals = await this._portalReglasAccesoQuery.StoreAll(insertPortalReglaAcceso);
                    /*add regla acceso*/
                    await this.StoreReglaAcceso(insert, reglasPersona, reglasArea, reglasHorario, reglasPortals);
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
        [HttpPut("update/{id:int}")]
        public async Task<ActionResult> Update(int id, ReglaAccesoCreateDto reglaAccesoCreateDto)
        {
            //await this._reglaAccesoQuery.ValidarNombre(reglaAccesoCreateDto.Nombre);
            var dataUpdate = new ReglaAcceso
            {
                Id = id,
                Nombre = reglaAccesoCreateDto.Nombre,
                Descripcion = reglaAccesoCreateDto.Descripcion
            };
            var update = await this._reglaAccesoQuery.Update(dataUpdate);
            //limpiando a persona reglas acceso
            var deletePerosna = await this._personaReglaAccesoQuery.DeleteAllReglaAccesoId(id);
            //limpiando a horario reglas acceso
            var deleteHorario = await this._horarioReglaAccesoQuery.DeleteAllReglaAccesoId(id);
            //limpiando a area reglas acceso
            var deleteArea = await this._areaReglaAccesoQuery.DeleteAllReglaAccesoId(id);
            //limpiando a portals reglas acceso
            var deletePortal = await this._portalReglasAccesoQuery.DeleteAllReglaAccesoId(id);

            /*adicionando persona*/
            var insertPersonaReglaAcceso = new List<PersonaReglasAcceso>();
            foreach (var persona in reglaAccesoCreateDto.PersonasSelecionadas)
            {
                insertPersonaReglaAcceso.Add(new PersonaReglasAcceso
                {
                    ReglaAccesoId = update.Id,
                    PersonaId = persona
                });
            }
            var reglasPersonas = await this._personaReglaAccesoQuery.StoreAll(insertPersonaReglaAcceso);
            /*adicionando horario*/
            var insertHorarioReglaAcceso = new List<HorarioReglaAcceso>();
            foreach (var horario in reglaAccesoCreateDto.HorarioSelecionados)
            {
                insertHorarioReglaAcceso.Add(new HorarioReglaAcceso
                {
                    HorarioId = horario,
                    ReglasAccesoId = update.Id,
                });

            }
            var reglasHorarios = await this._horarioReglaAccesoQuery.StoreAll(insertHorarioReglaAcceso);
            /*adicionando area*/
            var insertAreaReglaAcceso = new List<AreaReglaAcceso>();
            var listaAreas = new List<int>();
            foreach (var area in reglaAccesoCreateDto.AreaSelecionadas)
            {
                insertAreaReglaAcceso.Add(new AreaReglaAcceso
                {
                    ReglaAccesoId = update.Id,
                    AreaId = area
                });
                listaAreas.Add(area);
            }
            var reglasAreas = await this._areaReglaAccesoQuery.storeAll(insertAreaReglaAcceso);
            /*analizando portal deacuerdo a areas*/
            var portales = await this._portalQuery.GetAllAreaID(listaAreas);
            /*adicionando portal*/
            var insertPortalReglaAcceso = new List<PortalReglaAcceso>();
            foreach (var portal in portales)
            {
                insertPortalReglaAcceso.Add(new PortalReglaAcceso
                {
                    ReglaAccesoId = update.Id,
                    PortalId = portal.Id
                });
            }
            var reglasPortals = await this._portalReglasAccesoQuery.StoreAll(insertPortalReglaAcceso);
            // add a dispsitivo
            await this.UpdateReglaAcceso(update, reglasPersonas, reglasAreas, reglasHorarios, reglasPortals);
            return Json(
                new
                {
                    status = "success",
                    message = "recibido"
                }
            );
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var reglaAcceso = await this._reglaAccesoQuery.GetOne(id);
            /*validate */
            if (await this._reglaAccesoQuery.Delete(reglaAcceso.Id))
            {
                var delete = await this.DeleteReglaAcceso(reglaAcceso);
                return Json(
                    new
                    {
                        status = "success",
                        message = "Regla de acceso"
                    }
                );
            }
            else
            {
                return Json(
                     new
                     {
                         status = "error",
                         message = "Ocurrio un error"
                     }
                 );
            }
        }
        /*
           *CARGAR A CONTROL ID Y ACTUALIZAR SISTEMA
       */
        /*login dispositivo*/
        private async Task<bool> LoginControlId(string ip, int port, string user, string api, string password)
        {
            BodyLogin cuerpo = _loginControlIdQuery.Login(user, password);
            Response login = await this._httpClientService.LoginRun(ip, port, api, cuerpo, "");
            /*valido si es el login fue ok*/
            this._accessRulesControlIdQuery.Params(port, ip, user, password, login.data);
            this._usuarioRulesAccessControlIdQuery.Params(port, ip, user, password, login.data);
            this._areaAccesRuleControlIdQuery.Params(port, ip, user, password, login.data);
            this._horarioAccessRulesControlIdQuery.Params(port, ip, user, password, login.data);
            this._portalsAccessRulesControlIdQuery.Params(port, ip, user, password, login.data);
            //this._portalsControlIdQuery.Params(port, ip, user, password, login.data);
            return login.estado;
        }
        /*------Obtener data dispositivo------*/
        private async Task<bool> StoreReglaAcceso(ReglaAcceso reglaAcceso, List<PersonaReglasAcceso> personaReglasAcceso, List<AreaReglaAcceso> areaReglaAccesos, List<HorarioReglaAcceso> horarioReglaAccesos, List<PortalReglaAcceso> portalReglaAccesos)
        {
            /*buscar por dispositivos*/
            var dispositivos = await this._dispositivoQuery.GetAll();
            foreach (var dispositivo in dispositivos)
            {
                var loginStatus = await this.LoginControlId(dispositivo.Ip, dispositivo.Puerto, dispositivo.Usuario, this._apiRutas.ApiUrlLogin, dispositivo.Password);
                if (loginStatus)
                {
                    //crear usuario
                    await this.StoreAccesoRules(reglaAcceso);
                    await this.StorePersonaAccesoRules(personaReglasAcceso);
                    await this.StoreAreaAccesoRules(areaReglaAccesos);
                    await this.StoreHorarioAccesoRules(horarioReglaAccesos);
                    //await this.StorePortalsAccesoRules(portalReglaAccesos);
                }
            }
            return true;
        }
        /*------Delete data dispositivo------*/
        private async Task<bool> DeleteReglaAcceso(ReglaAcceso reglaAcceso)
        {
            /*buscar por dispositivos*/
            var dispositivos = await this._dispositivoQuery.GetAll();
            foreach (var dispositivo in dispositivos)
            {
                var loginStatus = await this.LoginControlId(dispositivo.Ip, dispositivo.Puerto, dispositivo.Usuario, this._apiRutas.ApiUrlLogin, dispositivo.Password);
                if (loginStatus)
                {
                    //crear regla acceso
                    await this.DeletePortalsAccesoRules(reglaAcceso.ControlId);
                }
            }
            return true;
        }
        /*------Obtener data dispositivo------*/
        private async Task<bool> StoreAccesoRules(ReglaAcceso reglaAcceso)
        {
            var reglasAccesos = new List<ReglaAcceso>();
            reglasAccesos.Add(reglaAcceso);
            var apiResponse = await this._accessRulesControlIdQuery.CreateAccessRules(reglasAccesos);
            if (apiResponse.status)
            {
                reglaAcceso.ControlId = apiResponse.ids[0];
                await this._reglaAccesoQuery.UpdateControlId(reglaAcceso);

                return apiResponse.status;
            }
            else
            {
                return apiResponse.status;
            }
        }

        private async Task<bool> StorePersonaAccesoRules(List<PersonaReglasAcceso> personaReglasAccesos)
        {
            var apiResponse = await this._usuarioRulesAccessControlIdQuery.CreateAll(personaReglasAccesos);
            if (apiResponse.status)
            {
                foreach (var personaReglasAcceso in personaReglasAccesos)
                {
                    personaReglasAcceso.ControlIdAccessRulesId = personaReglasAcceso.ReglaAcceso.ControlId;
                    personaReglasAcceso.ControlIdUserId = personaReglasAcceso.Persona.ControlId;
                    await this._personaReglaAccesoQuery.UpdateControlId(personaReglasAcceso);
                }
                return apiResponse.status;
            }
            else
            {
                return apiResponse.status;
            }
        }
        private async Task<bool> StoreAreaAccesoRules(List<AreaReglaAcceso> areaReglaAccesos)
        {
            var apiResponse = await this._areaAccesRuleControlIdQuery.CreateAll(areaReglaAccesos);
            if (apiResponse.status)
            {
                foreach (var areaReglaAcceso in areaReglaAccesos)
                {
                    areaReglaAcceso.ControlIdAreaId = areaReglaAcceso.Area.ControlId;
                    areaReglaAcceso.ControlidReglaAccesoId = areaReglaAcceso.ReglaAcceso.ControlId;
                    await this._areaReglaAccesoQuery.UpdateControlId(areaReglaAcceso);
                }
                return apiResponse.status;
            }
            else
            {
                return apiResponse.status;
            }
        }
        private async Task<bool> StoreHorarioAccesoRules(List<HorarioReglaAcceso> horarioReglaAccesos)
        {
            var apiResponse = await this._horarioAccessRulesControlIdQuery.CreateAll(horarioReglaAccesos);
            if (apiResponse.status)
            {
                foreach (var horarioReglaAcceso in horarioReglaAccesos)
                {
                    horarioReglaAcceso.ControlIdTimeZoneId = horarioReglaAcceso.Horario.ControlId;
                    horarioReglaAcceso.ControlIdAccessRulesId = horarioReglaAcceso.ReglasAcceso.ControlId;
                    await this._horarioReglaAccesoQuery.UpdateControlId(horarioReglaAcceso);
                }
                return apiResponse.status;
            }
            else
            {
                return apiResponse.status;
            }
        }
        private async Task<bool> StorePortalsAccesoRules(List<PortalReglaAcceso> portalReglaAccesos)
        {
            var apiResponse = await this._portalsAccessRulesControlIdQuery.CreateAll(portalReglaAccesos);
            if (apiResponse.status)
            {
                foreach (var portalReglaAcceso in portalReglaAccesos)
                {
                    portalReglaAcceso.ControlIdPortalId = portalReglaAcceso.Portal.ControlId;
                    portalReglaAcceso.ControlIdRulesId = portalReglaAcceso.ReglaAcceso.ControlId;
                    await this._portalReglasAccesoQuery.UpdateControlId(portalReglaAcceso);
                }
                return apiResponse.status;
            }
            else
            {
                return apiResponse.status;
            }
        }


        /*------Delete data dispositivo------*/
        private async Task<bool> UpdateReglaAcceso(ReglaAcceso reglaAcceso, List<PersonaReglasAcceso> personaReglasAcceso, List<AreaReglaAcceso> areaReglaAccesos, List<HorarioReglaAcceso> horarioReglaAccesos, List<PortalReglaAcceso> portalReglaAccesos)
        {
            /*buscar por dispositivos*/
            var dispositivos = await this._dispositivoQuery.GetAll();
            foreach (var dispositivo in dispositivos)
            {
                var loginStatus = await this.LoginControlId(dispositivo.Ip, dispositivo.Puerto, dispositivo.Usuario, this._apiRutas.ApiUrlLogin, dispositivo.Password);
                if (loginStatus)
                {
                    await this.UpdateAccesoRules(reglaAcceso);
                    await this.DeletePersonaAccesoRules(reglaAcceso.ControlId);
                    await this.DeleteAreaAccesoRules(reglaAcceso.ControlId);
                    await this.DeleteHorarioAccesoRules(reglaAcceso.ControlId);
                    //await this.DeletePortalsAccesoRules(reglaAcceso.ControlId);
                    //recrear
                    await this.StorePersonaAccesoRules(personaReglasAcceso);
                    await this.StoreAreaAccesoRules(areaReglaAccesos);
                    await this.StoreHorarioAccesoRules(horarioReglaAccesos);
                    //await this.StorePortalsAccesoRules(portalReglaAccesos);
                }
            }
            return true;
        }
        private async Task<bool> UpdateAccesoRules(ReglaAcceso reglaAcceso)
        {
            var apiResponse = await this._accessRulesControlIdQuery.Update(reglaAcceso);
            if (apiResponse.status)
            {
                reglaAcceso.ControlIdName = reglaAcceso.Nombre;
                await this._reglaAccesoQuery.UpdateControlId(reglaAcceso);

                return apiResponse.status;
            }
            else
            {
                return apiResponse.status;
            }
        }
        private async Task<bool> DeletePersonaAccesoRules(int ReglasAccesoId)
        {
            var apiResponse = await this._usuarioRulesAccessControlIdQuery.DeleteReglaAccesoId(ReglasAccesoId);
            if (apiResponse.status)
            {
                return apiResponse.status;
            }
            else
            {
                return apiResponse.status;
            }
        }
        private async Task<bool> DeleteAreaAccesoRules(int ReglasAccesoId)
        {
            var apiResponse = await this._areaAccesRuleControlIdQuery.DeleteAccessRulesId(ReglasAccesoId);
            if (apiResponse.status)
            {
                return apiResponse.status;
            }
            else
            {
                return apiResponse.status;
            }
        }
        private async Task<bool> DeleteHorarioAccesoRules(int ReglasAccesoId)
        {
            var apiResponse = await this._horarioAccessRulesControlIdQuery.DeleteReglasAccesoId(ReglasAccesoId);
            if (apiResponse.status)
            {
                return apiResponse.status;
            }
            else
            {
                return apiResponse.status;
            }
        }
        private async Task<bool> DeletePortalsAccesoRules(int ReglasAccesoId)
        {
            var apiResponse = await this._portalsAccessRulesControlIdQuery.DeleteAccessRulesId(ReglasAccesoId);
            if (apiResponse.status)
            {
                return apiResponse.status;
            }
            else
            {
                return apiResponse.status;
            }
        }
    }
}