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

        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }

        private readonly DBContext _dbContext;
        private readonly HorarioQuery _horarioQuery;
        private readonly DispositivoQuery _dispositivoQuery;
        private readonly LoginControlIdQuery _loginControlIdQuery;
        private readonly HttpClientService _httpClientService;
        private readonly HorarioControlIdQuery _horarioControlIdQuery;
        private readonly DiasControlIdQuery _diasControlIdQuery;
        private readonly DiaQuery _diaQuery;
        ApiRutas _apiRutas;
        public HorarioController(
            DBContext dbContext,
            HorarioQuery HorarioQuery,
            DispositivoQuery dispositivoQuery,
            LoginControlIdQuery loginControlIdQuery,
            HttpClientService httpClientService,
            HorarioControlIdQuery horarioControlIdQuery,
            DiasControlIdQuery diasControlIdQuery,
            DiaQuery diaQuery
            )
        {
            this._dbContext = dbContext;
            this._horarioQuery = HorarioQuery;
            this._dispositivoQuery = dispositivoQuery;
            this._loginControlIdQuery = loginControlIdQuery;
            this._httpClientService = httpClientService;

            /*api*/
            this._horarioControlIdQuery = horarioControlIdQuery;
            this._diasControlIdQuery = diasControlIdQuery;
            this._diaQuery = diaQuery;
            this._apiRutas = new ApiRutas();
        }

        private async Task<Boolean> loginControlId()
        {
            BodyLogin cuerpo = _loginControlIdQuery.Login(this.user, this.password);
            Response login = await this._httpClientService.LoginRun(this.controlador, this.port, this._apiRutas.ApiUrlLogin, cuerpo, "");
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
        /*
            *nota sobre horario este modulo necesita refractor  
            cuando el horario asignado es igual en uno mas dias se debe guardar en una solo objecto en el controlador
        */
        [HttpPost("store")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Store([FromForm] HorarioCreateDto horarioCreateDto)
        {
            if (ModelState.IsValid)
            {
                var dias = this.StoreDiaSistema(horarioCreateDto);
                var insert = new Horario
                {
                    Nombre = horarioCreateDto.Nombre,
                    Descripcion = horarioCreateDto.Descripcion,
                    ControlIdName = horarioCreateDto.Nombre,
                    Dias = dias
                };
                var horario=await this._horarioQuery.store(insert);
                await this.RegistrarHora(horario, horario.Dias);
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Horario/Create.cshtml");
        }

        [HttpGet("editar/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            var horario = await this._horarioQuery.GetOne(id);
            if (horario == null)
            {
                return NotFound();
            }
            var edit = new HorarioDto();
            edit.Nombre = horario.Nombre;
            edit.Descripcion = horario.Descripcion;
            foreach (var dia in horario.Dias)
            {
                edit.lunes = this.desarmarDias(horario.Dias, "lunes").dia;
                edit.martes = this.desarmarDias(horario.Dias, "martes").dia;
                edit.miercoles = this.desarmarDias(horario.Dias, "miercoles").dia;
                edit.jueves = this.desarmarDias(horario.Dias, "jueves").dia;
                edit.viernes = this.desarmarDias(horario.Dias, "viernes").dia;
                edit.sabado = this.desarmarDias(horario.Dias, "sabado").dia;
                edit.domingo = this.desarmarDias(horario.Dias, "domingo").dia;

                edit.hora_inicio_lunes = this.desarmarDias(horario.Dias, "lunes").hora_inicio;
                edit.hora_inicio_martes = this.desarmarDias(horario.Dias, "martes").hora_inicio;
                edit.hora_inicio_miercoles = this.desarmarDias(horario.Dias, "miercoles").hora_inicio;
                edit.hora_inicio_jueves = this.desarmarDias(horario.Dias, "jueves").hora_inicio;
                edit.hora_inicio_viernes = this.desarmarDias(horario.Dias, "viernes").hora_inicio;
                edit.hora_inicio_sabado = this.desarmarDias(horario.Dias, "sabado").hora_inicio;
                edit.hora_inicio_domingo = this.desarmarDias(horario.Dias, "domingo").hora_inicio;

                edit.hora_fin_lunes = this.desarmarDias(horario.Dias, "lunes").hora_fin;
                edit.hora_fin_martes = this.desarmarDias(horario.Dias, "martes").hora_fin;
                edit.hora_fin_miercoles = this.desarmarDias(horario.Dias, "miercoles").hora_fin;
                edit.hora_fin_jueves = this.desarmarDias(horario.Dias, "jueves").hora_fin;
                edit.hora_fin_viernes = this.desarmarDias(horario.Dias, "viernes").hora_fin;
                edit.hora_fin_sabado = this.desarmarDias(horario.Dias, "sabado").hora_fin;
                edit.hora_fin_domingo = this.desarmarDias(horario.Dias, "domingo").hora_fin;
            }
            return View("~/Views/Horario/Editar.cshtml", edit);
        }
        [HttpGet("update/{id:int}")]
        public async Task<ActionResult> Update(int id, HorarioDto horarioDto)
        {
            if (ModelState.IsValid)
            {
                var deleteDias = await this._horarioQuery.DeleteDias(id);
                var dias = this.UpdateDiaSistema(horarioDto);
                var insert = new Horario
                {
                    Nombre = horarioDto.Nombre,
                    Descripcion = horarioDto.Descripcion,
                    ControlIdName = horarioDto.Nombre,
                    Dias = dias
                };
                var horario = await this._horarioQuery.update(insert);
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Horario/Editar.cshtml", horarioDto);
        }
        [HttpGet("delete/{id:int}")]
        public async Task<ActionResult> Delete(int id, HorarioDto horarioDto)
        {
            if (ModelState.IsValid)
            {
                //verificar dias de uso
                var horario = await this._horarioQuery.GetOne(id);
                if (await this._horarioQuery.Delete(id))
                {
                    var deleteDias = await this._horarioQuery.DeleteDias(id);
                    await this.DeleteReglaAcceso(horario);
                    return Json(
                        new
                        {
                            status = "success",
                            message = "Horario eliminado correctamente"
                        }
                    );
                }else
                {
                    return Json(
                        new
                        {
                            status = "error",
                            message = "Ocurrio un error"
                        }
                    );
                }
            }
            return View("~/Views/Horario/Editar.cshtml", horarioDto);
        }
        public List<ExtraerDia> validarUpdate(HorarioDto horarioDto)
        {
            var validadores = new List<ExtraerDia>();
            validadores.Add(
                new ExtraerDia
                {
                    dia = horarioDto.lunes,
                    hora_fin = horarioDto.hora_fin_lunes,
                    hora_inicio = horarioDto.hora_inicio_lunes
                }
            );
            validadores.Add(
                new ExtraerDia
                {
                    dia = horarioDto.martes,
                    hora_fin = horarioDto.hora_fin_martes,
                    hora_inicio = horarioDto.hora_inicio_martes
                }
            );
            validadores.Add(
                new ExtraerDia
                {
                    dia = horarioDto.miercoles,
                    hora_fin = horarioDto.hora_fin_miercoles,
                    hora_inicio = horarioDto.hora_inicio_miercoles
                }
            );
            validadores.Add(
               new ExtraerDia
               {
                   dia = horarioDto.jueves,
                   hora_fin = horarioDto.hora_fin_jueves,
                   hora_inicio = horarioDto.hora_inicio_jueves
               }
           );
            validadores.Add(
               new ExtraerDia
               {
                   dia = horarioDto.viernes,
                   hora_fin = horarioDto.hora_fin_viernes,
                   hora_inicio = horarioDto.hora_inicio_viernes
               }
           );
            validadores.Add(
               new ExtraerDia
               {
                   dia = horarioDto.sabado,
                   hora_fin = horarioDto.hora_fin_sabado,
                   hora_inicio = horarioDto.hora_inicio_sabado
               }
           );
            validadores.Add(
                new ExtraerDia
                {
                    dia = horarioDto.domingo,
                    hora_fin = horarioDto.hora_fin_domingo,
                    hora_inicio = horarioDto.hora_inicio_domingo
                }
            );
            return validadores;
        }
        public List<ExtraerDia> validarCreate(HorarioCreateDto horarioCreateDto)
        {
            var validadores = new List<ExtraerDia>();
            validadores.Add(
                new ExtraerDia
                {
                    dia = horarioCreateDto.lunes,
                    hora_fin = horarioCreateDto.hora_fin_lunes,
                    hora_inicio = horarioCreateDto.hora_inicio_lunes
                }
            );
            validadores.Add(
                new ExtraerDia
                {
                    dia = horarioCreateDto.martes,
                    hora_fin = horarioCreateDto.hora_fin_martes,
                    hora_inicio = horarioCreateDto.hora_inicio_martes
                }
            );
            validadores.Add(
                new ExtraerDia
                {
                    dia = horarioCreateDto.miercoles,
                    hora_fin = horarioCreateDto.hora_fin_miercoles,
                    hora_inicio = horarioCreateDto.hora_inicio_miercoles
                }
            );
            validadores.Add(
               new ExtraerDia
               {
                   dia = horarioCreateDto.jueves,
                   hora_fin = horarioCreateDto.hora_fin_jueves,
                   hora_inicio = horarioCreateDto.hora_inicio_jueves
               }
           );
            validadores.Add(
               new ExtraerDia
               {
                   dia = horarioCreateDto.viernes,
                   hora_fin = horarioCreateDto.hora_fin_viernes,
                   hora_inicio = horarioCreateDto.hora_inicio_viernes
               }
           );
            validadores.Add(
               new ExtraerDia
               {
                   dia = horarioCreateDto.sabado,
                   hora_fin = horarioCreateDto.hora_fin_sabado,
                   hora_inicio = horarioCreateDto.hora_inicio_sabado
               }
           );
            validadores.Add(
                new ExtraerDia
                {
                    dia = horarioCreateDto.domingo,
                    hora_fin = horarioCreateDto.hora_fin_domingo,
                    hora_inicio = horarioCreateDto.hora_inicio_domingo
                }
            );
            return validadores;
        }
        private List<Dia> StoreDiaSistema(HorarioCreateDto horarioCreateDto)
        {
            var dias = this.validarCreate(horarioCreateDto);
            var insert = new List<Dia>();
            foreach (var dia in dias)
            {
                if (dia.dia != null)
                {
                    insert.Add(
                    new Dia
                    {
                        ControlEnd = Convert.ToInt32(dia.hora_fin.Hour) * Convert.ToInt32(dia.hora_fin.Minute * 60),
                        ControlStart = Convert.ToInt32(dia.hora_inicio.Hour) * Convert.ToInt32(dia.hora_inicio.Minute * 60),
                        ControlHol1 = 0,
                        ControlHol2 = 0,
                        ControlHol3 = 0,
                        ControlTimeZoneId = 0,
                        ControlMon = dia.dia == "lunes" ? 1 : 0,
                        ControlTue = dia.dia == "martes" ? 1 : 0,
                        ControlWed = dia.dia == "miercoles" ? 1 : 0,
                        ControlThu = dia.dia == "jueves" ? 1 : 0,
                        ControlFri = dia.dia == "viernes" ? 1 : 0,
                        ControlSat = dia.dia == "sabado" ? 1 : 0,
                        ControlSun = dia.dia == "domingo" ? 1 : 0,
                        Start = dia.hora_inicio,
                        End = dia.hora_fin,
                        Mon = dia.dia == "lunes" ? 1 : 0,
                        Tue = dia.dia == "martes" ? 1 : 0,
                        Wed = dia.dia == "miercoles" ? 1 : 0,
                        Thu = dia.dia == "jueves" ? 1 : 0,
                        Fri = dia.dia == "viernes" ? 1 : 0,
                        Sat = dia.dia == "sabado" ? 1 : 0,
                        Sun = dia.dia == "domingo" ? 1 : 0,
                        Hol1 = 0,
                        Hol2 = 0,
                        Hol3 = 0,
                        Nombre = dia.dia,
                        ControlId = 0,
                    });
                }
            }
            return insert;
        }
        private List<Dia> UpdateDiaSistema(HorarioDto horarioDto)
        {
            var dias = this.validarUpdate(horarioDto);
            var insert = new List<Dia>();
            foreach (var dia in dias)
            {
                if (dia.dia != null)
                {
                    insert.Add(
                    new Dia
                    {
                        ControlEnd = Convert.ToInt32(dia.hora_fin.Hour) * Convert.ToInt32(dia.hora_fin.Minute * 60),
                        ControlStart = Convert.ToInt32(dia.hora_inicio.Hour) * Convert.ToInt32(dia.hora_inicio.Minute * 60),
                        ControlHol1 = 0,
                        ControlHol2 = 0,
                        ControlHol3 = 0,
                        ControlTimeZoneId = 0,
                        ControlMon = dia.dia == "lunes" ? 1 : 0,
                        ControlTue = dia.dia == "martes" ? 1 : 0,
                        ControlWed = dia.dia == "miercoles" ? 1 : 0,
                        ControlThu = dia.dia == "jueves" ? 1 : 0,
                        ControlFri = dia.dia == "viernes" ? 1 : 0,
                        ControlSat = dia.dia == "sabado" ? 1 : 0,
                        ControlSun = dia.dia == "domingo" ? 1 : 0,
                        Start = dia.hora_inicio,
                        End = dia.hora_fin,
                        Mon = dia.dia == "lunes" ? 1 : 0,
                        Tue = dia.dia == "martes" ? 1 : 0,
                        Wed = dia.dia == "miercoles" ? 1 : 0,
                        Thu = dia.dia == "jueves" ? 1 : 0,
                        Fri = dia.dia == "viernes" ? 1 : 0,
                        Sat = dia.dia == "sabado" ? 1 : 0,
                        Sun = dia.dia == "domingo" ? 1 : 0,
                        Hol1 = 0,
                        Hol2 = 0,
                        Hol3 = 0,
                        Nombre = dia.dia,
                        ControlId = 0,
                    });
                }
            }
            return insert;
        }
        private ExtraerDia desarmarDias(List<Dia> dias, string validarDia)
        {
            var resultado = new ExtraerDia();
            foreach (var dia in dias)
            {
                if (dia.Nombre == validarDia)
                {
                    resultado.dia = dia.Nombre;
                    resultado.hora_fin = dia.End;
                    resultado.hora_inicio = dia.Start;
                }
            }
            return resultado;
        }
        private int identificarDia(string validar)
        {
            int resultado = 0;
            switch (validar)
            {
                case "lunes":
                    resultado = 1;
                    break;
                case "martes":
                    resultado = 1;
                    break;
                case "miercoles":
                    resultado = 1;
                    break;
                case "jueves":
                    resultado = 1;
                    break;
                case "viernes":
                    resultado = 1;
                    break;
                case "sabado":
                    resultado = 1;
                    break;
                case "domingo":
                    resultado = 1;
                    break;
                default:
                    resultado = 0;
                    break;
            }
            return resultado;
        }
        /*control Id*/
        /*login dispositivo*/
        private async Task<bool> LoginControlId(string ip, int port, string user, string api, string password)
        {
            BodyLogin cuerpo = _loginControlIdQuery.Login(user, password);
            Response login = await this._httpClientService.LoginRun(ip, port, api, cuerpo, "");
            /*valido si es el login fue ok*/

            this._horarioControlIdQuery.Params(port, ip, user, password, login.data);
            this._diasControlIdQuery.Params(port, ip, user, password, login.data);
            //this._portalsControlIdQuery.Params(port, ip, user, password, login.data);
            return login.estado;
        }
        /*------Obtener data dispositivo------*/
        private async Task<bool> RegistrarHora(Horario horario, List<Dia> dias)
        {
            /*buscar por dispositivos*/
            var dispositivos = await this._dispositivoQuery.GetAll();
            foreach (var dispositivo in dispositivos)
            {
                var loginStatus = await this.LoginControlId(dispositivo.Ip, dispositivo.Puerto, dispositivo.Usuario, this._apiRutas.ApiUrlLogin, dispositivo.Password);
                if (loginStatus)
                {
                    //crear horario
                    await this.StoreHorario(horario);
                }
            }
            return true;
        }
        private async Task<bool> StoreHorario(Horario horario)
        {
            var apiResponse = await this._horarioControlIdQuery.Create(horario);
            if (apiResponse.status)
            {
                horario.ControlId = apiResponse.ids[0];
                var update=await this._horarioQuery.UpdateControlId(horario);
                //dependecia
                await this.StoreDia(update, update.Dias);
                return apiResponse.status;
            }
            else
            {
                return apiResponse.status;
            }
        }
        private async Task<bool> StoreDia(Horario horario, List<Dia> dias)
        {
            var apiResponse = await this._diasControlIdQuery.CreateAll(dias);
            if (apiResponse.status)
            {
                int index = 0;
                foreach (var id in apiResponse.ids)
                {
                    dias[index].ControlId = id;
                    dias[index].ControlTimeZoneId = horario.ControlId;
                    await this._diaQuery.UpdateControlId(dias[index]);
                    index++;
                }
                return apiResponse.status;
            }
            else
            {
                return apiResponse.status;
            }
        }
        /*------Delete data dispositivo------*/
        private async Task<bool> DeleteReglaAcceso(Horario horario)
        {
            /*buscar por dispositivos*/
            var dispositivos = await this._dispositivoQuery.GetAll();
            foreach (var dispositivo in dispositivos)
            {
                var loginStatus = await this.LoginControlId(dispositivo.Ip, dispositivo.Puerto, dispositivo.Usuario, this._apiRutas.ApiUrlLogin, dispositivo.Password);
                if (loginStatus)
                {
                    //crear regla acceso
                    await this.DeleteHorario(horario, horario.Dias);
                }
            }
            return true;
        }
        private async Task<bool> DeleteHorario(Horario horario, List<Dia> dias)
        {
            var delete = await this._horarioQuery.Delete(horario.ControlId);
            var apiResponse = await this._horarioControlIdQuery.Delete(horario);
            if (apiResponse.status)
            {
                return apiResponse.status;
            }
            return apiResponse.status;
        }
    }
    public class ExtraerDia
    {
        public string dia { get; set; }
        public DateTime hora_inicio { get; set; }
        public DateTime hora_fin { get; set; }
    }
}