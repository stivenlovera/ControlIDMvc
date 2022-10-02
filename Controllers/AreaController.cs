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

namespace ControlIDMvc.Controllers
{
    [Route("area")]
    public class AreaController : Controller
    {
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
        ApiRutas _apiRutas;
        public AreaController(
            DBContext dbContext,
             AreaQuery areaQuery,
             PortalQuery portalQuery,
            LoginControlIdQuery loginControlIdQuery,
            HttpClientService httpClientService,
            AreaControlIdQuery areaControlIdQuery,
            PortalsControlIdQuery portalsControlIdQuery
             )
        {
            this._dbContext = dbContext;
            this._areaQuery = areaQuery;
            this._portalQuery = portalQuery;
            this._loginControlIdQuery = loginControlIdQuery;
            this._httpClientService = httpClientService;
            this._areaControlIdQuery = areaControlIdQuery;
            this._portalsControlIdQuery = portalsControlIdQuery;
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
            var puertas = await this._portalQuery.GetAll();
            ViewData["puertas"] = puertas;
            if (ModelState.IsValid)
            {
                if (await this.loginControlId())
                {
                    /*crear area controlador*/
                    var responseAreas = await this.SaveAreaControlId(areaCreateDto);
                    if (responseAreas.estado)
                    {
                        responseApi responseApiAreas = JsonConvert.DeserializeObject<responseApi>(responseAreas.data);
                        /*crear area sistema*/
                        areaCreateDto.ControlId = responseApiAreas.ids[0].ToString();
                        areaCreateDto.ControlIdName = areaCreateDto.Nombre;
                        var saveArea = await this._areaQuery.Store(areaCreateDto);
                        var listaPuertas = await this._portalQuery.GetAllByID(areaCreateDto.PuertasSelecionadas.Select(int.Parse).ToList());

                        foreach (var puerta in listaPuertas)
                        {
                            /*update portal controlador*/
                            var responseUpdatePortal = await this.updatePuertaControlId(
                                areaCreateDto.Nombre,
                                responseApiAreas.ids[0],
                                Convert.ToInt32(puerta.ControlId)
                                );
                            if (responseUpdatePortal.estado)
                            {
                                /*update portal sistema*/
                                puerta.Nombre = $"TO : {areaCreateDto.Nombre}";
                                puerta.ControlIdName = $"TO: {areaCreateDto.Nombre}";
                                puerta.ControlIdAreaFromId = responseApiAreas.ids[0];
                                puerta.ControlIdAreaToId = responseApiAreas.ids[0];
                                await this._portalQuery.UpdateArea(puerta, puerta.Id);
                            }
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View("~/Views/Area/Create.cshtml", areaCreateDto);
        }

        [HttpGet("edit")]
        public async Task<ActionResult> Edit()
        {
            var puertas = await this._portalQuery.GetAll();
            ViewData["puertas"] = puertas;
            return View("~/Views/Area/Create.cshtml");
        }

        [HttpPut("edit")]
        public async Task<ActionResult> Update()
        {
            var puertas = await this._portalQuery.GetAll();
            ViewData["puertas"] = puertas;
            return View("~/Views/Area/Create.cshtml");
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete()
        {
            var puertas = await this._portalQuery.GetAll();
            ViewData["puertas"] = puertas;
            return View("~/Views/Area/Create.cshtml");
        }
        private async Task<Boolean> loginControlId()
        {
            BodyLogin cuerpo = _loginControlIdQuery.Login(this.user, this.password);
            Response login = await this._httpClientService.LoginRun(this.controlador, this._apiRutas.ApiUrlLogin, cuerpo);
            this._httpClientService.session = login.data;
            return login.estado;
        }

        /*Extras*/
        private async Task<Response> SaveAreaControlId(AreaCreateDto areaCreateDto)
        {
            List<areaCreateDto> areaCreateControlIdDto = new List<areaCreateDto>();
            areaCreateControlIdDto.Add(
                new areaCreateDto()
                {
                    name = areaCreateDto.Nombre
                }
            );
            BodyCreateObject AddHorario = this._areaControlIdQuery.CreateAreas(areaCreateControlIdDto);
            Response responseAddHorario = await this._httpClientService.Run(controlador, this._apiRutas.ApiUrlCreate, AddHorario);
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
            Response responseUpdatePortal = await this._httpClientService.Run(controlador, this._apiRutas.ApiUrlUpdate, updatePortal);
            return responseUpdatePortal;
        }
    }
}