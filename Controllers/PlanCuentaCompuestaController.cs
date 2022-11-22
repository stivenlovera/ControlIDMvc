using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Dtos.PlanCuentaCompuesta;
using ControlIDMvc.Entities;
using ControlIDMvc.Models;
using ControlIDMvc.Querys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ControlIDMvc.Controllers
{
    [Route("plan-compuesta")]
    public class PlanCuentaCompuestaController : Controller
    {
        private readonly ILogger<PlanCuentaCompuestaController> _logger;
        private readonly PlanCuentaCompuestaQuery _planCuentaCompuestaQuery;

        public PlanCuentaCompuestaController(
            ILogger<PlanCuentaCompuestaController> logger,
             PlanCuentaCompuestaQuery planCuentaCompuestaQuery
            )
        {
            _logger = logger;
            this._planCuentaCompuestaQuery = planCuentaCompuestaQuery;
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
        [HttpGet("mostrar-one-compuesta/{id:int}")]
        public async Task<IActionResult> ShowCompuesta(int id)
        {
            var titulo = await this._planCuentaCompuestaQuery.GetOneCompuestaId(id);
            return Json(
                new
                {
                    status = "ok",
                    message = "nuevo",
                    data = titulo
                }
            );
        }
        [HttpPost("crear-compuesta")]
        public async Task<IActionResult> StoreCompuesta(PlanCuentaCompuestaCreateDto planCuentaCompuestaCreateDto)
        {
            if (ModelState.IsValid)
            {
                var insert = new PlanCuentaCompuesta
                {
                    Codigo = $"{planCuentaCompuestaCreateDto.Codigo}",
                    NombreCuenta = planCuentaCompuestaCreateDto.Nombre,
                    Nivel = planCuentaCompuestaCreateDto.Nivel,
                    PlanCuentaTituloId = planCuentaCompuestaCreateDto.PlanCuentaTituloId
                };
                if (!await this._planCuentaCompuestaQuery.ValidarCodigo(insert.Codigo, insert.PlanCuentaTituloId))
                {
                    var nuevo = await this._planCuentaCompuestaQuery.Store(insert);
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
        [HttpPut("update-compuesta/{id:int}")]
        public async Task<IActionResult> UpdateGrupo(int id, PlanCuentaCompuestaCreateDto planCuentaCompuestaCreateDto)
        {
            if (ModelState.IsValid)
            {
                var update = new PlanCuentaCompuesta
                {
                    Id = id,
                    Codigo = planCuentaCompuestaCreateDto.Codigo,
                    NombreCuenta = planCuentaCompuestaCreateDto.Nombre,
                    Nivel = planCuentaCompuestaCreateDto.Nivel,
                    PlanCuentaTituloId = planCuentaCompuestaCreateDto.PlanCuentaTituloId
                };
                if (!await this._planCuentaCompuestaQuery.ValidarCodigoUpdate(id, update.Codigo, update.PlanCuentaTituloId))
                {
                    var nuevo = await this._planCuentaCompuestaQuery.Update(update);
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

        [HttpDelete("delete-compuesta/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            //validar a futuro dependecias

            if (await this._planCuentaCompuestaQuery.Delete(id))
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