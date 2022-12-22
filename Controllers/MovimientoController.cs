using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Dtos.MovimientoDto;
using ControlIDMvc.Entities;
using ControlIDMvc.Models.DatatableModel;
using ControlIDMvc.Querys;
using jQueryDatatableServerSideNetCore.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
        public async Task<IActionResult> LoadTable(FiltroDatatable filtroDatatable)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var draw = Request.Form["draw"].FirstOrDefault();
            var sortColumnIndex = Request.Form["order[0][column]"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
            var aux = await this._movimientoAsientoQuery.Datatable(filtroDatatable.FechaInicio,filtroDatatable.FechaFin,filtroDatatable.TipoMovimientoId,filtroDatatable.PersonaId);
            var data = aux.AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(
                x => x.receptor.ToLower().Contains(searchValue.ToLower()) ||
                x.montoTotal.ToString().ToLower().Contains(searchValue.ToLower()) ||
                x.usuario.ToLower().Contains(searchValue.ToLower()) ||
                x.tipoMovimiento.ToString().ToLower().Contains(searchValue.ToLower()));
            }
            // get total count of records after searchEntregeA
            filterRecord = data.Count();
            //sort data
            //if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection)) data = data.OrderBy("");
            //numero de de columnas
            if (Convert.ToInt32(sortColumnIndex) == 1)
            {
                data = sortColumnDirection == "asc" ? data.OrderBy(c => c.usuario) : data.OrderByDescending(c => c.usuario);
            }
            if (Convert.ToInt32(sortColumnIndex) == 2)
            {
                data = sortColumnDirection == "asc" ? data.OrderBy(c => c.numeroRecibo) : data.OrderByDescending(c => c.numeroRecibo);
            }
            if (Convert.ToInt32(sortColumnIndex) == 5)
            {
                data = sortColumnDirection == "asc" ? data.OrderBy(c => c.receptor) : data.OrderByDescending(c => c.receptor);
            }
            if (Convert.ToInt32(sortColumnIndex) == 6)
            {
                data = sortColumnDirection == "asc" ? data.OrderBy(c => c.montoTotal) : data.OrderByDescending(c => c.montoTotal);
            }
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
        private async Task<List<DatatableMovimiento>> DatatableData()
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