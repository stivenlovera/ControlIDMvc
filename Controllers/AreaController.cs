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
            var puertas = await this._portalQuery.GetAll();
            ViewData["puertas"] = puertas;
            return View("~/Views/Area/Create.cshtml");
        }

        [HttpPost("store")]
        public async Task<ActionResult> Store(AreaCreateDto areaCreateDto)
        {
            if (ModelState.IsValid)
            {
                if (await this._areaQuery.ValidarNombre(areaCreateDto.Nombre))
                {
                    var area=new Area{
                        Descripcion=areaCreateDto.Nombre,
                        Nombre=areaCreateDto.Nombre,

                    };
                    var insert = await this._areaQuery.Store(area);
                    await this.StoreArea(area);
                    return RedirectToAction(nameof(Index));
                }

            }
            return View("~/Views/Area/Create.cshtml", areaCreateDto);
        }

        [HttpGet("edit/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            var puertas = await this._portalQuery.GetAll();
            ViewData["puertas"] = puertas;
            var area = await this._areaQuery.GetOne(id);
            return View("~/Views/Area/Edit.cshtml", area);
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult> Update(int id, AreaDto areaDto)
        {
            var puertas = await this._portalQuery.GetAll();
            ViewData["puertas"] = puertas;
            return View("~/Views/Area/Create.cshtml");
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> Delete()
        {
            var puertas = await this._portalQuery.GetAll();
            ViewData["puertas"] = puertas;
            return View("~/Views/Area/Create.cshtml");
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
            return login.estado;
        }
        /*------Obtener data dispositivo------*/
        private async Task<bool> StoreArea(Area area)
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
                    //crear tarjetas
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
                area.ControlId = apiResponseArea.ids[0];
                await this._areaQuery.Update(area);
                return apiResponseArea.status;
            }
            else
            {
                return apiResponseArea.status;
            }

        }
        /*Extras*/
        /*   private async Task<Response> SaveAreaControlId(AreaCreateDto areaCreateDto)
          {
              List<areaCreateDto> areaCreateControlIdDto = new List<areaCreateDto>();
              areaCreateControlIdDto.Add(
                  new areaCreateDto()
                  {
                      name = areaCreateDto.Nombre
                  }
              );
              BodyCreateObject AddHorario = this._areaControlIdQuery.CreateAreas(areaCreateControlIdDto);
              Response responseAddHorario = await this._httpClientService.Run(controlador, this.port, this._apiRutas.ApiUrlCreate, AddHorario, "");
              return responseAddHorario;
          }
          private async Task<Response> updatePuertaControlId(string nombre, int area_id, int portal_id) 
          {
              portalsCreateDto portalsCreateDto = new portalsCreateDto
              {
                  name = $"TO : {nombre}",
                  area_from_id = area_id,
                  area_to_id = area_id
              };
              BodyUpdateObject updatePortal = this._portalsControlIdQuery.UpdatePortals(portalsCreateDto, portal_id);
              Response responseUpdatePortal = await this._httpClientService.Run(controlador, this.port, this._apiRutas.ApiUrlUpdate, updatePortal, "");
              return responseUpdatePortal;
          }*/
    }
}