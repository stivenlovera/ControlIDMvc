using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ControlIDMvc.Models;
using ControlIDMvc.Querys;
using ControlIDMvc.Dtos.Area;
using ControlIDMvc.Dtos.Portal;
using ControlIDMvc.ServicesCI.QueryCI;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using ControlIDMvc.ServicesCI;
using ControlIDMvc.ServicesCI.Dtos.areasDto;
using ControlIDMvc.ServicesCI.Dtos.access_rulesDto;
using Newtonsoft.Json;
using ControlIDMvc.ServicesCI.Dtos.portalsDto;
using ControlIDMvc.Entities;

namespace ControlIDMvc.Controllers
{
    [Route("area")]
    public class AreaController : Controller
    {
        public int port { get; set; }
        public string controlador = "192.168.88.129";
        public string user = "admin";
        public string password = "admin";
        private readonly DBContext _dbContext;
        private readonly AreaQuery _areaQuery;
        private readonly PortalQuery _portalQuery;
        private readonly LoginControlIdQuery _loginControlIdQuery;
        private readonly HttpClientService _httpClientService;
        private readonly AreaControlIdQuery _areaControlIdQuery;
        private readonly PortalsControlIdQuery _portalsControlIdQuery;

        private readonly DispositivoQuery _dispositivoQuery;

        ApiRutas _apiRutas;
        public AreaController(
            DBContext dbContext,
             AreaQuery areaQuery,
             PortalQuery portalQuery,
            LoginControlIdQuery loginControlIdQuery,
            HttpClientService httpClientService,
            AreaControlIdQuery areaControlIdQuery,
            PortalsControlIdQuery portalsControlIdQuery,
             DispositivoQuery dispositivoQuery
             )
        {
            this._dbContext = dbContext;
            this._areaQuery = areaQuery;
            this._portalQuery = portalQuery;
            this._loginControlIdQuery = loginControlIdQuery;
            this._httpClientService = httpClientService;
            this._areaControlIdQuery = areaControlIdQuery;
            this._portalsControlIdQuery = portalsControlIdQuery;
            this._dispositivoQuery = dispositivoQuery;
            _apiRutas = new ApiRutas();
        }

        [HttpPost("data-table")]
        public async Task<ActionResult> DataTable()
        {
            var dataTable = await this._areaQuery.DataTable(Request);
            return Json(dataTable);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Area/Lista.cshtml");
        }

        [HttpGet("create")]
        public async Task<ActionResult> Create()
        {
            var portals = await this._portalQuery.GetAllDetail();
            //mostrar disponibles
            var PuertasDisponibleId = new List<string>();
            var PuertasDisponibleNombre = new List<string>();
            var PuertasDisponibleAreaSalida = new List<string>();
            var PuertasDisponibleAreaSalidaNombre = new List<string>();
            var PuertasDisponibleAreaEntrada = new List<string>();
            var PuertasDisponibleAreaEntradaNombre = new List<string>();

            foreach (var item in portals)
            {
                PuertasDisponibleId.Add(item.Id.ToString());
                PuertasDisponibleNombre.Add(item.Nombre);
                PuertasDisponibleAreaEntrada.Add(item.AreaFromId.ToString());
                PuertasDisponibleAreaEntradaNombre.Add(item.AreaFrom.Nombre);
                PuertasDisponibleAreaSalida.Add(item.AreaToId.ToString());
                PuertasDisponibleAreaSalidaNombre.Add(item.AreaTo.Nombre);
            }

            var areaCreateDto = new AreaCreateDto()
            {
                Nombre = "",
                Descripcion = "",

                PuertasDisponibleId = PuertasDisponibleId,
                PuertasDisponibleNombre = PuertasDisponibleNombre,
                PuertasDisponibleAreaSalida = PuertasDisponibleAreaSalida,
                PuertasDisponibleAreaSalidaNombre = PuertasDisponibleAreaSalidaNombre,
                PuertasDisponibleAreaEntrada = PuertasDisponibleAreaEntrada,
                PuertasDisponibleAreaEntradaNombre = PuertasDisponibleAreaEntradaNombre,

                PuertasSelecionadasId = new List<string>(),
                PuertasSelecionadasNombre = new List<string>(),
                PuertasSelecionadasAreaEntradaNombre = new List<string>(),
                PuertasSelecionadasAreaEntrada = new List<string>(),
                PuertasSelecionadasAreaSalida = new List<string>(),
                PuertasSelecionadasAreaSalidaNombre = new List<string>()
            };
            return View("~/Views/Area/Create.cshtml", areaCreateDto);
        }

        [HttpPost("store")]
        public async Task<ActionResult> Store(AreaCreateDto areaCreateDto)
        {
            if (ModelState.IsValid)
            {
                if (await this._areaQuery.ValidarNombre(areaCreateDto.Nombre))
                {
                    var area = new Area
                    {
                        Descripcion = areaCreateDto.Nombre,
                        Nombre = areaCreateDto.Nombre,

                    };
                    var insertArea = await this._areaQuery.Store(area);
                    foreach (var PuertasSelecionada in areaCreateDto.PuertasSelecionadasId)
                    {
                        var update = new Portal
                        {
                            Id = Convert.ToInt32(PuertasSelecionada),
                            ControlIdAreaFromId = insertArea.ControlId,
                            ControlIdAreaToId = insertArea.ControlId,
                            AreaFromId = insertArea.Id,
                            AreaToId = insertArea.Id
                        };
                        var updatePortal = await this._portalQuery.Update(update);
                    }

                    await this.StoreArea(insertArea, areaCreateDto);
                    return RedirectToAction(nameof(Index));
                }

            }
            return View("~/Views/Area/Create.cshtml", areaCreateDto);
        }

        [HttpGet("edit/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            var area = await this._areaQuery.GetOne(id);

            var portalsDisponibles = await this._portalQuery.GetAllDisponibles(id);
            //mostrar disponibles
            var PuertasDisponibleId = new List<string>();
            var PuertasDisponibleNombre = new List<string>();
            var PuertasDisponibleAreaSalida = new List<string>();
            var PuertasDisponibleAreaSalidaNombre = new List<string>();
            var PuertasDisponibleAreaEntrada = new List<string>();
            var PuertasDisponibleAreaEntradaNombre = new List<string>();

            foreach (var item in portalsDisponibles)
            {
                PuertasDisponibleId.Add(item.Id.ToString());
                PuertasDisponibleNombre.Add(item.Nombre);
                PuertasDisponibleAreaEntrada.Add(item.AreaFromId.ToString());
                PuertasDisponibleAreaEntradaNombre.Add(item.AreaFrom.Nombre);
                PuertasDisponibleAreaSalida.Add(item.AreaToId.ToString());
                PuertasDisponibleAreaSalidaNombre.Add(item.AreaTo.Nombre);
            }
            //mostrar ocupados
            var portalsSelecionadas = await this._portalQuery.GetAllSelecionadas(id);

            var PuertasSelecionadasId = new List<string>();
            var PuertasSelecionadasNombre = new List<string>();
            var PuertasSelecionadasAreaEntrada = new List<string>();
            var PuertasSelecionadasAreaEntradaNombre = new List<string>();
            var PuertasSelecionadasAreaSalida = new List<string>();
            var PuertasSelecionadasAreaSalidaNombre = new List<string>();

            foreach (var item in portalsSelecionadas)
            {
                PuertasSelecionadasId.Add(item.Id.ToString());
                PuertasSelecionadasNombre.Add(item.Nombre);
                PuertasSelecionadasAreaEntrada.Add(item.AreaFromId.ToString());
                PuertasSelecionadasAreaEntradaNombre.Add(item.AreaFrom.Nombre);
                PuertasSelecionadasAreaSalida.Add(item.AreaToId.ToString());
                PuertasSelecionadasAreaSalidaNombre.Add(item.AreaTo.Nombre);
            }

            var editar = new AreaDto()
            {
                Id = area.Id,
                Nombre = area.Nombre,
                Descripcion = area.Descripcion,

                PuertasDisponibleId = PuertasDisponibleId,
                PuertasDisponibleNombre = PuertasDisponibleNombre,
                PuertasDisponibleAreaSalida = PuertasDisponibleAreaSalida,
                PuertasDisponibleAreaSalidaNombre = PuertasDisponibleAreaSalidaNombre,
                PuertasDisponibleAreaEntrada = PuertasDisponibleAreaEntrada,
                PuertasDisponibleAreaEntradaNombre = PuertasDisponibleAreaEntradaNombre,

                PuertasSelecionadasId = PuertasSelecionadasId,
                PuertasSelecionadasNombre = PuertasSelecionadasNombre,
                PuertasSelecionadasAreaEntradaNombre = PuertasSelecionadasAreaEntradaNombre,
                PuertasSelecionadasAreaEntrada = PuertasSelecionadasAreaEntrada,
                PuertasSelecionadasAreaSalida = PuertasSelecionadasAreaSalida,
                PuertasSelecionadasAreaSalidaNombre = PuertasSelecionadasAreaSalidaNombre
            };

            return View("~/Views/Area/Edit.cshtml", editar);
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult> Update(int id, AreaDto areaDto)
        {
            if (ModelState.IsValid)
            {
                if (await this._areaQuery.ValidarNombre(areaDto.Nombre))
                {
                    var area = new Area
                    {
                        Id = id,
                        Descripcion = areaDto.Nombre,
                        Nombre = areaDto.Nombre,
                        ControlIdName = areaDto.Nombre,

                    };
                    var updateArea = await this._areaQuery.Update(area);
                    foreach (var PuertasSelecionada in areaDto.PuertasSelecionadasId)
                    {
                        var update = new Portal
                        {
                            Id = Convert.ToInt32(PuertasSelecionada),
                            ControlIdAreaFromId = updateArea.ControlId,
                            ControlIdAreaToId = updateArea.ControlId,
                            AreaFromId = updateArea.Id,
                            AreaToId = updateArea.Id
                        };
                        var updatePortal = await this._portalQuery.UpdateControlId(update);
                    }
                    //actualizar controlId
                    await this.UpdateArea(updateArea);
                    return RedirectToAction(nameof(Index));
                }

            }
            return View("~/Views/Area/Create.cshtml", areaDto);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var verificar = await this._areaQuery.VerificarDelete(id);
            if (!verificar)
            {
                var area=await this._areaQuery.GetOne(id);
                if (await this._areaQuery.Delete(id))
                {
                    await this._areaControlIdQuery.Delete(area);
                    return Json(new
                    {
                        status = "success",
                        message = "Eliminado correctamente",
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = "error",
                        message = "Ocurrio un error",
                    });
                }
            }
            else
            {
                return Json(new
                {
                    status = "error",
                    message = "No se puede eliminar por esta siendo usado",
                });
            }
        }
        private async Task<Boolean> loginControlId()
        {
            BodyLogin cuerpo = _loginControlIdQuery.Login(this.user, this.password);
            Response login = await this._httpClientService.LoginRun(this.controlador, this.port, this._apiRutas.ApiUrlLogin, cuerpo, "");

            return login.estado;
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
            this._areaControlIdQuery.Params(port, ip, user, password, login.data);
            this._portalsControlIdQuery.Params(port, ip, user, password, login.data);
            return login.estado;
        }
        /*------Obtener data dispositivo------*/
        private async Task<bool> StoreArea(Area area, AreaCreateDto areaCreateDto)
        {
            /*buscar por dispositivos*/
            var dispositivos = await this._dispositivoQuery.GetAll();
            foreach (var dispositivo in dispositivos)
            {
                var loginStatus = await this.LoginControlId(dispositivo.Ip, dispositivo.Puerto, dispositivo.Usuario, this._apiRutas.ApiUrlLogin, dispositivo.Password);
                if (loginStatus)
                {
                    //crear usuario
                    await this.AreaStore(area);
                    await this.UpdatePortals(area);
                    //await this.CardStoreControlId(persona);
                }
            }
            return true;
        }
        private async Task<bool> AreaStore(Area area)
        {
            var apiResponseArea = await this._areaControlIdQuery.Store(area);
            if (apiResponseArea.status)
            {
                //var updatePortal=this._portalsControlIdQuery.Update()
                area.ControlId = apiResponseArea.ids[0];
                await this._areaQuery.UpdateControlId(area);
                return apiResponseArea.status;
            }
            else
            {
                return apiResponseArea.status;
            }
        }
        private async Task<bool> UpdatePortals(Area area)
        {
            var gePortals = await this._portalQuery.GetAllAreaId(area.Id);
            foreach (var portal in gePortals)
            {
                portal.ControlIdAreaFromId=area.ControlId;
                portal.ControlIdAreaToId=area.ControlId;
                var apiResponse = await this._portalsControlIdQuery.Update(portal);
                if (apiResponse.status)
                {
                    portal.ControlIdAreaFromId = area.ControlId;
                    portal.ControlIdAreaToId = area.ControlId;
                    await this._portalQuery.UpdateControlId(portal);
                    return apiResponse.status;
                }
                else
                {
                    return apiResponse.status;
                }
            }
            return true;
        }
        /*------Obtener data dispositivo------*/
        private async Task<bool> UpdateArea(Area area)
        {
            /*buscar por dispositivos*/
            var dispositivos = await this._dispositivoQuery.GetAll();
            foreach (var dispositivo in dispositivos)
            {
                var loginStatus = await this.LoginControlId(dispositivo.Ip, dispositivo.Puerto, dispositivo.Usuario, this._apiRutas.ApiUrlLogin, dispositivo.Password);
                if (loginStatus)
                {
                    //crear usuario
                    await this.AreaUpdate(area);
                    await this.UpdatePortals(area);
                    //await this.CardStoreControlId(persona);
                }
            }
            return true;
        }
        private async Task<bool> AreaUpdate(Area area)
        {
            var apiResponse = await this._areaControlIdQuery.Update(area);
            if (apiResponse.status)
            {
                if (apiResponse.changes > 0)
                {
                    await this._areaQuery.UpdateControlId(area);
                }
                return apiResponse.status;
            }
            else
            {
                return apiResponse.status;
            }
        }
    }
}