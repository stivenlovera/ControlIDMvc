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
        public string uri = "login.fcgi";
        public string user = "admin";
        public string password = "admin";
        private readonly DBContext _dbContext;
        private readonly HorarioQuery _horarioQuery;
        private readonly LoginControlIdQuery _loginControlIdQuery;
        private readonly HttpClientService _httpClientService;
        private readonly HorarioControlIdQuery _horarioControlIdQuery;

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
            this._loginControlIdQuery.ApiUrl = "login.fcgi";
            this._horarioControlIdQuery.ApiUrl = "create_objects.fcgi";
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
        public async Task<ActionResult> Store([FromForm] HorarioForm horarioForm)
        {
            BodyLogin cuerpo = this._loginControlIdQuery.Login(this.user, this.password);
            Response login = await this._httpClientService.LoginRun(controlador, this._loginControlIdQuery.ApiUrl, cuerpo);
            this._httpClientService.session = login.data;
            if (login.estado)
            {
                time_zonesCreateDto time_ZonesCreateDto = new time_zonesCreateDto();
                time_ZonesCreateDto.name = horarioForm.nombre;

                BodyCreateObject AddHorario = this._horarioControlIdQuery.CreateHorario(time_ZonesCreateDto);
                Response responseAddHorario = await this._httpClientService.Run(controlador, this._horarioControlIdQuery.ApiUrl, AddHorario);
                if (responseAddHorario.estado)
                {
                    time_zonesResponseDto responseHorario = JsonConvert.DeserializeObject<time_zonesResponseDto>(responseAddHorario.data);

                    List<time_spansCreateDto> dias = new List<time_spansCreateDto>();
                    
                    int i = 0;
                    foreach (var dia in horarioForm.dia)
                    {
                        //System.Console.WriteLine(Convert.ToInt32(horarioForm.hora_inicio[i].Minute));
                        var hora_inicio= Convert.ToDateTime(horarioForm.hora_inicio[i]);
                        var hora_fin= Convert.ToDateTime(horarioForm.hora_fin[i]);
                        time_spansCreateDto time_SpansCreateDto = new time_spansCreateDto
                        {
                            start = (Convert.ToInt32(hora_inicio.Minute)*Convert.ToInt32(hora_inicio.Second)*Convert.ToInt32(hora_inicio.Hour)),
                            end = (Convert.ToInt32(hora_fin.Minute)*Convert.ToInt32(hora_fin.Second)*Convert.ToInt32(hora_fin.Hour)),
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
                    Response responseAddHorarioDias = await this._httpClientService.Run(controlador, this._horarioControlIdQuery.ApiUrl, AddHorario);
                    
                    if (responseAddHorario.estado)
                    {
                        var storePersona = await this._horarioQuery.store(horarioForm);
                    }

                }
            }
            /* else
            {
                personaCreateDto.Sincronizacion = "no";
                var storePersona = await this._personaQuery.Store(personaCreateDto);
                int i = 0;
                foreach (var card in personaCreateDto.Area)
                {
                    TarjetaCreateDto tarjetaCreateDto = new TarjetaCreateDto();
                    tarjetaCreateDto.Sincronizacion = "no";
                    tarjetaCreateDto.PersonaId = storePersona.Id;
                    tarjetaCreateDto.Codigo = Int32.Parse(personaCreateDto.Codigo[i]);
                    var storeTarjeta = await this._tarjetaQuery.Store(tarjetaCreateDto);
                }
            } */
            return RedirectToAction(nameof(Index));
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
    }
}