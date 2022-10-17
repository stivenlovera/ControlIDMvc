
using Microsoft.AspNetCore.Mvc;
using ControlIDMvc.ServicesCI;
using ControlIDMvc.Querys;
using ControlIDMvc.ServicesCI.UtilidadesCI;

using ControlIDMvc.ServicesCI.QueryCI;
using ControlIDMvc.ServicesCI.Dtos.usersDto;
using Newtonsoft.Json;
using ControlIDMvc.Dtos;
using ControlIDMvc.ServicesCI.Dtos.cardsDto;
using ControlIDMvc.Dtos.Tarjeta;
using ControlIDMvc.Dtos.Persona;
using Microsoft.AspNetCore.Authorization;

namespace ControlIDMvc.Controllers;


[Route("persona")]
public class PersonaController : Controller
{
    /* propiedades */
    public string controlador = "192.168.88.129";
    public string user = "admin";
    public string password = "admin";
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClientService _httpClientService;
    private PersonaQuery _personaQuery;
    private readonly TarjetaQuery _tarjetaQuery;
    private readonly LoginControlIdQuery _loginControlIdQuery;
    private readonly UsuarioControlIdQuery _usuarioControlIdQuery;
    private readonly CardControlIdQuery _cardControlIdQuery;
    ApiRutas _ApiRutas;
    public PersonaController(
        ILogger<HomeController> logger,
        HttpClientService httpClientService,
        PersonaQuery personaQuery,
        TarjetaQuery tarjetaQuery,
        LoginControlIdQuery loginControlIdQuery,
        UsuarioControlIdQuery usuarioControlIdQuery,
        CardControlIdQuery cardControlIdQuery
        )
    {
        this._httpClientService = httpClientService;

        this._personaQuery = personaQuery;
        this._tarjetaQuery = tarjetaQuery;

        this._usuarioControlIdQuery = usuarioControlIdQuery;
        this._cardControlIdQuery = cardControlIdQuery;
        this._logger = logger;

        this._loginControlIdQuery = loginControlIdQuery;
        this._ApiRutas = new ApiRutas();
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var personas = await this._personaQuery.GetAll();
        return View("~/Views/Persona/Lista.cshtml");
    }

    [HttpGet("create")]
    public ActionResult Create()
    {
        return View("~/Views/Persona/Create.cshtml");
    }
    private async Task<Boolean> loginControlId()
    {
        BodyLogin cuerpo = _loginControlIdQuery.Login(this.user, this.password);
        Response login = await this._httpClientService.LoginRun(this.controlador, this._ApiRutas.ApiUrlLogin, cuerpo);
        this._httpClientService.session = login.data;
        return login.estado;
    }

    [HttpPost("store")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Post(PersonaCreateDto personaCreateDto)
    {
        if (ModelState.IsValid)
        {
            if (await this._personaQuery.ValidarUsuario(personaCreateDto.Ci))
            {
                if (await this.loginControlId())
                {
                    ViewData["Verificar CI"] = "Carnet de identidad ya se encuentra registrada";
                    List<usersCreateDto> personas = new List<usersCreateDto>();
                    personas.Add(new usersCreateDto
                    {
                        name = personaCreateDto.Nombre,
                        password = "",
                        registration = "",
                        salt = ""
                    });

                    BodyCreateObject AddUsers = this._usuarioControlIdQuery.CreateUser(personas);
                    Response responseAddUsers = await this._httpClientService.Run(this.controlador, this._ApiRutas.ApiUrlCreate, AddUsers);
                    if (responseAddUsers.estado)
                    {
                        usersResponseDto responseUser = JsonConvert.DeserializeObject<usersResponseDto>(responseAddUsers.data);
                        personaCreateDto.ControlId = responseUser.ids[0].ToString();

                        var storePersona = await this._personaQuery.Store(personaCreateDto);

                        if (personaCreateDto.Area != null)
                        {
                            /*tarjeta*/
                            List<cardsCreateDto> tarjetas = new List<cardsCreateDto>();
                            int index = 0;
                            foreach (var area in personaCreateDto.Area)
                            {
                                int area_convert = Int32.Parse(area);
                                int area_codigo = Int32.Parse(personaCreateDto.Codigo[index]);
                                long calculo = (area_convert * Convert.ToInt64((Math.Pow(2, 32)))) + area_codigo;
                                tarjetas.Add(
                                    new cardsCreateDto
                                    {
                                        user_id = Convert.ToInt32(storePersona.ControlId),
                                        value = calculo
                                    }
                                );
                            }
                            BodyCreateObject AddCards = this._cardControlIdQuery.CreateCards(tarjetas);
                            Response responseAddCards = await this._httpClientService.Run(this.controlador, this._ApiRutas.ApiUrlCreate, AddCards);
                            if (responseAddCards.estado)
                            {
                                cardsResponseDto responseCards = JsonConvert.DeserializeObject<cardsResponseDto>(responseAddCards.data);
                                int i = 0;
                                foreach (var id in responseCards.ids)
                                {
                                    TarjetaCreateDto tarjetaCreateDto = new TarjetaCreateDto();
                                    tarjetaCreateDto.PersonaId = storePersona.Id;
                                    tarjetaCreateDto.Area = Int32.Parse(personaCreateDto.Area[i]);
                                    tarjetaCreateDto.Codigo = Int32.Parse(personaCreateDto.Codigo[i]);
                                    var storeTarjeta = await this._tarjetaQuery.Store(tarjetaCreateDto);
                                    i++;
                                }
                            }
                        }
                    }
                }
                else
                {
                    ViewData["ErrorConexion"] = "Error de conexion con el dipositivo";
                    return View("~/Views/Persona/Create.cshtml", personaCreateDto);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["Error"] = "CI ya fue registrado";
                return View("~/Views/Persona/Create.cshtml", personaCreateDto);
            }
        }
        return View("~/Views/Persona/Create.cshtml", personaCreateDto);
    }
    [HttpPost("data-table")]
    public ActionResult DataTable()
    {
        var dataTable = this._personaQuery.DataTable(Request);
        return Json(dataTable);
    }

    [HttpGet("editar/{id:int}")]
    public async Task<ActionResult> Edit(int id)
    {
        var persona = await this._personaQuery.GetOne(id);
        if (persona == null)
        {
            return NotFound();
        }
        return View("~/Views/Persona/Edit.cshtml", persona);
    }
    [HttpGet("update/{id:int}")]
    public async Task<ActionResult> Update(int id, PersonaUpdateDto personaCreateDto)
    {
        if (ModelState.IsValid)
        {
            if (await this._personaQuery.ValidarUsuario(personaCreateDto.Ci))
            {

            }
            else
            {
                ViewData["Error"] = "CI ya fue registrado";
                return View("~/Views/Persona/Edit.cshtml", personaCreateDto);
            }
        }
        return View("~/Views/Persona/Edit.cshtml", personaCreateDto);
    }
    [HttpPost("buscar")]
    public async Task<ActionResult> buscar(PersonaCreateDto personaCreateDto)
    {
        var personas = await this._personaQuery.GetAllLikeCi(Convert.ToInt32(personaCreateDto.Ci));

        List<object> lista_personas = new List<object>();

        foreach (var persona in personas)
        {
            var fecha_inicio = this.UnixTimeStampToDateTime(persona.ControlIdBegin_time);
            var fecha_fin = this.UnixTimeStampToDateTime(persona.ControlIdEnd_time);
            lista_personas.Add(new
            {
                id = persona.Id,
                ci = persona.Ci,
                nombre = persona.Nombre,
                apellido = persona.Apellido,
                fecha_inicio = fecha_inicio,
                fecha_fin = fecha_fin
            });
        }
        return Json(lista_personas);
    }
    [HttpPost("store-ajax")]
    public async Task<ActionResult> StoreAjax(PersonaCreateDto personaCreateDto)
    {
        var storePersona = await this._personaQuery.Store(personaCreateDto);

        return Json(
            new
            {
                status = "success",
                data=storePersona,
                message = "Persona creado correctamente"
            }
        );
    }
    private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {

        // Unix timestamp son los segundos pasados después de una fecha establecida, por lo general unix utiliza esta fecha 
        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dtDateTime;
    }
    private long DateTimeToUnix(DateTime MyDateTime)
    {
        TimeSpan timeSpan = MyDateTime - new DateTime(2022, 9, 10, 0, 0, 0);

        return (long)timeSpan.TotalSeconds;
    }
}

