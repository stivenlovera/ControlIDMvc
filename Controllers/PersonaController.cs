
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

namespace ControlIDMvc.Controllers;

[Route("persona")]
public class PersonaController : Controller
{
    /* propiedades */
    public string controlador = "192.168.88.129";
    public string uri = "login.fcgi";
    public string user = "admin";
    public string password = "admin";
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClientService _httpClientService;
    private PersonaQuery _personaQuery;
    private readonly TarjetaQuery _tarjetaQuery;
    private readonly LoginControlIdQuery _loginControlIdQuery;
    private readonly UsuarioControlIdQuery _usuarioControlIdQuery;
    private readonly CardControlIdQuery _cardControlIdQuery;

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
        this._loginControlIdQuery.ApiUrl = "login.fcgi";
        this._usuarioControlIdQuery.ApiUrl = "create_objects.fcgi";
        this._cardControlIdQuery.ApiUrl = "create_objects.fcgi";
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

    [HttpPost("store")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Post(PersonaCreateDto personaCreateDto)
    {
        var existCI = await this._personaQuery.ValidarUsuario(personaCreateDto.Ci);
        if (!existCI)
        {
            return NotFound();
        }

        BodyLogin cuerpo = _loginControlIdQuery.Login(this.user, this.password);
        Response login = await this._httpClientService.LoginRun(controlador, this._loginControlIdQuery.ApiUrl, cuerpo);
        this._httpClientService.session = login.data;
        if (login.estado)
        {
            List<PersonaCreateDto> personas = new List<PersonaCreateDto>();
            personas.Add(personaCreateDto);

            BodyCreateObject AddUsers = this._usuarioControlIdQuery.CreateUser(personas);
            Response responseAddUsers = await this._httpClientService.Run(controlador, this._usuarioControlIdQuery.ApiUrl, AddUsers);
            if (responseAddUsers.estado)
            {
                usersResponseDto responseUser = JsonConvert.DeserializeObject<usersResponseDto>(responseAddUsers.data);
                personaCreateDto.Sincronizacion = "si";

                var storePersona = await this._personaQuery.Store(personaCreateDto);

                if (personaCreateDto.Area!= null)
                {
                    BodyCreateObject AddCards = this._cardControlIdQuery.CreateCards(personas, responseUser.ids);
                    Response responseAddCards = await this._httpClientService.Run(controlador, this._cardControlIdQuery.ApiUrl, AddCards);
                    cardsResponseDto responseCards = JsonConvert.DeserializeObject<cardsResponseDto>(responseAddCards.data);

                    int i = 0;
                    foreach (var area in personaCreateDto.Area)
                    {
                        TarjetaCreateDto tarjetaCreateDto = new TarjetaCreateDto();
                        tarjetaCreateDto.Sincronizacion = "si";
                        tarjetaCreateDto.PersonaId = storePersona.Id;
                        tarjetaCreateDto.Area = Int32.Parse(area);
                        tarjetaCreateDto.Codigo = Int32.Parse(personaCreateDto.Codigo[i]);
                        var storeTarjeta = await this._tarjetaQuery.Store(tarjetaCreateDto);
                        i++;
                    }
                }
            }
        }
        else
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
        }
        return RedirectToAction(nameof(Index));
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
}

