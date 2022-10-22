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
        private readonly HttpClientService _httpClientService;
        private readonly PortalQuery _portalQuery;
        private readonly PortalsControlIdQuery _portalsControlIdQuery;
        private readonly AreaControlIdQuery _areaControlIdQuery;
        private readonly AreaQuery _areaQuery;
        private readonly ActionsControlIdQuery _actionsControlIdQuery;
        private readonly AccionQuery _accionQuery;
        private readonly AccessRulesControlIdQuery _accessRulesControlIdQuery;
        private readonly ReglaAccesoQuery _reglaAccesoQuery;
        ApiRutas _apiRutas;
        public DispositivoController(
            LoginControlIdQuery loginControlIdQuery,
            DispositivoQuery dispositivoQuery,
            HttpClientService httpClientService,
            PortalQuery portalQuery,
            PortalsControlIdQuery portalsControlIdQuery,
            AreaControlIdQuery areaControlIdQuery,
            AreaQuery areaQuery,
            ActionsControlIdQuery actionsControlIdQuery,
            AccionQuery accionQuery,
            AccessRulesControlIdQuery accessRulesControlIdQuery,
            ReglaAccesoQuery reglaAccesoQuery
         )
        {
            this._loginControlIdQuery = loginControlIdQuery;
            this._dispositivoQuery = dispositivoQuery;
            this._httpClientService = httpClientService;
            this._portalQuery = portalQuery;
            this._portalsControlIdQuery = portalsControlIdQuery;
            this._areaControlIdQuery = areaControlIdQuery;
            this._areaQuery = areaQuery;
            this._actionsControlIdQuery = actionsControlIdQuery;
            this._accionQuery = accionQuery;
            this._accessRulesControlIdQuery = accessRulesControlIdQuery;
            this._reglaAccesoQuery = reglaAccesoQuery;
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
            await getDispostivo(dispositivoCreateDto.Ip,dispositivoCreateDto.Puerto,dispositivoCreateDto.Usuario,"login.fcgi",dispositivoCreateDto.Password);
           /*  if (ModelState.IsValid)
            {
                this.user = dispositivoCreateDto.Usuario;
                this.password = dispositivoCreateDto.Password;
                this.controlador = dispositivoCreateDto.Ip;
                this.port = dispositivoCreateDto.Puerto;

                await this.loginControlId();
                Response responseInfoSistem = await this.getControlIdHardware(dispositivoCreateDto.Ip, dispositivoCreateDto.Puerto);
                getInformacionSistemaDto responsenfoSistem = JsonConvert.DeserializeObject<getInformacionSistemaDto>(responseInfoSistem.data);
           
                dispositivoCreateDto.NumeroSerie = responsenfoSistem.serial;
                var dispositivo = await this._dispositivoQuery.Store(dispositivoCreateDto);
                
                Response responseArea = await this.getControlIdAreaDefault();
                if (responseArea.estado)
                {
                   
                    areaResponseDto responseApiAreas = JsonConvert.DeserializeObject<areaResponseDto>(responseArea.data);
                    await this.saveAreaSistema(responseApiAreas);
                }

              
                var responsePortals = await this.getControlIdPortalsDefault();
                if (responsePortals.estado)
                {
                    ResponseportalsDto responseApiPortals = JsonConvert.DeserializeObject<ResponseportalsDto>(responsePortals.data);
                    await this.savePortalSistema(responseApiPortals, dispositivo.Id);
                }
                
                var responseActions = await this.getAccionDefault();
                if (responseActions.estado)
                {
                    ResponseActionsDto responseApiActions = JsonConvert.DeserializeObject<ResponseActionsDto>(responseActions.data);
                    await this.saveActionSistema(responseApiActions);
                }
               
                return RedirectToAction(nameof(Index));
            }
            var aux = dispositivoCreateDto; */
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
                Response login = await this._httpClientService.LoginRun(probarConexionDto.Ip,this.port, this._apiRutas.ApiUrlLogin, cuerpo,"");
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
            this._areaControlIdQuery.Params(port, ip, user, password, login.data);
            return login.estado;
        }
        /*------Obtener data dispositivo------*/
        private async Task<bool> getDispostivo(string Ip, int Puerto, string Usuario, string ApiUrlLogin, string Password)
        {
            /*consutar por dispositivos*/

            var loginStatus = await this.LoginControlId(Ip, Puerto, Usuario, ApiUrlLogin, Password);
            if (loginStatus)
            {
                //crear area
                await this.ApiAreaStore();
                //crear tarjetas
                //await this.CardStoreControlId(persona);
            }
            return loginStatus;
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

        }



        private async Task<Boolean> loginControlId()
        {
            BodyLogin cuerpo = this._loginControlIdQuery.Login(this.user, this.password);
            Response login = await this._httpClientService.LoginRun(this.controlador,this.port, this._apiRutas.ApiUrlLogin, cuerpo,"");
            return login.estado;
        }

     
       /*  private async Task<Response> getControlIdHardware(string ip, int port)
        {
            Response responseAddHorario = await this._httpClientService.RunBlank(controlador,this.port, this._apiRutas.ApiUrlGetInfoSistema,"");
            return responseAddHorario;
        }
        private async Task<Response> getControlIdPortalsDefault()
        {
            BodyShowAllObject getPortals = this._portalsControlIdQuery.ShowPortals();
            Response responseGetPortals = await this._httpClientService.Run(controlador, this.port,this._apiRutas.ApiUrlMostrar, getPortals,"");
            return responseGetPortals;
        }
        private async Task<Response> getControlIdAreaDefault()
        {
            BodyShowAllObject getArea = this._areaControlIdQuery.MostrarTodoAreas();
            Response responseGetArea = await this._httpClientService.Run(controlador,this.port, this._apiRutas.ApiUrlMostrar, getArea,"");
            return responseGetArea;
        }
        private async Task<Response> getAccionDefault()
        {
            BodyShowAllObject getActions = this._actionsControlIdQuery.ShowActions();
            Response responseGetActions = await this._httpClientService.Run(controlador,this.port, this._apiRutas.ApiUrlMostrar, getActions ,"");
            return responseGetActions;
        }
        /*Sistema*/
        /* private async Task<bool> saveAreaSistema(areaResponseDto areaResponseDto)
        {
            foreach (var area in areaResponseDto.areas)
            {
                AreaCreateDto areaCreateDto = new AreaCreateDto
                {
                    ControlId = area.id.ToString(),
                    ControlIdName = area.name,
                    Descripcion = area.name,
                    Nombre = area.name
                };
                await this._areaQuery.Store(areaCreateDto);
            }
            return true;
        }
        private async Task<Boolean> savePortalSistema(ResponseportalsDto responseportalsDto, int dispositivo_id)
        {
            foreach (var portal in responseportalsDto.portals)
            {
                PortalCreateDto portalCreateDto = new PortalCreateDto
                {
                    ControlId = portal.id.ToString(),
                    ControlIdName = portal.name,
                    Descripcion = "default",
                    Nombre = portal.name,
                    ControlIdAreaFromId = portal.area_from_id,
                    ControlIdAreaToId = portal.area_to_id,
                    DispositivoId = dispositivo_id
                };
                await this._portalQuery.store(portalCreateDto);
            }
            return true;
        }
        private async Task<Boolean> saveActionSistema(ResponseActionsDto responseActionsDto) 
        {
            foreach (var action in responseActionsDto.actions)
            {
                AccionCreateDto actionsDto = new AccionCreateDto
                {
                    ControlId = action.id,
                    ControlIdName = action.name,
                    ControlIdAction = action.action,
                    ControlIdParametrers = action.parameters,
                    ControlIdRunAt = action.run_at,
                    Nombre = action.name
                };
                await this._accionQuery.store(actionsDto);
            }
            return true;
        } */
    }
}