using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Dtos.PlanCuentaTitulo;
using ControlIDMvc.Entities;
using ControlIDMvc.Models;
using ControlIDMvc.Querys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ControlIDMvc.Controllers
{
     [Authorize]
    [Route("plan-titulo")]
    public class PlanCuentaTituloController : Controller
    {
        private readonly ILogger<PlanCuentaTituloController> _logger;
        private readonly PlanCuentaTituloQuery _planCuentaTituloQuery;

        public PlanCuentaTituloController(
            ILogger<PlanCuentaTituloController> logger,
            PlanCuentaTituloQuery planCuentaTituloQuery
            )
        {
            _logger = logger;
            this._planCuentaTituloQuery = planCuentaTituloQuery;
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

        [HttpGet("mostrar-one-titulo/{id:int}")]
        public async Task<IActionResult> ShowTitulo(int id)
        {
            var titulo = await this._planCuentaTituloQuery.GetOnetituloId(id);
            return Json(
                new
                {
                    status = "ok",
                    message = "nuevo",
                    data = titulo
                }
            );
        }
        [HttpPost("store-titulo")]
        public async Task<IActionResult> StoreTitulo(PlanCuentaTituloCreateDto planCuentaTituloCreateDto)
        {
            if (ModelState.IsValid)
            {
                var insert = new PlanCuentaTitulo
                {
                    Codigo = $"{planCuentaTituloCreateDto.Codigo}",
                    NombreCuenta = planCuentaTituloCreateDto.Nombre,
                    Nivel = planCuentaTituloCreateDto.Nivel,
                    PlanCuentaRubroId = planCuentaTituloCreateDto.PlanCuentaRubroId,
                };
                if (!await this._planCuentaTituloQuery.ValidarCodigo(insert.Codigo, insert.PlanCuentaRubroId))
                {
                    var nuevo = await this._planCuentaTituloQuery.Store(insert);
                    return Json(
                       new
                       {
                           status = "ok",
                           message = "Registrado Correctamente"
                       });
                }
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
            return Json(
                new
                {
                    status = "error",
                    message = "A ocurrido un error",
                    errors = ModelState
                });
        }
        [HttpPut("update-titulo/{id:int}")]
        public async Task<IActionResult> UpdateGrupo(int id, PlanCuentaTituloCreateDto planCuentaTituloCreateDto)
        {
            if (ModelState.IsValid)
            {
                var update = new PlanCuentaTitulo
                {
                    Id = id,
                    Codigo = planCuentaTituloCreateDto.Codigo,
                    NombreCuenta = planCuentaTituloCreateDto.Nombre,
                    Nivel = planCuentaTituloCreateDto.Nivel,
                    PlanCuentaRubroId = planCuentaTituloCreateDto.PlanCuentaRubroId,
                };
                if (!await this._planCuentaTituloQuery.ValidarCodigoUpdate(id, update.Codigo, update.PlanCuentaRubroId))
                {
                    var nuevo = await this._planCuentaTituloQuery.Update(update);
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
                           message = "Codigo ya existe",
                       });
                }
            }
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

        [HttpDelete("delete-titulo/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            //validar a futuro dependecias

            if (await this._planCuentaTituloQuery.Delete(id))
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