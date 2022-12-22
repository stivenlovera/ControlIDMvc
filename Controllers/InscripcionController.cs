
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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using ControlIDMvc.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;
using ControlIDMvc.Models;

namespace ControlIDMvc.Controllers
{
    [Authorize]
    [Route("inscripcion")]
    public class InscripcionController : Controller
    {
        private readonly PaqueteQuery _paqueteQuery;
        private readonly PersonaQuery _personaQuery;
        private readonly InscripcionQuery _inscripcionQuery;
        private readonly CajaQuery _cajaQuery;
        private readonly PlanCuentaSubCuentaQuery _planCuentaSubCuentaQuery;
        private readonly MetodoPagoQuery _metodoPagoQuery;
        private readonly LoginControlIdQuery _loginControlIdQuery;
        private readonly HttpClientService _httpClientService;
        private readonly UsuarioControlIdQuery _usuarioControlIdQuery;
        private readonly DispositivoQuery _dispositivoQuery;
        private readonly MovimientoAsientoQuery _movimientoAsientoQuery;
        private readonly GeneratePDF _generatePDF;
        private readonly RazorRendererHelper _razorRendererHelper;
        ApiRutas _ApiRutas;
        public InscripcionController(
            PaqueteQuery PaqueteQuery,
            PersonaQuery personaQuery,
            InscripcionQuery inscripcionQuery,
            CajaQuery cajaQuery,
            PlanCuentaSubCuentaQuery planCuentaSubCuentaQuery,
            MetodoPagoQuery metodoPagoQuery,
            /*controlId*/
            LoginControlIdQuery loginControlIdQuery,
            HttpClientService httpClientService,
            UsuarioControlIdQuery usuarioControlIdQuery,
            DispositivoQuery dispositivoQuery,
            MovimientoAsientoQuery movimientoAsientoQuery,
            //documentos
            GeneratePDF generatePDF,
            RazorRendererHelper razorRendererHelper
        )
        {
            this._paqueteQuery = PaqueteQuery;
            this._personaQuery = personaQuery;
            this._inscripcionQuery = inscripcionQuery;
            this._cajaQuery = cajaQuery;
            this._planCuentaSubCuentaQuery = planCuentaSubCuentaQuery;
            this._metodoPagoQuery = metodoPagoQuery;
            this._loginControlIdQuery = loginControlIdQuery;
            this._httpClientService = httpClientService;
            this._usuarioControlIdQuery = usuarioControlIdQuery;
            this._dispositivoQuery = dispositivoQuery;
            this._movimientoAsientoQuery = movimientoAsientoQuery;
            this._generatePDF = generatePDF;
            this._razorRendererHelper = razorRendererHelper;
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
            ViewData["metodosPagos"] = await this._metodoPagoQuery.GetAll();
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
            ViewData["metodosPagos"] = await this._metodoPagoQuery.GetAll();

            if (ModelState.IsValid)
            {
                var PersonaId = Convert.ToInt32(User.FindFirst("UsuarioId").Value);
                var cliente = await this._personaQuery.GetOneByCI(InscripcionCreateDto.CI);
                var insert = new Inscripcion
                {
                    Costo = InscripcionCreateDto.Costo,
                    FechaCreacion = InscripcionCreateDto.FechaCreacion,
                    FechaFin = InscripcionCreateDto.FechaFin,
                    FechaInicio = InscripcionCreateDto.FechaInicio,
                    MetodoPagoId = InscripcionCreateDto.MetodoId,
                    NumeroRecibo = InscripcionCreateDto.NumeroRecibo,
                    PaqueteId = InscripcionCreateDto.PaqueteId,
                    PersonaId = PersonaId,
                    ClienteId = cliente.Id
                };

                //registrando Ingresos
                var metodo = await this.ObtenerMetodoCuenta(InscripcionCreateDto.MetodoId);
                await this.IngresoAutomatico(
                    metodo.PlanId,
                    metodo.PlanCuenta,
                    $"{InscripcionCreateDto.Nombres} {InscripcionCreateDto.Apellidos}",
                    InscripcionCreateDto.FechaCreacion,
                    InscripcionCreateDto.Costo,
                    InscripcionCreateDto.NumeroRecibo,
                    PersonaId,
                    2
                );

                var inscripcion = await this._inscripcionQuery.Store(insert);
                cliente.ControlIdBegin_time = this.DateTimeToUnix(InscripcionCreateDto.FechaInicio);
                cliente.ControlIdEnd_time = this.DateTimeToUnix(InscripcionCreateDto.FechaFin);
                var updateFecha = await this._personaQuery.UpdateOne(cliente);

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
            var cliente = await this._personaQuery.GetOne(inscripcion.ClienteId);
            InscripcionUpdateDto inscripcionUpdateDto = new InscripcionUpdateDto
            {
                Id = inscripcion.Id,
                Nombres = cliente.Nombre,
                Apellidos = cliente.Apellido,
                CI = cliente.Ci,
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
                    Id = inscripcionUpdateDto.Id,
                    PersonaId = inscripcionUpdateDto.Id,
                    Costo = inscripcionUpdateDto.Costo,
                    FechaFin = inscripcionUpdateDto.FechaFin,
                    FechaInicio = inscripcionUpdateDto.FechaInicio,
                    NumeroRecibo = inscripcionUpdateDto.NumeroRecibo,
                    PaqueteId = inscripcionUpdateDto.PaqueteId,

                };
                var updateInscripcion = await this._inscripcionQuery.Update(data, id);
                //update dispositivo
                var persona = await this._personaQuery.GetOneByCI(inscripcionUpdateDto.CI);
                persona.ControlIdBegin_time = this.DateTimeToUnix(inscripcionUpdateDto.FechaInicio);
                persona.ControlIdEnd_time = this.DateTimeToUnix(inscripcionUpdateDto.FechaFin);
                var updateFecha = await this._personaQuery.UpdateOne(persona);
                await this.UpdateDispositivo(updateFecha);
                return RedirectToAction("PreviewRecibo", new { id = 1 });
            }
            var aux = ModelState.Values;
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

        [HttpGet("Inscripcion-recibo")]
        public async Task<FileStreamResult> InscripcionRecibo(SelectDto selectDto)
        {
            var recibo = new Recibo
            {
                Costo = 100m,
                Fecha = DateTime.Now,
                Nombres = "demo demo"
            };
            var htmlContent = this._razorRendererHelper.RenderPartialToString("/Views/DOCUMENTOS/Recibo.cshtml", recibo);
            var pdf = this._generatePDF.Generate(htmlContent, "xxxx-xxxx-xxxx", false);
            var stream = new MemoryStream(pdf);
            var contentType = @"application/pdf";
            var fileName = $"Demo.pdf";
            //stream.Seek(0, SeekOrigin.Begin);
            //return File(stream, contentType, fileName);
            return new FileStreamResult(stream, contentType);
        }

        /*insercion de inscripcion por defecto*/
        private async Task<MetodoPago> ObtenerMetodoCuenta(int MetodoPagoId)
        {
            return await this._metodoPagoQuery.ObtenerMetodoPago(MetodoPagoId);
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
        //metodo de pago defaul
        private async Task<bool> IngresoAutomatico(
            string PlanCuentaId,
            string NombrePlanCuenta,
            string EntregeA,
            DateTime Fecha,
            decimal Monto,
            string NroRecibo,
            int PersonaId,
            int TipoMovimiento
        )
        {
            var insertMovimiento = new MovimientosAsiento
            {
                EntregeA = EntregeA,
                EntregeATipo = "Cliente",
                Fecha = Fecha,
                Monto = Monto,
                NroRecibo = NroRecibo,
                PersonaId = PersonaId,
                MontoLiteral = "MONTO LITERAL",
                TipoMovimientoId = 2,
                Asientos = new List<Asiento>(){
                    new Asiento{
                        Monto=Monto,
                        NombreAsiento="Pago por Inscripcion",
                        PlanAsientos=new List<PlanAsiento>(){
                            new PlanAsiento{
                                Debe=Monto,
                                Haber=0,
                                PlanId="50101M02",
                                PlanCuenta="Servicios"
                            },
                            new PlanAsiento{
                                Debe=0,
                                Haber=Monto,
                                PlanId=PlanCuentaId,
                                PlanCuenta=NombrePlanCuenta
                            }
                        }
                    }
                }
            };
            var nuevoReciboEgreso = await this._movimientoAsientoQuery.Store(insertMovimiento);
            return true;
        }
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