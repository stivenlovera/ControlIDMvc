using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Dtos.PlanCuentaRubro;
using ControlIDMvc.Entities;
using ControlIDMvc.Models;
using ControlIDMvc.Querys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ControlIDMvc.Controllers
{
     [Authorize]
    [Route("plan-rubro")]
    public class PlanCuentaRubroController : Controller
    {
        private readonly ILogger<PlanCuentaRubroController> _logger;
        private readonly PlanCuentaRubroQuery _planCuentaRubroQuery;
        private readonly PlanCuentasGrupoQuery _planCuentasGrupoQuery;

        public PlanCuentaRubroController(
            ILogger<PlanCuentaRubroController> logger,
            PlanCuentaRubroQuery planCuentaRubroQuery,
            PlanCuentasGrupoQuery planCuentasGrupoQuery
            )
        {
            _logger = logger;
            this._planCuentaRubroQuery = planCuentaRubroQuery;
            this._planCuentasGrupoQuery = planCuentasGrupoQuery;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("mostrar-one-grupo/{id:int}")]
        public async Task<IActionResult> ShowGrupo(int id)
        {
            var grupo = await this._planCuentasGrupoQuery.GetOne(id);

            return Json(
                new
                {
                    status = "ok",
                    message = "nuevo",
                    data = grupo
                }
            );
        }
        [HttpGet("mostrar-one-rubro/{id:int}")]
        public async Task<IActionResult> ShowRubro(int id)
        {
            var rubro = await this._planCuentaRubroQuery.GetOne(id);

            return Json(
                new
                {
                    status = "ok",
                    message = "nuevo",
                    data = rubro
                }
            );
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpPost("store-rubro")]
        public async Task<IActionResult> StoreRubro(PlanCuentaRubroCreateDto planCuentaRubroCreateDto)
        {
            if (ModelState.IsValid)
            {
                var insert = new PlanCuentaRubro
                {
                    Codigo = $"{planCuentaRubroCreateDto.Codigo}",
                    NombreCuenta = planCuentaRubroCreateDto.Nombre,
                    Nivel = planCuentaRubroCreateDto.Nivel,
                    PlanCuentaGrupoId = planCuentaRubroCreateDto.PlanCuentaGrupoId,
                    Debe = 100
                };
                if (!await this._planCuentaRubroQuery.ValidarCodigo(insert.Codigo, insert.PlanCuentaGrupoId))
                {
                    var nuevo = await this._planCuentaRubroQuery.Store(insert);
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
        [HttpPut("update-rubro/{id:int}")]
        public async Task<IActionResult> UpdateGrupo(int id, PlanCuentaRubroCreateDto planCuentaRubroCreateDto)
        {
            if (ModelState.IsValid)
            {
                var update = new PlanCuentaRubro
                {
                    Id = id,
                    Codigo = planCuentaRubroCreateDto.Codigo,
                    NombreCuenta = planCuentaRubroCreateDto.Nombre,
                    Nivel = planCuentaRubroCreateDto.Nivel,
                    PlanCuentaGrupoId = planCuentaRubroCreateDto.PlanCuentaGrupoId,
                };
                if (!await this._planCuentaRubroQuery.ValidarCodigoUpdate(id, update.Codigo, update.PlanCuentaGrupoId))
                {
                    var nuevo = await this._planCuentaRubroQuery.Update(update);
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
        [HttpDelete("delete-rubro/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            //validar a futuro dependecias

            if (await this._planCuentaRubroQuery.Delete(id))
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