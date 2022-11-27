using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Dtos.PlanCuentaGrupo;
using ControlIDMvc.Entities;
using ControlIDMvc.Models;
using ControlIDMvc.Models.DatatableModel;
using ControlIDMvc.Querys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ControlIDMvc.Controllers
{
    [Route("PlanDeCuentas")]
    public class PlanCuentaGrupoController : Controller
    {
        private readonly ILogger<PlanCuentaGrupoController> _logger;
        private readonly PlanCuentaRubroQuery _planCuentaRubroQuery;
        private readonly PlanCuentaTituloQuery _planCuentaTituloQuery;
        private readonly PlanCuentaCompuestaQuery _planCuentaCompuestaQuery;
        private readonly PlanCuentaSubCuentaQuery _planCuentaSubCuentaQuery;
        private readonly PlanCuentasGrupoQuery _planCuentasGrupoQuery;

        public PlanCuentaGrupoController(
            ILogger<PlanCuentaGrupoController> logger,
            PlanCuentaRubroQuery planCuentaRubroQuery,
            PlanCuentaTituloQuery planCuentaTituloQuery,
            PlanCuentaCompuestaQuery planCuentaCompuestaQuery,
            PlanCuentaSubCuentaQuery planCuentaSubCuentaQuery,
            PlanCuentasGrupoQuery planCuentasGrupoQuery
        )
        {
            this._logger = logger;
            this._planCuentaRubroQuery = planCuentaRubroQuery;
            this._planCuentaTituloQuery = planCuentaTituloQuery;
            this._planCuentaCompuestaQuery = planCuentaCompuestaQuery;
            this._planCuentaSubCuentaQuery = planCuentaSubCuentaQuery;
            this._planCuentasGrupoQuery = planCuentasGrupoQuery;
        }
        private async Task<PlanCuentaListaDto> Tabla()
        {
            var planCuentaListaDto = new PlanCuentaListaDto();
            planCuentaListaDto.ListaPlanes = new List<ListaPlanes>();
            var grupos = await this._planCuentasGrupoQuery.GetAll();
            if (grupos != null)
            {
                foreach (var grupo in grupos)
                {
                    planCuentaListaDto.ListaPlanes.Add(new ListaPlanes()
                    {
                        Id = grupo.Id,
                        Codigo = grupo.Codigo,
                        NombreCuenta = grupo.NombreCuenta,
                        Moneda = grupo.Moneda,
                        Nivel = grupo.Nivel,
                        Valor = grupo.Valor,
                        ModalCreate = "rubro",
                        ModalEdit = "grupo",
                        Debe = grupo.Debe,
                        Haber = grupo.Haber,
                    });
                    if (grupo.PlanCuentaRubros != null)
                    {
                        foreach (var rubro in grupo.PlanCuentaRubros)
                        {
                            planCuentaListaDto.ListaPlanes.Add(new ListaPlanes
                            {
                                Id = rubro.Id,
                                Codigo = $"{grupo.Codigo}{rubro.Codigo}",
                                NombreCuenta = rubro.NombreCuenta,
                                Moneda = rubro.Moneda,
                                Nivel = rubro.Nivel,
                                Valor = rubro.Valor,
                                ModalCreate = "titulo",
                                ModalEdit = "rubro",
                                Debe = rubro.Debe,
                                Haber = rubro.Haber,
                            });
                            var titulos = await this._planCuentaTituloQuery.GetOneRubroId(rubro.Id);
                            if (titulos != null)
                            {
                                foreach (var titulo in titulos)
                                {
                                    planCuentaListaDto.ListaPlanes.Add(new ListaPlanes
                                    {
                                        Id = titulo.Id,
                                        Codigo = $"{grupo.Codigo}{rubro.Codigo}{titulo.Codigo}",
                                        NombreCuenta = titulo.NombreCuenta,
                                        Moneda = titulo.Moneda,
                                        Nivel = titulo.Nivel,
                                        Valor = titulo.Valor,
                                        ModalCreate = "compuesta",
                                        ModalEdit = "titulo",
                                        Debe = titulo.Debe,
                                        Haber = titulo.Haber,
                                    });
                                    if (titulo.PlanCuentaCompuesta != null)
                                    {
                                        foreach (var compuesta in titulo.PlanCuentaCompuesta)
                                        {
                                            planCuentaListaDto.ListaPlanes.Add(new ListaPlanes
                                            {
                                                Id = compuesta.Id,
                                                Codigo = $"{grupo.Codigo}{rubro.Codigo}{titulo.Codigo}{compuesta.Codigo}",
                                                NombreCuenta = compuesta.NombreCuenta,
                                                Moneda = compuesta.Moneda,
                                                Nivel = compuesta.Nivel,
                                                Valor = compuesta.Valor,
                                                ModalCreate = "subCuenta",
                                                ModalEdit = "compuesta",
                                                Debe = compuesta.Debe,
                                                Haber = compuesta.Haber,
                                            });
                                            var planCuentaSubCuentas = await this._planCuentaSubCuentaQuery.GetSubCuentaId(compuesta.Id);
                                            if (planCuentaSubCuentas != null)
                                            {
                                                foreach (var subCuenta in planCuentaSubCuentas)
                                                {
                                                    planCuentaListaDto.ListaPlanes.Add(new ListaPlanes
                                                    {
                                                        Id = subCuenta.Id,
                                                        Codigo = $"{grupo.Codigo}{rubro.Codigo}{titulo.Codigo}{compuesta.Codigo}{subCuenta.Codigo}",
                                                        NombreCuenta = subCuenta.NombreCuenta,
                                                        Moneda = subCuenta.Moneda,
                                                        Nivel = subCuenta.Nivel,
                                                        Valor = subCuenta.Valor,
                                                        ModalCreate = "",
                                                        ModalEdit = "subCuenta",
                                                        Debe = subCuenta.Debe,
                                                        Haber = subCuenta.Haber,
                                                    });
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return planCuentaListaDto;
        }

        [HttpPost("select-plan-cuenta")]
        public async Task<List<Select2>> Selectplan(string searchTerm)
        {
            var planCuenta = await this.Tabla();
            var resultado = new List<ListaPlanes>();
            var select = new List<Select2>();
            if (searchTerm != "" && searchTerm!=null)
            {
                resultado = planCuenta.ListaPlanes.Where(x => x.Codigo.Contains(searchTerm)).ToList();
                //var resultado=planCuenta.ListaPlanes.Where(x=>EF.Functions.Like(x.Codigo,$"%{searchTerm}%")).ToList();
            }
            else
            {
                resultado = planCuenta.ListaPlanes;
            }

            foreach (var plan in resultado)
            {
                select.Add(new Select2
                {
                    Id = plan.Codigo,
                    Text = plan.Codigo,
                    Nombre= plan.NombreCuenta,
                    Codigo=plan.Codigo
                });
            }
            return select;
        }

        [HttpPost("data-table")]
        public async Task<ActionResult> Datatable()
        {
            var planCuenta = await this.Tabla();
            int totalRecord = 0;
            int filterRecord = 0;
            var draw = Request.Form["draw"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");

            var data = (from i in planCuenta.ListaPlanes
                        select new DatatablePlanCuentas()
                        {
                            Id = i.Id,
                            Codigo = i.Codigo,
                            Debe = i.Debe,
                            Haber = i.Haber,
                            ModalCreate = i.ModalCreate,
                            ModalEdit = i.ModalEdit,
                            Moneda = i.Moneda,
                            Nivel = i.Nivel,
                            NombreCuenta = i.NombreCuenta,
                            Valor = i.Valor,

                        });

            totalRecord = data.Count();
            // buscar por valor
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x => x.Codigo.ToLower().Contains(searchValue.ToLower()) || x.Nivel.ToLower().Contains(searchValue.ToLower()) || x.NombreCuenta.ToLower().Contains(searchValue.ToLower()) || x.Valor.ToString().ToLower().Contains(searchValue.ToLower()));
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var planCuentaListaDto = new PlanCuentaListaDto();
            planCuentaListaDto.ListaPlanes = new List<ListaPlanes>();
            var grupos = await this._planCuentasGrupoQuery.GetAll();
            if (grupos != null)
            {
                foreach (var grupo in grupos)
                {
                    planCuentaListaDto.ListaPlanes.Add(new ListaPlanes()
                    {
                        Id = grupo.Id,
                        Codigo = grupo.Codigo,
                        NombreCuenta = grupo.NombreCuenta,
                        Moneda = grupo.Moneda,
                        Nivel = grupo.Nivel,
                        Valor = grupo.Valor,
                        ModalCreate = "rubro",
                        ModalEdit = "grupo",
                        Debe = grupo.Debe,
                        Haber = grupo.Haber,
                    });
                    if (grupo.PlanCuentaRubros != null)
                    {
                        foreach (var rubro in grupo.PlanCuentaRubros)
                        {
                            planCuentaListaDto.ListaPlanes.Add(new ListaPlanes
                            {
                                Id = rubro.Id,
                                Codigo = rubro.Codigo,
                                NombreCuenta = rubro.NombreCuenta,
                                Moneda = rubro.Moneda,
                                Nivel = rubro.Nivel,
                                Valor = rubro.Valor,
                                ModalCreate = "titulo",
                                ModalEdit = "rubro",
                                Debe = rubro.Debe,
                                Haber = rubro.Haber,
                            });
                            var titulos = await this._planCuentaTituloQuery.GetOneRubroId(rubro.Id);
                            if (titulos != null)
                            {
                                foreach (var titulo in titulos)
                                {
                                    planCuentaListaDto.ListaPlanes.Add(new ListaPlanes
                                    {
                                        Id = titulo.Id,
                                        Codigo = titulo.Codigo,
                                        NombreCuenta = titulo.NombreCuenta,
                                        Moneda = titulo.Moneda,
                                        Nivel = titulo.Nivel,
                                        Valor = titulo.Valor,
                                        ModalCreate = "compuesta",
                                        ModalEdit = "titulo",
                                        Debe = titulo.Debe,
                                        Haber = titulo.Haber,
                                    });
                                    if (titulo.PlanCuentaCompuesta != null)
                                    {
                                        foreach (var compuesta in titulo.PlanCuentaCompuesta)
                                        {
                                            planCuentaListaDto.ListaPlanes.Add(new ListaPlanes
                                            {
                                                Id = compuesta.Id,
                                                Codigo = compuesta.Codigo,
                                                NombreCuenta = compuesta.NombreCuenta,
                                                Moneda = compuesta.Moneda,
                                                Nivel = compuesta.Nivel,
                                                Valor = compuesta.Valor,
                                                ModalCreate = "subCuenta",
                                                ModalEdit = "compuesta",
                                                Debe = compuesta.Debe,
                                                Haber = compuesta.Haber,
                                            });
                                            var planCuentaSubCuentas = await this._planCuentaSubCuentaQuery.GetSubCuentaId(compuesta.Id);
                                            if (planCuentaSubCuentas != null)
                                            {
                                                foreach (var subCuenta in planCuentaSubCuentas)
                                                {
                                                    planCuentaListaDto.ListaPlanes.Add(new ListaPlanes
                                                    {
                                                        Id = subCuenta.Id,
                                                        Codigo = subCuenta.Codigo,
                                                        NombreCuenta = subCuenta.NombreCuenta,
                                                        Moneda = subCuenta.Moneda,
                                                        Nivel = subCuenta.Nivel,
                                                        Valor = subCuenta.Valor,
                                                        ModalCreate = "",
                                                        ModalEdit = "subCuenta",
                                                        Debe = subCuenta.Debe,
                                                        Haber = subCuenta.Haber,
                                                    });
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return View("~/Views/PlanesDeCuentas/ListaPlanDeCuentas.cshtml", planCuentaListaDto);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
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
        [HttpGet("mostrar-grupo")]
        public async Task<IActionResult> CrearGrupo()
        {
            var grupos = await this._planCuentasGrupoQuery.GetAll();
            int codigoGrupo = 1;
            if (grupos.Count > 0)
            {
                codigoGrupo = Convert.ToInt32(grupos[grupos.Count - 1].Codigo) + 1;
            }
            return Json(
                new
                {
                    status = "ok",
                    message = "nuevo",
                    data = new
                    {
                        codigoGrupo = codigoGrupo
                    }
                }
            );
        }

        [HttpPost("store-grupo")]
        public async Task<IActionResult> StoreGrupo(PlanCuentaGrupoCreateDto planCuentaGrupoCreateDto)
        {
            if (ModelState.IsValid)
            {
                var insert = new PlanCuentaGrupo
                {
                    Codigo = planCuentaGrupoCreateDto.Codigo,
                    NombreCuenta = planCuentaGrupoCreateDto.Nombre,
                    Nivel = planCuentaGrupoCreateDto.Nivel,
                };
                if (!await this._planCuentasGrupoQuery.ValidarCodigo(insert.Codigo))
                {
                    var nuevo = await this._planCuentasGrupoQuery.Store(insert);
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
        [HttpPut("update-grupo/{id:int}")]
        public async Task<IActionResult> UpdateGrupo(int id, PlanCuentaGrupoCreateDto planCuentaGrupoCreateDto)
        {
            if (ModelState.IsValid)
            {
                var update = new PlanCuentaGrupo
                {
                    Id = id,
                    Codigo = planCuentaGrupoCreateDto.Codigo,
                    NombreCuenta = planCuentaGrupoCreateDto.Nombre,
                    Nivel = planCuentaGrupoCreateDto.Nivel,
                };
                if (!await this._planCuentasGrupoQuery.ValidarCodigoUpdate(id, update.Codigo))
                {
                    var nuevo = await this._planCuentasGrupoQuery.Update(update);
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
        [HttpDelete("delete-grupo/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            //validar a futuro dependecias

            if (await this._planCuentasGrupoQuery.Delete(id))
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