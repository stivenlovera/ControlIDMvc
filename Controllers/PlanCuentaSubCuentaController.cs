using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Dtos.PlanCuentaSubCuenta;
using ControlIDMvc.Entities;
using ControlIDMvc.Models;
using ControlIDMvc.Querys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ControlIDMvc.Controllers
{
     [Authorize]
    [Route("plan-sub-cuenta")]
    public class PlanCuentaSubCuentaController : Controller
    {
        private readonly ILogger<PlanCuentaSubCuentaController> _logger;
        private readonly PlanCuentaSubCuentaQuery _planCuentaSubCuentaQuery;

        public PlanCuentaSubCuentaController(
            ILogger<PlanCuentaSubCuentaController> logger,
            PlanCuentaSubCuentaQuery planCuentaSubCuentaQuery
            )
        {
            _logger = logger;
            this._planCuentaSubCuentaQuery = planCuentaSubCuentaQuery;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        [HttpGet("mostrar-one-subcuenta/{id:int}")]
        public async Task<IActionResult> ShowSubcuenta(int id)
        {
            var subCuenta = await this._planCuentaSubCuentaQuery.GetOneSubCuentaId(id);
            return Json(
                new
                {
                    status = "ok",
                    message = "nuevo",
                    data = subCuenta
                }
            );
        }
        [HttpPost("store-subcuenta")]
        public async Task<IActionResult> StoreSubCuenta(PlanCuentaSubCuentaCreateDto planCuentaSubCuentaCreateDto)
        {
            if (ModelState.IsValid)
            {
                var insert = new PlanCuentaSubCuenta
                {
                    Codigo = $"{planCuentaSubCuentaCreateDto.Codigo}",
                    NombreCuenta = planCuentaSubCuentaCreateDto.Nombre,
                    Nivel = planCuentaSubCuentaCreateDto.Nivel,
                    PlanCuentaCompuestaId = planCuentaSubCuentaCreateDto.PlanCuentaCompuestaId,
                    Valor = planCuentaSubCuentaCreateDto.Valor,
                    Moneda = planCuentaSubCuentaCreateDto.Moneda
                };
                if (!await this._planCuentaSubCuentaQuery.ValidarCodigo(insert.Codigo, insert.PlanCuentaCompuestaId))
                {
                    var nuevo = await this._planCuentaSubCuentaQuery.Store(insert);
                    return Json(
                        new
                        {
                            status = "ok",
                            message = "Registrado Correctamente"
                        });
                }
                else
                {
                    return Json(
                       new
                       {
                           status = "error",
                           message = "A ocurrido un error",
                           errors = new
                           {
                               Codigo = new Errors
                               {
                                   errors = new List<ErrorsMessage>(){
                                        new ErrorsMessage{
                                            errorMessage="Codigo ya existe"
                                        }
                                    }
                               }
                           },
                       });
                }
            }
            return Json(
                new
                {
                    status = "error",
                    message = "A ocurrido un error",
                    errors = ModelState
                });
        }
        [HttpPut("update-subcuenta/{id:int}")]
        public async Task<IActionResult> UpdateGrupo(int id, PlanCuentaSubCuentaCreateDto planCuentaSubCuentaCreateDto)
        {
            if (ModelState.IsValid)
            {
                var update = new PlanCuentaSubCuenta
                {
                    Id = id,
                    Codigo = planCuentaSubCuentaCreateDto.Codigo,
                    NombreCuenta = planCuentaSubCuentaCreateDto.Nombre,
                    Nivel = planCuentaSubCuentaCreateDto.Nivel,
                    Valor = planCuentaSubCuentaCreateDto.Valor,
                    Moneda = planCuentaSubCuentaCreateDto.Moneda,
                    PlanCuentaCompuestaId = planCuentaSubCuentaCreateDto.PlanCuentaCompuestaId,
                };
                if (!await this._planCuentaSubCuentaQuery.ValidarCodigoUpdate(id, update.Codigo, update.PlanCuentaCompuestaId))
                {
                    var nuevo = await this._planCuentaSubCuentaQuery.Update(update);
                    return Json(
                        new
                        {
                            status = "ok",
                            message = "Modificado Correctamente"
                        });
                }
                else
                {
                    return Json(
                     new
                     {
                         status = "error",
                         message = "A ocurrido un error",
                         errors = new
                         {
                             Codigo = new Errors
                             {
                                 errors = new List<ErrorsMessage>(){
                                        new ErrorsMessage{
                                            errorMessage="Codigo ya existe"
                                        }
                                    }
                             }
                         },
                     });
                }
            }
            return Json(
               new
               {
                   status = "error",
                   message = "A ocurrido un error",
                   errors = ModelState
               });
        }

        [HttpDelete("delete-subcuenta/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            //validar a futuro dependecias

            if (await this._planCuentaSubCuentaQuery.Delete(id))
            {
                return Json(
                    new
                    {
                        status = "ok",
                        message = "Eliminado correctamente"
                    });
            }
            else
            {
                return Json(
                   new
                   {
                       status = "error",
                       message = "Ocurrio un error",
                   });
            }
        }
    }
}