using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ControlIDMvc.Models.DatatableModel;
using ControlIDMvc.Entities;
using ControlIDMvc.Models.ModelForm;
using Microsoft.EntityFrameworkCore;
using ControlIDMvc.Dtos.Horario;
using ControlIDMvc.Querys;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using ControlIDMvc.ServicesCI.QueryCI;
using ControlIDMvc.ServicesCI;
using ControlIDMvc.ServicesCI.Dtos.time_zonesDto;
using Newtonsoft.Json;
using ControlIDMvc.ServicesCI.Dtos.time_spansDto;

namespace ControlIDMvc.Controllers
{
    [Route("horario")]
    public class HorarioController : Controller
    {
        /* propiedades */
        public string controlador = "192.168.88.129";
        public string user = "admin";
        public string password = "admin";
        private readonly DBContext _dbContext;
        private readonly HorarioQuery _horarioQuery;
        private readonly LoginControlIdQuery _loginControlIdQuery;
        private readonly HttpClientService _httpClientService;
        private readonly HorarioControlIdQuery _horarioControlIdQuery;
        ApiRutas _apiRutas;
        public HorarioController(
            DBContext dbContext,
            HorarioQuery HorarioQuery,
            LoginControlIdQuery loginControlIdQuery,
            HttpClientService httpClientService,
            HorarioControlIdQuery horarioControlIdQuery
            )
        {
            this._dbContext = dbContext;
            this._horarioQuery = HorarioQuery;
            this._loginControlIdQuery = loginControlIdQuery;
            this._httpClientService = httpClientService;
            this._horarioControlIdQuery = horarioControlIdQuery;
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
            return View("~/Views/Horario/Lista.cshtml");
        }
        [HttpGet("create")]
        public ActionResult Create()
        {
            return View("~/Views/Horario/Create.cshtml");
        }

        [HttpPost("data-table")]
        public async Task<ActionResult> Datatable()
        {
            var dataTable = await this._horarioQuery.Datatable(Request);
            return Json(dataTable);
        }
        [HttpPost("store")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Store([FromForm] HorarioCreateDto horarioCreateDto)
        {
            if (ModelState.IsValid)
            {
                if (await this.loginControlId())
                {
                    time_zonesCreateDto time_ZonesCreateDto = new time_zonesCreateDto();
                    time_ZonesCreateDto.name = horarioCreateDto.Nombre;

                    BodyCreateObject AddHorario = this._horarioControlIdQuery.CreateHorario(time_ZonesCreateDto);
                    Response responseAddHorario = await this._httpClientService.Run(controlador, this._apiRutas.ApiUrlCreate, AddHorario);
                    if (responseAddHorario.estado)
                    {
                        time_zonesResponseDto responseHorario = JsonConvert.DeserializeObject<time_zonesResponseDto>(responseAddHorario.data);
                        horarioCreateDto.ControlId = responseHorario.ids[0].ToString();

                        List<time_spansCreateDto> dias = new List<time_spansCreateDto>();
                        int i = 0;
                        foreach (var dia in horarioCreateDto.Dias)
                        {
                            var hora_inicio = Convert.ToDateTime(horarioCreateDto.Hora_inicio[i]);
                            var hora_fin = Convert.ToDateTime(horarioCreateDto.Hora_fin[i]);

                            var cal_hora_inicio = Convert.ToInt32(hora_inicio.Hour == 00 ? 60 : hora_inicio.Hour) * Convert.ToInt32(hora_inicio.Minute == 00 ? 60 : hora_inicio.Minute * 60) /* * Convert.ToInt32(hora_inicio.Second) */;
                            var cal_hora_fin = Convert.ToInt32(hora_fin.Hour == 00 ? 60 : hora_fin.Hour) * Convert.ToInt32(hora_fin.Minute == 00 ? 60 : hora_fin.Minute * 60) /* * Convert.ToInt32(hora_fin.Second); */;
                            time_spansCreateDto time_SpansCreateDto = new time_spansCreateDto
                            {
                                start = cal_hora_inicio,
                                end = cal_hora_fin,
                                sun = dia == "sabado" ? 1 : 0,
                                mon = dia == "lunes" ? 1 : 0,
                                tue = dia == "martes" ? 1 : 0,
                                wed = dia == "miercoles" ? 1 : 0,
                                thu = dia == "jueves" ? 1 : 0,
                                fri = dia == "viernes" ? 1 : 0,
                                sat = dia == "sabado" ? 1 : 0,
                                hol1 = 0,
                                hol2 = 0,
                                hol3 = 0,
                                time_zone_id = responseHorario.ids[0]
                            };
                            dias.Add(time_SpansCreateDto);
                            i++;
                        }
                        BodyCreateObject AddHorarioDias = this._horarioControlIdQuery.CreateDias(dias, responseHorario.ids[0]);
                        Response responseAddHorarioDias = await this._httpClientService.Run(controlador, this._apiRutas.ApiUrlCreate, AddHorarioDias);

                        if (responseAddHorario.estado)
                        {
                            horarioResponseDto responseHorarioDias = JsonConvert.DeserializeObject<horarioResponseDto>(responseAddHorarioDias.data);
                            int index = 0;
                            horarioCreateDto.DiasControlId= new List<string>();
                            foreach (var dia in horarioCreateDto.Dias)
                            {
                                horarioCreateDto.DiasControlId.Add((responseHorarioDias.ids[index]).ToString());
                            }
                            var storePersona = await this._horarioQuery.store(horarioCreateDto);
                        }
                    }
                }
                else
                {
                    var storePersona = await this._horarioQuery.store(horarioCreateDto);
                }
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Horario/Create.cshtml");
        }
        /*  [HttpGet("editar/{id:int}")]
         public ActionResult Edit(int id)
         {
             var persona = _dbContext.Persona.Find(id);
             if (persona == null)
             {
                 return NotFound();
             }
             return View("~/Views/Persona/Edit.cshtml", persona);
         } */
        /*fuciones separadas*/
    }
}