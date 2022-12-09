using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Dtos.MovimientoDto;
using ControlIDMvc.Entities;
using ControlIDMvc.Models.DatatableModel;
using ControlIDMvc.Querys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlIDMvc.Controllers
{
    [Authorize]
    [Route("movimientos")]
    public class MovimientoController : Controller
    {
        private readonly ILogger<MovimientoController> _logger;
        private readonly MovimientoAsientoQuery _movimientoAsientoQuery;
        private readonly AsientoQuery _asientoQuery;
        private readonly UsuarioQuery _usuarioQuery;

        public MovimientoController(
            ILogger<MovimientoController> logger,
            MovimientoAsientoQuery movimientoAsientoQuery,
            AsientoQuery asientoQuery,
            UsuarioQuery usuarioQuery
            )
        {
            _logger = logger;
            this._movimientoAsientoQuery = movimientoAsientoQuery;
            this._asientoQuery = asientoQuery;
            this._usuarioQuery = usuarioQuery;
        }
        [HttpGet]
        public async Task<ActionResult> Index(MovmientosDto MovmientosDto)
        {
            MovmientosDto.Usuarios = new List<Dtos.MovimientoDto.Usuario>();
            foreach (var usuario in await this._usuarioQuery.GetAll())
            {
                MovmientosDto.Usuarios.Add(new Dtos.MovimientoDto.Usuario
                {
                    Nombre = $"{usuario.Persona.Nombre} {usuario.Persona.Apellido}",
                    Id = usuario.Persona.Id
                });
            }
            MovmientosDto.TipoMovimientos = new List<Dtos.MovimientoDto.TipoMovimiento>(){
                new Dtos.MovimientoDto.TipoMovimiento{
                    Nombre="Ingreso",
                    Id=2
                },
                new Dtos.MovimientoDto.TipoMovimiento{
                    Nombre="Egreso",
                    Id=1
                }
             };

            return View("~/Views/Movimientos/Lista.cshtml", MovmientosDto);
        }

        [HttpPost("data-table")]
        public async Task<IActionResult> DataTable([FromQuery] FiltroDatatable FiltroDatatable)
        {
            var movimientos = await this.DatatableData();
            int totalRecord = 0;
            int filterRecord = 0;
            var draw = Request.Form["draw"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");

            //activacion de filtros
            var personaId = FiltroDatatable.PersonaId;

            var data = (from i in movimientos
                        where i.PersonaId == FiltroDatatable.PersonaId
                        &&
                        i.TipoMovimientoId == FiltroDatatable.TipoMovimientoId
                        &&
                        i.Fecha >= FiltroDatatable.FechaInicio && i.Fecha <= FiltroDatatable.FechaFin
                        select new DatatableMovimiento()
                        {
                            Id = i.Id,
                            Usuario = $"{i.Persona.Nombre} {i.Persona.Apellido}",
                            Fecha = i.Fecha.ToString("dd/MM/yyyy"),
                            MontoTotal = i.Monto,
                            NumeroRecibo = i.NroRecibo,
                            Receptor = i.EntregeA,
                            TipoMovimiento = i.TipoMovimiento.NombreMovimiento
                        });

            totalRecord = data.Count();
            // buscar por valor
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x => x.Usuario.ToLower().Contains(searchValue.ToLower()) || x.Fecha.ToLower().Contains(searchValue.ToLower()) || x.NumeroRecibo.ToLower().Contains(searchValue.ToLower()) || x.MontoTotal.ToString().ToLower().Contains(searchValue.ToLower()));
            }
            // get total count of records after search
            filterRecord = data.Count();
            System.Console.WriteLine(" filtro " + sortColumn + " " + sortColumnDirection);
            //filtro columna
            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection)) data = data.OrderBy(x => sortColumn).ThenBy(x => sortColumnDirection);
            //pagination
            var empList = data.Skip(skip).Take(pageSize).ToList();
            return Json(new
            {
                draw = draw,
                recordsTotal = totalRecord,
                recordsFiltered = filterRecord,
                data = empList
            });
        }
        [HttpGet("detail/{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {
            var asientos = await this._asientoQuery.GetAllDetail(id);
            return Json(new
            {
                status = "success",
                message = "",
                data = asientos
            });
        }
        private async Task<List<MovimientosAsiento>> DatatableData()
        {
            //select 
            var movimientos = await this._movimientoAsientoQuery.ShowAllGrl();
            return movimientos;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}