
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
using ControlIDMvc.Entities;
using System.Web;

namespace ControlIDMvc.Controllers;

[Route("persona")]
public class PersonaController : Controller
{
    /* propiedades */
    public string controlador = "192.168.88.129";
    public string user = "admin";
    public string password = "admin";
    public int port { get; set; }
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClientService _httpClientService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private PersonaQuery _personaQuery;
    private readonly TarjetaQuery _tarjetaQuery;
    private readonly LoginControlIdQuery _loginControlIdQuery;
    private readonly UsuarioControlIdQuery _usuarioControlIdQuery;
    private readonly CardControlIdQuery _cardControlIdQuery;
    private readonly DispositivoQuery _dispositivoQuery;
    ApiRutas _ApiRutas;
    public PersonaController(
        ILogger<HomeController> logger,
        HttpClientService httpClientService,
        IWebHostEnvironment webHostEnvironment,
        PersonaQuery personaQuery,
        TarjetaQuery tarjetaQuery,
        LoginControlIdQuery loginControlIdQuery,
        UsuarioControlIdQuery usuarioControlIdQuery,
        CardControlIdQuery cardControlIdQuery,
        DispositivoQuery dispositivoQuery
        )
    {
        this._httpClientService = httpClientService;
        this._webHostEnvironment = webHostEnvironment;
        this._personaQuery = personaQuery;
        this._tarjetaQuery = tarjetaQuery;

        this._usuarioControlIdQuery = usuarioControlIdQuery;
        this._cardControlIdQuery = cardControlIdQuery;
        this._dispositivoQuery = dispositivoQuery;
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
        Response login = await this._httpClientService.LoginRun(this.controlador, this.port, this._ApiRutas.ApiUrlLogin, cuerpo, "");
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
                //save img
                if (personaCreateDto.perfil != null)
                {
                    string folder = "images/perfiles";
                    folder += Guid.NewGuid().ToString() + personaCreateDto.perfil.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await personaCreateDto.perfil.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

                }
                /*insertar*/
                personaCreateDto.ControlIdSalt = "";
                personaCreateDto.ControlIdRegistration = "";
                personaCreateDto.ControlIdName=personaCreateDto.Nombre;
                var persona = await this._personaQuery.Store(personaCreateDto);
                if (personaCreateDto.Area != null)
                {
                    int index = 0;
                    foreach (var area in personaCreateDto.Area)
                    {
                        TarjetaCreateDto tarjetaCreateDto = new TarjetaCreateDto
                        {
                            Area = Convert.ToInt32(area),
                            Codigo = Convert.ToInt32(personaCreateDto.Area[index]),
                            ControlId = "",
                            PersonaId = persona.Id,
                            Sincronizacion = "no"
                        };
                        await this._tarjetaQuery.Store(tarjetaCreateDto);
                        index++;
                    }
                }
                await this.SaveDispositivo(persona);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["Error"] = "CI ya fue registrado";
                return View("~/Views/Persona/Create.cshtml", personaCreateDto);
            }
        }
        else
        {
            return View("~/Views/Persona/Create.cshtml", personaCreateDto);
        }
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
        else
        {
            var editPersona = new PersonaDto
            {
                Id = persona.Id,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Area = null,
                Celular = persona.Celular,
                Ci = persona.Ci,
                Codigo = null,
                Dirrecion = persona.Dirrecion,
                Email = persona.Email,
                Fecha_nac = persona.Fecha_nac,
                Observaciones = persona.Observaciones,
                ControlIdPassword = persona.ControlIdPassword
            };
            return View("~/Views/Persona/Edit.cshtml", editPersona);
        }
    }

    [HttpPost("update/{id:int}")]
    public async Task<ActionResult> Update(int id, PersonaDto personaDto)
    {
        if (ModelState.IsValid)
        {
            if (await this._personaQuery.ValidateExistExceptoId(personaDto.Ci, id))
            {
                var personaUpdate = new Persona
                {
                    Id = id,
                    Celular = personaDto.Celular,
                    Apellido = personaDto.Apellido,
                    Ci = personaDto.Ci,
                    Nombre = personaDto.Nombre,
                    Email = personaDto.Email,
                    Observaciones = personaDto.Observaciones,
                    Dirrecion = personaDto.Dirrecion,
                    Fecha_nac = personaDto.Fecha_nac,
                    ControlIdPassword = personaDto.ControlIdPassword,
                    
                };
                var persona = await this._personaQuery.UpdateOne(personaUpdate);
                await this.UpdateDispositivo(persona);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["Error"] = "CI ya fue registrado";
                return View("~/Views/Persona/Edit.cshtml", personaDto);
            }
        }
        else
        {
            return View("~/Views/Persona/Edit.cshtml", personaDto);
        }
    }
    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        if (ModelState.IsValid)
        {
            var persona = await this._personaQuery.GetOne(id);
            var delete = await this._personaQuery.Delete(persona.Id);
            await this.DeleteDispositivo(persona);
        }
        return Json(new
        {
            status = "success",
            message = "Eliminado correctamente",
        });
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
                data = storePersona,
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

    /*
        *CARGAR DATA CONTROL ID 
    */
    /*login dispositivo*/
    private async Task<bool> LoginControlId(string ip, int port, string user, string api, string password)
    {
        BodyLogin cuerpo = _loginControlIdQuery.Login(user, password);
        Response login = await this._httpClientService.LoginRun(ip, port, api, cuerpo, "");
        /*valido si es el login fue ok*/
        this._usuarioControlIdQuery.Params(port, ip, user, password, login.data);
        this._cardControlIdQuery.Params(port, ip, user, password, login.data);
        return login.estado;
    }
    /*------USUARIO------*/
    private async Task<bool> SaveDispositivo(Persona persona)
    {
        /*consutar por dispositivos*/
        var dispositivos = await this._dispositivoQuery.GetAll();
        foreach (var dispositivo in dispositivos)
        {
            var loginStatus = await this.LoginControlId(dispositivo.Ip, dispositivo.Puerto, dispositivo.Usuario, this._ApiRutas.ApiUrlLogin, dispositivo.Password);
            if (loginStatus)
            {
                //crear usuario
                await this.UserStoreControlId(persona);
                //crear tarjetas si hay 
                if (persona.card != null)
                {
                    await this.CardStoreControlId(persona);
                }
            }
        }
        return true;
    }

    private async Task<bool> UserStoreControlId(Persona persona)
    {
        var addUsuario = await this._usuarioControlIdQuery.CreateOneUser(persona);
        if (addUsuario.status)
        {
            persona.ControlId = addUsuario.ids[0].ToString();
            var updateUsuario = await this._personaQuery.UpdateOne(persona);
        }
        return addUsuario.status;
    }
    /*------CARD------*/
    private cardsCreateDto ConvertCard(int usuarioId, string area, string codigo)
    {
        int area_convert = Int32.Parse(area);
        int area_codigo = Int32.Parse(codigo);
        long calculo = (area_convert * Convert.ToInt64((Math.Pow(2, 32)))) + area_codigo;
        var card = new cardsCreateDto
        {
            user_id = usuarioId,
            value = calculo
        };
        return card;
    }
    private async Task<bool> CardStoreControlId(Persona persona)
    {
        List<cardsCreateDto> cardsCreateDto = new List<cardsCreateDto>();
        if (persona.card.Count > 0)
        {
            foreach (var card in persona.card)
            {
                var newCard = this.ConvertCard(Convert.ToInt32(persona.ControlId), card.area.ToString(), card.codigo.ToString());
                cardsCreateDto.Add(newCard);
                var createCard = await this._cardControlIdQuery.CreateCards(cardsCreateDto);
                if (createCard.status)
                {
                    card.ControlId = createCard.ids[0].ToString();
                    var updateCard = await this._tarjetaQuery.UpdateOne(card);
                }
            }
        }
        return true;
    }

    /*
       *UPDATE DATA CONTROL ID 
   */
    private async Task<bool> UpdateDispositivo(Persona persona)
    {
        /*consutar por dispositivos*/
        var dispositivos = await this._dispositivoQuery.GetAll();
        foreach (var dispositivo in dispositivos)
        {
            var loginStatus = await this.LoginControlId(dispositivo.Ip, dispositivo.Puerto, dispositivo.Usuario, this._ApiRutas.ApiUrlLogin, dispositivo.Password);
            if (loginStatus)
            {
                //crear usuario
                await this.UserUpdateControlId(persona);
                //crear tarjetas si hay 
                if (persona.card != null)
                {
                    //await this.CardUpdateControlId(persona);
                }
            }
        }
        return true;
    }
    private async Task<bool> UserUpdateControlId(Persona persona)
    {
        var addUsuario = await this._usuarioControlIdQuery.Update(persona);
        if (addUsuario.status)
        {
            //si el cambio due exitoso
            if (addUsuario.changes == 1)
            {
                var updateUsuario = await this._personaQuery.UpdateOne(persona);
            }

        }
        return addUsuario.status;
    }
    private async Task<bool> CardUpdateControlId(Persona persona)
    {
        List<cardsCreateDto> cardsCreateDto = new List<cardsCreateDto>();
        if (persona.card.Count > 0)
        {
            foreach (var card in persona.card)
            {
                var newCard = this.ConvertCard(Convert.ToInt32(persona.ControlId), card.area.ToString(), card.codigo.ToString());
                cardsCreateDto.Add(newCard);
                var createCard = await this._cardControlIdQuery.CreateCards(cardsCreateDto);
                if (createCard.status)
                {
                    card.ControlId = createCard.ids[0].ToString();
                    var updateCard = await this._tarjetaQuery.UpdateOne(card);
                }
            }
        }
        return true;
    }
    /*
       *DELETE DATA CONTROL ID 
    */
    private async Task<bool> DeleteDispositivo(Persona persona)
    {
        /*consutar por dispositivos*/
        var dispositivos = await this._dispositivoQuery.GetAll();
        foreach (var dispositivo in dispositivos)
        {
            var loginStatus = await this.LoginControlId(dispositivo.Ip, dispositivo.Puerto, dispositivo.Usuario, this._ApiRutas.ApiUrlLogin, dispositivo.Password);
            if (loginStatus)
            {
                //crear usuario
                await this.UserDeleteControlId(persona);
                //crear tarjetas si hay 
                if (persona.card != null)
                {
                    //await this.CardUpdateControlId(persona);
                }
            }
        }
        return true;
    }
    private async Task<bool> UserDeleteControlId(Persona persona)
    {
        var addUsuario = await this._usuarioControlIdQuery.Delete(persona);
        if (addUsuario.status)
        {
            //si el eliminacion due exitoso
        }
        return addUsuario.status;
    }
    private async Task<bool> CardDeleteControlId(Persona persona)
    {
        List<cardsCreateDto> cardsCreateDto = new List<cardsCreateDto>();
        if (persona.card.Count > 0)
        {
            foreach (var card in persona.card)
            {
                var newCard = this.ConvertCard(Convert.ToInt32(persona.ControlId), card.area.ToString(), card.codigo.ToString());
                cardsCreateDto.Add(newCard);
                var createCard = await this._cardControlIdQuery.CreateCards(cardsCreateDto);
                if (createCard.status)
                {
                    card.ControlId = createCard.ids[0].ToString();
                    var updateCard = await this._tarjetaQuery.UpdateOne(card);
                }
            }
        }
        return true;
    }
}

