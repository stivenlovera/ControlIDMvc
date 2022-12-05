using System.Reflection.Metadata.Ecma335;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Dtos.Dispositivo;
using ControlIDMvc.Dtos.Portal;
using ControlIDMvc.Querys;
using ControlIDMvc.ServicesCI;
using ControlIDMvc.ServicesCI.Dtos.areasDto;
using ControlIDMvc.ServicesCI.Dtos.portalsDto;
using ControlIDMvc.ServicesCI.Dtos.SISTEMA;
using ControlIDMvc.ServicesCI.QueryCI;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ControlIDMvc.Dtos.Area;
using ControlIDMvc.Dtos.Accion;
using ControlIDMvc.ServicesCI.Dtos.actionsDto;
using ControlIDMvc.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ControlIDMvc.Controllers
{
    [Authorize]
    [Route("dispositivo")]
    public class DispositivoController : Controller
    {
        /*proyecto por default*/
        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        private readonly LoginControlIdQuery _loginControlIdQuery;
        private readonly DispositivoQuery _dispositivoQuery;
        private readonly AccionQuery _accionQuery;
        private readonly ActionsControlIdQuery _actionsControlIdQuery;
        private readonly PortalsActionsControlIdQuery _portalsActionsControlIdQuery;
        private readonly PortalAccionQuery _portalAccionQuery;
        private readonly PortalQuery _portalQuery;
        private readonly PortalReglasAccesoQuery _portalReglasAccesoQuery;
        private readonly ReglaAccesoQuery _reglaAccesoQuery;
        private readonly HorarioReglaAccesoQuery _horarioReglaAccesoQuery;
        private readonly HorarioQuery _horarioQuery;
        private readonly DiaQuery _diaQuery;
        private readonly AreaQuery _areaQuery;
        private readonly AreaReglasAccesoQuery _areaReglasAccesoQuery;
        private readonly DispositivoControlIdQuery _dispositivoControlIdQuery;
        private readonly PortalsControlIdQuery _portalsControlIdQuery;
        private readonly PortalsAccessRulesControlIdQuery _portalsAccessRulesControlIdQuery;
        private readonly HorarioControlIdQuery _horarioControlIdQuery;
        private readonly DiasControlIdQuery _diasControlIdQuery;
        private readonly AccessRulesControlIdQuery _accessRulesControlIdQuery;
        private readonly HorarioAccessRulesControlIdQuery _horarioAccessRulesControlIdQuery;
        private readonly AreaControlIdQuery _areaControlIdQuery;
        private readonly AreaAccesRuleControlIdQuery _areaAccesRuleControlIdQuery;
        private readonly HttpClientService _httpClientService;
        ApiRutas _apiRutas;
        public DispositivoController(
            LoginControlIdQuery loginControlIdQuery,
            ActionsControlIdQuery actionsControlIdQuery,
            PortalsActionsControlIdQuery portalsActionsControlIdQuery,
            PortalsControlIdQuery portalsControlIdQuery,
            AccessRulesControlIdQuery accessRulesControlIdQuery,
            PortalsAccessRulesControlIdQuery portalsAccessRulesControlIdQuery,
            HorarioControlIdQuery horarioControlIdQuery,
            DiasControlIdQuery diasControlIdQuery,
            HorarioAccessRulesControlIdQuery horarioAccessRulesControlIdQuery,
            AreaControlIdQuery areaControlIdQuery,
            AreaAccesRuleControlIdQuery areaAccesRuleControlIdQuery,
            //sistema
            DispositivoQuery dispositivoQuery,
            AccionQuery accionQuery,
            PortalAccionQuery portalAccionQuery,
            PortalQuery portalQuery,
            PortalReglasAccesoQuery portalReglasAccesoQuery,
            ReglaAccesoQuery reglaAccesoQuery,
            HorarioReglaAccesoQuery horarioReglaAccesoQuery,
            HorarioQuery horarioQuery,
            DiaQuery diaQuery,
            AreaQuery areaQuery,
            AreaReglasAccesoQuery areaReglasAccesoQuery,
            DispositivoControlIdQuery dispositivoControlIdQuery,
            HttpClientService httpClientService
         )
        {
            this._httpClientService = httpClientService;
            this._loginControlIdQuery = loginControlIdQuery;
            this._dispositivoQuery = dispositivoQuery;
            this._accionQuery = accionQuery;
            this._portalAccionQuery = portalAccionQuery;
            this._portalQuery = portalQuery;
            this._portalReglasAccesoQuery = portalReglasAccesoQuery;
            this._reglaAccesoQuery = reglaAccesoQuery;
            this._horarioReglaAccesoQuery = horarioReglaAccesoQuery;
            this._horarioQuery = horarioQuery;
            this._diaQuery = diaQuery;
            this._areaQuery = areaQuery;
            this._areaReglasAccesoQuery = areaReglasAccesoQuery;
            this._dispositivoControlIdQuery = dispositivoControlIdQuery;
            this._actionsControlIdQuery = actionsControlIdQuery;
            this._portalsActionsControlIdQuery = portalsActionsControlIdQuery;
            this._accessRulesControlIdQuery = accessRulesControlIdQuery;
            this._portalsControlIdQuery = portalsControlIdQuery;
            this._portalsAccessRulesControlIdQuery = portalsAccessRulesControlIdQuery;
            this._horarioControlIdQuery = horarioControlIdQuery;
            this._diasControlIdQuery = diasControlIdQuery;
            this._horarioAccessRulesControlIdQuery = horarioAccessRulesControlIdQuery;
            this._areaControlIdQuery = areaControlIdQuery;
            this._areaAccesRuleControlIdQuery = areaAccesRuleControlIdQuery;
            this._apiRutas = new ApiRutas();
        }


        [HttpPost("data-table")]
        public async Task<ActionResult> DataTable()
        {
            var dataTable = await this._dispositivoQuery.DataTable(Request);
            return Json(dataTable);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Dispositivo/Lista.cshtml");
        }

        [HttpGet("create")]
        public ActionResult Create()
        {

            return View("~/Views/Dispositivo/Create.cshtml");
        }

        [HttpPost("store")]
        public async Task<ActionResult> Store(DispositivoCreateDto dispositivoCreateDto)
        {
            if (ModelState.IsValid)
            {
                await this.SaveDispositivo(dispositivoCreateDto);
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Dispositivo/Create.cshtml", dispositivoCreateDto);
        }

        [HttpGet("edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Dispositivo/Create.cshtml");
        }
        [HttpPut("update")]
        public ActionResult Update()
        {
            return View("~/Views/Dispositivo/Create.cshtml");
        }
        [HttpDelete("delete")]
        public ActionResult Delete()
        {
            return View("~/Views/Dispositivo/Create.cshtml");
        }
        [HttpPost("probar-conexion")]
        public async Task<ActionResult> ProbarConexion(ProbarConexionDto probarConexionDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BodyLogin cuerpo = this._loginControlIdQuery.Login(probarConexionDto.Usuario, probarConexionDto.Password);
                    Response login = await this._httpClientService.LoginRun(probarConexionDto.Ip, probarConexionDto.Puerto, this._apiRutas.ApiUrlLogin, cuerpo, "");
                    if (login.estado)
                    {
                        return Json(new
                        {
                            message = "Conexion satisfactoria",
                            descripcion = "Dispositivo encontrado"
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            message = "Error de conectividad",
                            descripcion = "No se pudo conectar al dipositivo"
                        });
                    }
                }
                return Json(new
                {
                    message = "Error de conexion",
                    errors = ModelState
                });
            }
            catch (System.Exception)
            {
                return Json(new
                {
                    message = "Error de conectividad",
                    descripcion = "No se pudo conectar al dipositivo"
                });
            }
        }

        /*
            *CARGAR DESDE CONTROL ID 
        */
        /*login dispositivo*/
        private async Task<bool> LoginControlId(string ip, int port, string user, string password, string api)
        {
            BodyLogin cuerpo = _loginControlIdQuery.Login(user, password);
            Response login = await this._httpClientService.LoginRun(ip, port, api, cuerpo, "");
            /*valido si es el login fue ok*/
            this._actionsControlIdQuery.Params(port, ip, user, password, login.data);
            this._portalsControlIdQuery.Params(port, ip, user, password, login.data);
            this._portalsActionsControlIdQuery.Params(port, ip, user, password, login.data);
            this._accessRulesControlIdQuery.Params(port, ip, user, password, login.data);
            this._portalsAccessRulesControlIdQuery.Params(port, ip, user, password, login.data);
            this._portalsControlIdQuery.Params(port, ip, user, password, login.data);
            this._portalsAccessRulesControlIdQuery.Params(port, ip, user, password, login.data);
            this._horarioControlIdQuery.Params(port, ip, user, password, login.data);
            this._diasControlIdQuery.Params(port, ip, user, password, login.data);
            this._horarioAccessRulesControlIdQuery.Params(port, ip, user, password, login.data);
            this._areaControlIdQuery.Params(port, ip, user, password, login.data);
            this._areaAccesRuleControlIdQuery.Params(port, ip, user, password, login.data);

            this._dispositivoControlIdQuery.Params(port, ip, user, password, login.data);
            return login.estado;
        }
        /*------USUARIO------*/
        private async Task<bool> SaveDispositivo(DispositivoCreateDto dispositivoCreateDto)
        {
            /*consutar por dispositivos*/

            var loginStatus = await this.LoginControlId(dispositivoCreateDto.Ip, dispositivoCreateDto.Puerto, dispositivoCreateDto.Usuario, dispositivoCreateDto.Password, this._apiRutas.ApiUrlLogin);
            if (loginStatus)
            {
                //crear usuario
                await this.AreaStore();
                await this.ActionsStoreControlId();
                await this.PortalStoreControlId();
                await this.ActionsPortalStoreControlId();

                await this.AccessRulesStoreControlId();
                await this.portalAccessRulesStoreControlId();
                await this.HorarioStoreControlId();
                await this.DiaStoreControlId();
                await this.HorarioAccessRulesControlId();

                await this.AreaReglasAccesoStore();
                await this.DipositivoStore(dispositivoCreateDto);
                //save data dispositivo

            }
            return true;
        }
        private async Task<bool> ActionsStoreControlId()
        {
            var actions = await this._actionsControlIdQuery.ShowAll();
            if (actions.status)
            {
                var acciones = new List<Accion>();
                foreach (var action in actions.actionsDto)
                {
                    acciones.Add(new Accion
                    {
                        ControlId = action.id,
                        ControlIdAction = action.action,
                        ControlIdParametrers = action.parameters,
                        ControlIdName = action.name,
                        ControlIdRunAt = action.run_at,
                        Nombre = action.name,
                    });
                }
                var updateUsuario = await this._accionQuery.AllStore(acciones);
            }
            return actions.status;
        }
        private async Task<bool> PortalStoreControlId()
        {
            var apiportales = await this._portalsControlIdQuery.ShowAll();
            if (apiportales.status)
            {
                var portals = new List<Portal>();
                foreach (var portal in apiportales.portalsDtos)
                {
                    var obtenerFromArea = await this._areaQuery.GetControlId(portal.area_from_id);
                    var obtenerToArea = await this._areaQuery.GetControlId(portal.area_to_id);
                    portals.Add(new Portal
                    {
                        Nombre = portal.name,
                        Descripcion = portal.name,
                        ControlId = portal.id,
                        ControlIdName = portal.name,
                        ControlIdAreaFromId = portal.area_from_id,
                        ControlIdAreaToId = portal.area_to_id,
                        AreaFromId = obtenerFromArea.Id,
                        AreaToId = obtenerToArea.Id
                    });
                }
                var storePortals = await this._portalQuery.StoreAll(portals);
            }
            return apiportales.status;
        }
        private async Task<bool> ActionsPortalStoreControlId()
        {
            var apiPortalActions = await this._portalsActionsControlIdQuery.Show();
            if (apiPortalActions.status)
            {
                var acciones = new List<AccionPortal>();
                foreach (var actionPortal in apiPortalActions.portalsActionsDtos)
                {
                    var portal = await this._portalQuery.SearchControlId(actionPortal.portal_id);
                    var action = await this._accionQuery.SearchControlId(actionPortal.action_id);
                    acciones.Add(new AccionPortal
                    {
                        AccionId = action.Id,
                        ControlActionId = actionPortal.action_id,
                        ControlIdPortalId = actionPortal.portal_id,
                        portalId = portal.Id
                    });
                }
                var updateUsuario = await this._portalAccionQuery.storeAll(acciones);
            }
            return apiPortalActions.status;
        }
        private async Task<bool> AccessRulesStoreControlId()
        {
            var apiAccessRules = await this._accessRulesControlIdQuery.showAll();
            if (apiAccessRules.status)
            {
                var AccessRules = new List<ReglaAcceso>();
                foreach (var AccessRule in apiAccessRules.accessRulesDtos)
                {
                    AccessRules.Add(new ReglaAcceso
                    {
                        ControlId = AccessRule.id,
                        ControlIdName = AccessRule.name,
                        ControlIdPriority = AccessRule.priority,
                        ControlIdType = AccessRule.type,
                        Descripcion = AccessRule.name,
                        Nombre = AccessRule.name
                    });
                }
                var updateUsuario = await this._reglaAccesoQuery.StoreAll(AccessRules);
            }
            return apiAccessRules.status;
        }
        private async Task<bool> portalAccessRulesStoreControlId()
        {
            var apiportalAccessRules = await this._portalsAccessRulesControlIdQuery.ShowAll();
            if (apiportalAccessRules.status)
            {
                var portalAccessRule = new List<PortalReglaAcceso>();
                foreach (var apiPortalAccessRule in apiportalAccessRules.portalAccesoRulesDtos)
                {
                    var portal = await this._portalQuery.SearchControlId(apiPortalAccessRule.portal_id);
                    var ruleAccess = await this._reglaAccesoQuery.SearchControlId(apiPortalAccessRule.access_rule_id);
                    portalAccessRule.Add(new PortalReglaAcceso
                    {
                        ControlIdPortalId = apiPortalAccessRule.portal_id,
                        ControlIdRulesId = apiPortalAccessRule.access_rule_id,
                        PortalId = portal.Id,
                        ReglaAccesoId = ruleAccess.Id
                    });
                }
                var updateUsuario = await this._portalReglasAccesoQuery.StoreAll(portalAccessRule);
            }
            return apiportalAccessRules.status;
        }
        //dia
        private async Task<bool> HorarioStoreControlId()
        {
            var apiHorario = await this._horarioControlIdQuery.ShowAll();
            if (apiHorario.status)
            {
                var horarios = new List<Horario>();
                foreach (var timezoneDto in apiHorario.timezoneDtos)
                {
                    horarios.Add(new Horario
                    {
                        ControlId = timezoneDto.id,
                        ControlIdName = timezoneDto.name,
                        Descripcion = timezoneDto.name,
                        Nombre = timezoneDto.name
                    });
                }
                var updateHorario = await this._horarioQuery.StoreAll(horarios);
            }
            return apiHorario.status;
        }
        private async Task<bool> DiaStoreControlId()
        {
            var apiDias = await this._diasControlIdQuery.ShowAll();
            if (apiDias.status)
            {
                var dias = new List<Dia>();
                foreach (var time_SpansDtos in apiDias.time_SpansDtos)
                {
                    var time_zone = await this._portalQuery.SearchControlId(time_SpansDtos.time_zone_id);
                    dias.Add(new Dia
                    {
                        ControlTimeZoneId = time_SpansDtos.time_zone_id,
                        ControlStart = time_SpansDtos.start,
                        ControlEnd = time_SpansDtos.end,
                        ControlSun = time_SpansDtos.sun,
                        ControlMon = time_SpansDtos.mon,
                        ControlWed = time_SpansDtos.wed,
                        ControlThu = time_SpansDtos.thu,
                        ControlTue = time_SpansDtos.tue,
                        ControlFri = time_SpansDtos.fri,
                        ControlSat = time_SpansDtos.sat,
                        ControlHol1 = time_SpansDtos.hol1,
                        ControlHol2 = time_SpansDtos.hol2,
                        ControlHol3 = time_SpansDtos.hol3,
                        HorarioId = time_zone.ControlId,
                        Start = new DateTime(2022, 12, 25, 00, 00, 00),
                        End = new DateTime(2022, 12, 25, 23, 59, 59),
                        Sun = time_SpansDtos.sun,
                        Mon = time_SpansDtos.mon,
                        Wed = time_SpansDtos.wed,
                        Thu = time_SpansDtos.thu,
                        Tue = time_SpansDtos.tue,
                        Fri = time_SpansDtos.fri,
                        Sat = time_SpansDtos.sat,
                        Hol1 = time_SpansDtos.hol1,
                        Hol2 = time_SpansDtos.hol2,
                        Hol3 = time_SpansDtos.hol3,
                        Nombre = $"default {time_SpansDtos.time_zone_id}"
                    });
                }
                var updateUsuario = await this._diaQuery.StoreAll(dias);
            }
            return apiDias.status;
        }

        private async Task<bool> HorarioAccessRulesControlId()
        {
            var apiHorario = await this._horarioAccessRulesControlIdQuery.ShowAll();
            if (apiHorario.status)
            {
                var horarioAccessRules = new List<HorarioReglaAcceso>();
                foreach (var time_Zones_Access_Rules in apiHorario.time_Zones_Access_RulesDtos)
                {
                    var horario = await this._horarioQuery.SearchControlId(time_Zones_Access_Rules.time_zone_id);
                    var reglasAcceso = await this._reglaAccesoQuery.SearchControlId(time_Zones_Access_Rules.access_rule_id);
                    horarioAccessRules.Add(new HorarioReglaAcceso
                    {
                        ControlIdAccessRulesId = time_Zones_Access_Rules.access_rule_id,
                        ControlIdTimeZoneId = time_Zones_Access_Rules.time_zone_id,
                        HorarioId = horario.Id,
                        ReglasAccesoId = reglasAcceso.Id
                    });
                }
                var updateHorario = await this._horarioReglaAccesoQuery.StoreAll(horarioAccessRules);
            }
            return apiHorario.status;
        }
        private async Task<bool> AreaStore()
        {
            var areas = await this._areaControlIdQuery.ShowAll();
            if (areas.status)
            {
                List<AreaCreateDto> data = new List<AreaCreateDto>();
                foreach (var area in areas.areaResponseDtos)
                {
                    data.Add(
                        new AreaCreateDto
                        {
                            ControlId = area.id,
                            ControlIdName = area.name,
                            Descripcion = area.name,
                            Nombre = area.name
                        }
                    );
                }
                var updateUsuario = await this._areaQuery.StoreAll(data);
                return areas.status;
            }
            else
            {
                return areas.status;
            }
        }
        private async Task<bool> AreaReglasAccesoStore()
        {
            var areas = await this._areaAccesRuleControlIdQuery.ShowAll();
            if (areas.status)
            {

                List<AreaReglaAcceso> data = new List<AreaReglaAcceso>();
                foreach (var area_Access_Rules in areas.area_Access_RulesControlDtos)
                {
                    var reglasAcceso = await this._reglaAccesoQuery.SearchControlId(area_Access_Rules.access_rule_id);
                    var area = await this._areaQuery.SearchControlId(area_Access_Rules.area_id);
                    data.Add(
                        new AreaReglaAcceso
                        {
                            ControlIdAreaId = area_Access_Rules.area_id,
                            ControlidReglaAccesoId = area_Access_Rules.access_rule_id,
                            AreaId = area.Id,
                            ReglaAccesoId = reglasAcceso.Id
                        }
                    );
                }
                var updateUsuario = await this._areaReglasAccesoQuery.storeAll(data);
                return areas.status;
            }
            else
            {
                return areas.status;
            }
        }
        private async Task<bool> DipositivoStore(DispositivoCreateDto dispositivoCreateDto)
        {
            var dispostivos = await this._dispositivoControlIdQuery.ShowAll();
            if (dispostivos.status)
            {
                List<Dispositivo> data = new List<Dispositivo>();
                foreach (var dispositivoDto in dispostivos.dispositivoDtos)
                {
                    data.Add(
                        new Dispositivo
                        {
                            ControlId = dispositivoDto.id,
                            ControlIdIp = dispositivoDto.ip,
                            ControlIdPublicKey = dispositivoDto.public_key,
                            ControlIdName = dispositivoDto.name,
                            Ip = dispositivoDto.ip,
                            Modelo = dispositivoCreateDto.Modelo,
                            NumeroSerie = dispositivoCreateDto.NumeroSerie,
                            Puerto = dispositivoCreateDto.Puerto,
                            Password = dispositivoCreateDto.Password,
                            Nombre = dispositivoCreateDto.Nombre,
                            Usuario = dispositivoCreateDto.Usuario
                        }
                    );
                }
                var create = await this._dispositivoQuery.Store(data);
                return dispostivos.status;
            }
            else
            {
                return dispostivos.status;
            }
        }
    }
}