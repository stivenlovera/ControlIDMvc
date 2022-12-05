using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ControlIDMvc.Dtos.ReciboEgresoDto;
using ControlIDMvc.Querys;
using ControlIDMvc.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ControlIDMvc.Controllers
{
     [Authorize]
    [Route("recibo-egreso")]
    public class ReciboEgresoController : Controller
    {
        private readonly ILogger<ReciboEgresoController> _logger;
        private readonly MovimientoAsientoQuery _movimientoAsientoQuery;
        private readonly AsientoQuery _asientoQuery;
        private readonly PlanAsientoQuery _planAsientoQuery;

        public ReciboEgresoController(
            ILogger<ReciboEgresoController> logger,
            MovimientoAsientoQuery movimientoAsientoQuery,
            AsientoQuery asientoQuery,
            PlanAsientoQuery planAsientoQuery
            )
        {
            _logger = logger;
            this._movimientoAsientoQuery = movimientoAsientoQuery;
            this._asientoQuery = asientoQuery;
            this._planAsientoQuery = planAsientoQuery;
        }
        [HttpGet("crear")]
        public IActionResult Create()
        {
            int nroRecibo = 1000;
            var ReciboEgresoDto = new ReciboEgresoDto()
            {
                NroRecibo = nroRecibo.ToString(),
                Fecha = DateTime.Now,
                EntregeA = "",
                Monto = 0.0m,
                MontoLiteral = "",
                NombrePersona = "stiven"
            };
            return View("~/Views/ReciboEgresos/Create.cshtml", ReciboEgresoDto);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpPost("store")]
        public async Task<IActionResult> Store(ReciboEgresoDto reciboEgresoDto)
        {
            var insertMovimiento = new MovimientosAsiento
            {
                EntregeA = reciboEgresoDto.EntregeA,
                EntregeATipo = "no registrado",
                Fecha = reciboEgresoDto.Fecha,
                Monto = reciboEgresoDto.Monto,
                NroRecibo = reciboEgresoDto.NroRecibo,
                PersonaId = 1,
                MontoLiteral = reciboEgresoDto.MontoLiteral,
                TipoMovimientoId = 1
            };
            var nuevoReciboEgreso = await this._movimientoAsientoQuery.Store(insertMovimiento);
            //var insertItems = new List<Asiento>();
            foreach (var item in reciboEgresoDto.Items)
            {
                var insertItems = new Asiento
                {
                    Monto = item.Monto,
                    NombreAsiento = item.Concepto,
                    MovimientosAsientoId = insertMovimiento.Id
                };
                var nuevoAsiento = await this._asientoQuery.Store(insertItems);
                foreach (var plan in item.Planes)
                {
                    var insertPlanAsiento = new PlanAsiento
                    {
                        PlanId = plan.PlanId,
                        PlanCuenta = plan.PlanCuenta,
                        Debe = plan.Debe,
                        Haber = plan.Haber,
                        AsientoId = nuevoAsiento.Id
                    };
                    var nuevoPlanAsiento = await this._planAsientoQuery.Store(insertPlanAsiento);
                }
            }
            return Json(new
            {
                status = "success",
                message = "recibido"
            });
        }
        [HttpGet("editar/{id:int}")]
        public async Task<IActionResult> Editar(int id)
        {
            var movimiento = await this._movimientoAsientoQuery.GetOne(id);
            var items = new List<Items>();
            foreach (var asiento in movimiento.Asientos)
            {
                var planes = new List<Planes>();
                foreach (var plan in asiento.PlanAsientos)
                {
                    planes.Add(new Planes
                    {
                        PlanId = plan.PlanId,
                        PlanCuenta = plan.PlanCuenta,
                        Debe = plan.Debe,
                        Haber = plan.Haber
                    });
                }
                items.Add(new Items
                {
                    Concepto = asiento.NombreAsiento,
                    Facturacion = 1,
                    Monto = asiento.Monto,
                    Planes = planes
                });
            }
            var ReciboEgresoDto = new ReciboEgresoDto()
            {
                Id = id,
                NroRecibo = movimiento.NroRecibo,
                Fecha = movimiento.Fecha,
                EntregeA = movimiento.EntregeA,
                Monto = movimiento.Monto,
                MontoLiteral = movimiento.MontoLiteral,
                NombrePersona = "stiven",
                Items = items
            };
            return View("~/Views/ReciboEgresos/Edit.cshtml", ReciboEgresoDto);
        }
        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> Update(int id, ReciboEgresoDto reciboEgresoDto)
        {
            var insertMovimiento = new MovimientosAsiento
            {
                Id = id,
                Monto = reciboEgresoDto.Monto,
                EntregeA = reciboEgresoDto.EntregeA,
                MontoLiteral = reciboEgresoDto.MontoLiteral,
            };
            var nuevoReciboEgreso = await this._movimientoAsientoQuery.Update(insertMovimiento);
            //delete all asiento
            await this._asientoQuery.DeleteMovimientoId(id);
            foreach (var item in reciboEgresoDto.Items)
            {
                var insertItems = new Asiento
                {
                    Monto = item.Monto,
                    NombreAsiento = item.Concepto,
                    MovimientosAsientoId = insertMovimiento.Id
                };
                var nuevoAsiento = await this._asientoQuery.Store(insertItems);
                foreach (var plan in item.Planes)
                {
                    var insertPlanAsiento = new PlanAsiento
                    {
                        PlanId = plan.PlanId,
                        PlanCuenta = plan.PlanCuenta,
                        Debe = plan.Debe,
                        Haber = plan.Haber,
                        AsientoId = nuevoAsiento.Id
                    };
                    var nuevoPlanAsiento = await this._planAsientoQuery.Store(insertPlanAsiento);
                }
            }
            return Json(new
            {
                status = "success",
                message = "Modificado"
            });
        }
    }
}