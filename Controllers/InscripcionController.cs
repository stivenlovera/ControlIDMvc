
using ControlIDMvc.Dtos.Caja;
using ControlIDMvc.Dtos.Inscripcion;
using ControlIDMvc.Dtos.Paquete;
using ControlIDMvc.Dtos.Utils;
using ControlIDMvc.Entities;
using ControlIDMvc.Querys;
using ControlIDMvc.ServicesCI;
using ControlIDMvc.ServicesCI.QueryCI;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using Microsoft.AspNetCore.Mvc;

namespace ControlIDMvc.Controllers
{
    [Route("inscripcion")]
    public class InscripcionController : Controller
    {
        private readonly PaqueteQuery _paqueteQuery;
        private readonly PersonaQuery _personaQuery;
        private readonly InscripcionQuery _inscripcionQuery;
        private readonly CajaQuery _cajaQuery;
        private readonly LoginControlIdQuery _loginControlIdQuery;
        private readonly HttpClientService _httpClientService;
        private readonly UsuarioControlIdQuery _usuarioControlIdQuery;
        private readonly DispositivoQuery _dispositivoQuery;
        ApiRutas _ApiRutas;
        public InscripcionController(
            PaqueteQuery PaqueteQuery,
            PersonaQuery personaQuery,
            InscripcionQuery inscripcionQuery,
            CajaQuery cajaQuery,
            /*controlId*/
            LoginControlIdQuery loginControlIdQuery,
            HttpClientService httpClientService,
            UsuarioControlIdQuery usuarioControlIdQuery,
            DispositivoQuery dispositivoQuery
        )
        {
            this._paqueteQuery = PaqueteQuery;
            this._personaQuery = personaQuery;
            this._inscripcionQuery = inscripcionQuery;
            this._cajaQuery = cajaQuery;
            this._loginControlIdQuery = loginControlIdQuery;
            this._httpClientService = httpClientService;
            this._usuarioControlIdQuery = usuarioControlIdQuery;
            this._dispositivoQuery = dispositivoQuery;
            this._ApiRutas = new ApiRutas();
        }
        [HttpGet]
        public ActionResult Index(string message)
        {
            ViewData["message"] = message;
            return View("~/Views/Inscripcion/Lista.cshtml");
        }

        [HttpPost("data-table")]
        public ActionResult DataTable()
        {
            var dataTable = this._inscripcionQuery.DataTable(Request);
            return Json(dataTable);
        }

        [HttpGet("factura-recibo/{id:int}")]
        public ActionResult PreviewRecibo(int id)
        {
            return View("~/Views/Inscripcion/PriviewInscripcion.cshtml");
        }

        [HttpGet("create")]
        public async Task<ActionResult> Create()
        {
            var paquetes = await this._paqueteQuery.GetAll();
            ViewData["paquetes"] = paquetes;
            var personas = await this._personaQuery.GetAll();
            ViewData["personas"] = personas;
            DateTime fechaRecibo = DateTime.Now;
            fechaRecibo.ToString("yyyyMMdd");
            ViewData["numeroRecibo"] = fechaRecibo.ToString("MMddHHmmss");
            return View("~/Views/Inscripcion/Create.cshtml");
        }

        [HttpPost("store")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Store(InscripcionCreateDto InscripcionCreateDto)
        {
            //regeneracion de parametros requeridos
            var paquetes = await this._paqueteQuery.GetAll();
            ViewData["paquetes"] = paquetes;
            var personas = await this._personaQuery.GetAll();
            ViewData["personas"] = personas;
            DateTime fechaRecibo = DateTime.Now;
            fechaRecibo.ToString("yyyyMMdd");
            ViewData["numeroRecibo"] = fechaRecibo.ToString("MMddHHmmss");

            if (ModelState.IsValid)
            {
                var inscripcion = await this._inscripcionQuery.Store(InscripcionCreateDto);
                var caja = await this.addCash(inscripcion);
                //update dispositivo 
                var persona = await this._personaQuery.GetOne(InscripcionCreateDto.PersonaId);
                persona.ControlIdBegin_time = this.DateTimeToUnix(InscripcionCreateDto.FechaInicio);
                persona.ControlIdEnd_time = this.DateTimeToUnix(InscripcionCreateDto.FechaFin);
                var updateFecha = await this._personaQuery.UpdateOne(persona);
                await this.UpdateDispositivo(updateFecha);
                return RedirectToAction("PreviewRecibo", new { id = 1 });
            }
            return View("~/Views/Inscripcion/Create.cshtml");
        }

        [HttpPost("store-recibo")]
        [ValidateAntiForgeryToken]
        public ActionResult StoreInscripcion()
        {
            return RedirectToAction("Index", new { message = "Nueva inscripcion registrada" });
        }

        [HttpGet("show/{id:int}")]
        public async Task<ActionResult> Show(int id)
        {
            var inscripcion = await this._inscripcionQuery.Edit(id);
            return View("~/Views/Inscripcion/Create.cshtml");
        }

        [HttpGet("edit/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            var paquetes = await this._paqueteQuery.GetAll();
            ViewData["paquetes"] = paquetes;
            var personas = await this._personaQuery.GetAll();
            ViewData["personas"] = personas;
            var inscripcion = await this._inscripcionQuery.Edit(id);
            ViewData["numeroRecibo"] = inscripcion.NumeroRecibo;

            InscripcionUpdateDto inscripcionUpdateDto = new InscripcionUpdateDto
            {
                Id = inscripcion.Id,
                Nombres = inscripcion.Persona.Nombre,
                Apellidos = inscripcion.Persona.Apellido,
                CI = inscripcion.Persona.Ci,
                Costo = inscripcion.Paquete.Costo,
                Dias = inscripcion.Paquete.Dias,
                FechaCreacion = inscripcion.FechaCreacion,
                FechaFin = inscripcion.FechaFin,
                FechaInicio = inscripcion.FechaInicio,
                PaqueteId = inscripcion.PaqueteId,
                PaqueteNombre = inscripcion.Paquete.Nombre,
                PersonaId = inscripcion.PersonaId
            };
            return View("~/Views/Inscripcion/Edit.cshtml", inscripcionUpdateDto);
        }

        [HttpPost("update/{id:int}")]
        public async Task<ActionResult> Update(int id, InscripcionUpdateDto inscripcionUpdateDto)
        {
            //regeneracion requerida
            var paquetes = await this._paqueteQuery.GetAll();
            ViewData["paquetes"] = paquetes;
            var personas = await this._personaQuery.GetAll();
            ViewData["personas"] = personas;
            var inscripcion = await this._inscripcionQuery.Edit(id);
            ViewData["numeroRecibo"] = inscripcion.NumeroRecibo;
            if (ModelState.IsValid)
            {
                var data = new Inscripcion
                {
                    Id=inscripcionUpdateDto.Id,
                    PersonaId = inscripcionUpdateDto.Id,
                    Costo = inscripcionUpdateDto.Costo,
                    FechaFin = inscripcionUpdateDto.FechaFin,
                    FechaInicio = inscripcionUpdateDto.FechaInicio,
                    NumeroRecibo = inscripcionUpdateDto.NumeroRecibo,
                    PaqueteId = inscripcionUpdateDto.PaqueteId,

                };
                var updateInscripcion = await this._inscripcionQuery.Update(data, id);
                //update dispositivo
                var persona = await this._personaQuery.GetOne(inscripcionUpdateDto.PersonaId);
                persona.ControlIdBegin_time = this.DateTimeToUnix(inscripcionUpdateDto.FechaInicio);
                persona.ControlIdEnd_time = this.DateTimeToUnix(inscripcionUpdateDto.FechaFin);
                var updateFecha = await this._personaQuery.UpdateOne(persona);
                await this.UpdateDispositivo(updateFecha);
                return RedirectToAction("PreviewRecibo", new { id = 1 });
            }
            var aux=ModelState.Values;
            return View("~/Views/Inscripcion/Edit.cshtml", inscripcionUpdateDto);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> Destroy(int id)
        {
            var inscripcion = await this._inscripcionQuery.Delete(id);
            if (inscripcion)
            {
                return Json(
                    new
                    {
                        status = "success",
                        message = "Eliminado correctamente",
                    }
                );
            }
            else
            {
                return Json(
                    new
                    {
                        status = "error",
                        message = "ocurrio un error",
                    }
                );
            }
        }

        [HttpPost("select-paquetes")]
        public async Task<List<PaqueteDto>> Select(SelectDto selectDto)
        {
            List<PaqueteDto> paquetes = new List<PaqueteDto>();
            System.Console.WriteLine(String.IsNullOrEmpty(selectDto.searchTerm));
            if (String.IsNullOrEmpty(selectDto.searchTerm))
            {
                paquetes = await this._paqueteQuery.GetAll();
            }
            else
            {
                paquetes = await this._paqueteQuery.GetAllLike(selectDto.searchTerm);

            }
            return paquetes;
        }
        /*Utils*/
        private long DateTimeToUnix(DateTime MyDateTime)
        {
            TimeSpan timeSpan = MyDateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            var data = (long)timeSpan.TotalSeconds;
            System.Console.WriteLine(data);
            return data;
        }
        /*
        * Modelo de negocio
        */
        private async Task<bool> addCash(InscripcionDto inscripcionDto)
        {
            var egresoCaja = new CajaCreateDto
            {
                Concepto = $"pago inscripcion",
                Fecha = inscripcionDto.FechaCreacion,
                NumeroRecibo = inscripcionDto.NumeroRecibo,
                Persona = "cliente x",
                Tipo = "ingreso",
                Valor = inscripcionDto.Costo
            };
            var caja = await this._cajaQuery.Store(egresoCaja);
            if (caja != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /*Controlador de fechas*/
        private async Task<bool> LoginControlId(string ip, int port, string user, string api, string password)
        {
            BodyLogin cuerpo = _loginControlIdQuery.Login(user, password);
            Response login = await this._httpClientService.LoginRun(ip, port, api, cuerpo, "");
            /*valido si es el login fue ok*/
            this._usuarioControlIdQuery.Params(port, ip, user, password, login.data);
            return login.estado;
        }
        /*------USUARIO------*/
        private async Task<bool> UpdateDispositivo(Persona persona)
        {
            /*consutar por dispositivos*/
            var dispositivos = await this._dispositivoQuery.GetAll();
            foreach (var dispositivo in dispositivos)
            {
                var loginStatus = await this.LoginControlId(dispositivo.Ip, dispositivo.Puerto, dispositivo.Usuario, this._ApiRutas.ApiUrlLogin, dispositivo.Password);
                if (loginStatus)
                {
                    //modificar usuario
                    await this.UserUpdateControlId(persona);

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
    }
}