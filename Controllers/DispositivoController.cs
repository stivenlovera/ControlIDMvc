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

namespace ControlIDMvc.Controllers
{
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
        private readonly PortalsControlIdQuery _portalsControlIdQuery;
        private readonly PortalsAccessRulesControlIdQuery _portalsAccessRulesControlIdQuery;
        private readonly HorarioControlIdQuery _horarioControlIdQuery;
        private readonly DiasControlIdQuery _diasControlIdQuery;
        private readonly AccessRulesControlIdQuery _accessRulesControlIdQuery;
        private readonly HorarioAccessRulesControlIdQuery _horarioAccessRulesControlIdQuery;
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

            //sistema
            DispositivoQuery dispositivoQuery,
            AccionQuery accionQuery,
            PortalAccionQuery portalAccionQuery,
            PortalQuery portalQuery,
            PortalReglasAccesoQuery portalReglasAccesoQuery,
            ReglaAccesoQuery reglaAccesoQuery,
            HorarioReglaAccesoQuery horarioReglaAccesoQuery,
            HorarioQuery horarioQuery,
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
            this._actionsControlIdQuery = actionsControlIdQuery;
            this._portalsActionsControlIdQuery = portalsActionsControlIdQuery;
            this._accessRulesControlIdQuery = accessRulesControlIdQuery;
            this._portalsControlIdQuery = portalsControlIdQuery;
            this._portalsAccessRulesControlIdQuery = portalsAccessRulesControlIdQuery;
            this._horarioControlIdQuery = horarioControlIdQuery;
            this._diasControlIdQuery = diasControlIdQuery;
            this._horarioAccessRulesControlIdQuery = horarioAccessRulesControlIdQuery;
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
            await this.SaveDispositivo(dispositivoCreateDto.Ip, dispositivoCreateDto.Puerto, dispositivoCreateDto.Usuario, dispositivoCreateDto.Password);

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
            if (ModelState.IsValid)
            {
                BodyLogin cuerpo = this._loginControlIdQuery.Login(probarConexionDto.Usuario, probarConexionDto.Password);
                Response login = await this._httpClientService.LoginRun(probarConexionDto.Ip, this.port, this._apiRutas.ApiUrlLogin, cuerpo, "");
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

        /*
            *CARGAR DESDE CONTROL ID 
        */
        /*login dispositivo*/
        private async Task<bool> LoginControlId(string ip, int port, string user, string api, string password)
        {
            BodyLogin cuerpo = _loginControlIdQuery.Login(user, password);
            Response login = await this._httpClientService.LoginRun(ip, port, api, cuerpo, "");
            /*valido si es el login fue ok*/
            this._actionsControlIdQuery.Params(port, ip, user, password, login.data);
            this._accessRulesControlIdQuery.Params(port, ip, user, password, login.data);
            this._portalsAccessRulesControlIdQuery.Params(port, ip, user, password, login.data);
            this._portalsControlIdQuery.Params(port, ip, user, password, login.data);
            this._portalsAccessRulesControlIdQuery.Params(port, ip, user, password, login.data);
            this._horarioControlIdQuery.Params(port, ip, user, password, login.data);
            this._diasControlIdQuery.Params(port, ip, user, password, login.data);
            this._horarioAccessRulesControlIdQuery.Params(port, ip, user, password, login.data);
            return login.estado;
        }
        /*------USUARIO------*/
        private async Task<bool> SaveDispositivo(string Ip, int Puerto, string Usuario, string Password)
        {
            /*consutar por dispositivos*/

            var loginStatus = await this.LoginControlId(Ip, Puerto, Usuario, this._apiRutas.ApiUrlLogin, Password);
            if (loginStatus)
            {
                //crear usuario
                await this.ActionsStoreControlId();
                await this.PortalStoreControlId();
                await this.ActionsPortalStoreControlId();
                //crear tarjetas si hay 
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
                    portals.Add(new Portal
                    {
                        Nombre = portal.name,
                        Descripcion = portal.name,
                        ControlId = portal.id,
                        ControlIdName = portal.name,
                        ControlIdAreaFromId = portal.area_from_id,
                        ControlIdAreaToId = portal.area_to_id,
                        DispositivoId = 0
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
        private async Task<bool> portalAccessRulesStoreControlId(int PortalId, int ReglaAccesoId)
        {
            var apiportalAccessRules = await this._portalsAccessRulesControlIdQuery.ShowAll();
            if (apiportalAccessRules.status)
            {
                var portalAccessRule = new List<PortalReglaAcceso>();
                foreach (var apiPortalAccessRule in apiportalAccessRules.portalAccesoRulesDtos)
                {
                    portalAccessRule.Add(new PortalReglaAcceso
                    {
                        ControlIdPortalId = apiPortalAccessRule.portal_id,
                        ControlIdRulesId = apiPortalAccessRule.access_rule_id,
                        PortalId = PortalId,
                        ReglaAccesoId = ReglaAccesoId
                    });
                }
                var updateUsuario = await this._portalReglasAccesoQuery.StoreAll(portalAccessRule);
            }
            return apiportalAccessRules.status;
        }
        /* private async Task<bool> HorarioStoreControlId()
        {
            var actions = await this._actionsControlIdQuery.ShowAll();
            if (actions.status)
            {
                var acciones = new List<Accion>();
                foreach (var action in actions.actionsDto)
                {
                    acciones.Add(new Accion{
                        ControlId=action.id,
                        ControlIdAction=action.action,
                        ControlIdParametrers=action.parameters,
                        ControlIdName=action.name,
                        ControlIdRunAt=action.run_at,
                        Nombre=action.name,
                    });
                }
                var updateUsuario = await this._accionQuery.AllStore(acciones);
            }
            return actions.status;
        }
        private async Task<bool> HorarioDiaStoreControlId()
        {
            var actions = await this._actionsControlIdQuery.ShowAll();
            if (actions.status)
            {
                var acciones = new List<Accion>();
                foreach (var action in actions.actionsDto)
                {
                    acciones.Add(new Accion{
                        ControlId=action.id,
                        ControlIdAction=action.action,
                        ControlIdParametrers=action.parameters,
                        ControlIdName=action.name,
                        ControlIdRunAt=action.run_at,
                        Nombre=action.name,
                    });
                }
                var updateUsuario = await this._accionQuery.AllStore(acciones);
            }
            return actions.status;
        }
         private async Task<bool> DiaStoreControlId()
        {
            var actions = await this._actionsControlIdQuery.ShowAll();
            if (actions.status)
            {
                var acciones = new List<Accion>();
                foreach (var action in actions.actionsDto)
                {
                    acciones.Add(new Accion{
                        ControlId=action.id,
                        ControlIdAction=action.action,
                        ControlIdParametrers=action.parameters,
                        ControlIdName=action.name,
                        ControlIdRunAt=action.run_at,
                        Nombre=action.name,
                    });
                }
                var updateUsuario = await this._accionQuery.AllStore(acciones);
            }
            return actions.status;
        }
        
        private async Task<bool> ApiAreaStore()
        {
            var areas = await this._areaControlIdQuery.ShowAll();
            if (areas.status)
            {
                List<AreaCreateDto> new_area = new List<AreaCreateDto>();
                foreach (var area in areas.areaResponseDtos)
                {
                    new_area.Add(
                        new AreaCreateDto
                        {
                            ControlId = area.id,
                            ControlIdName = area.name,
                            Descripcion = area.name,
                            Nombre = area.name
                        }
                    );
                }
                var updateUsuario = await this._areaQuery.StoreAll(new_area);
                return areas.status;
            }
            else
            {
                return areas.status;
            }
        } */
    }
}