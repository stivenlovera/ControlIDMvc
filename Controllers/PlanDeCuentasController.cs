using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Dtos.PlanCuentaGrupo;
using ControlIDMvc.Dtos.PlanCuentaRubro;
using ControlIDMvc.Dtos.PlanCuentaTitulo;
using ControlIDMvc.Dtos.PlanCuentaCompuesta;
using ControlIDMvc.Dtos.PlanCuentaSubCuenta;
using ControlIDMvc.Entities;
using ControlIDMvc.Querys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ControlIDMvc.Controllers
{
    [Route("PlanDeCuentas")]
    public class PlanDeCuentasController : Controller
    {
        private readonly ILogger<PlanDeCuentasController> _logger;
        private readonly PlanCuentasGrupoQuery _planCuentasGrupoQuery;
        private readonly PlanCuentaRubroQuery _planCuentaRubroQuery;
        private readonly PlanCuentaTituloQuery _planCuentaTituloQuery;
        private readonly PlanCuentaCompuestaQuery _planCuentaCompuestaQuery;
        private readonly PlanCuentaSubCuentaQuery _planCuentaSubCuentaQuery;

        public PlanDeCuentasController(ILogger<PlanDeCuentasController> logger, PlanCuentasGrupoQuery planCuentasGrupoQuery, PlanCuentaRubroQuery planCuentaRubroQuery, PlanCuentaTituloQuery planCuentaTituloQuery, PlanCuentaCompuestaQuery planCuentaCompuestaQuery, PlanCuentaSubCuentaQuery planCuentaSubCuentaQuery)
        {
            _logger = logger;
            this._planCuentasGrupoQuery = planCuentasGrupoQuery;
            this._planCuentaRubroQuery = planCuentaRubroQuery;
            this._planCuentaTituloQuery = planCuentaTituloQuery;
            this._planCuentaCompuestaQuery = planCuentaCompuestaQuery;
            this._planCuentaSubCuentaQuery = planCuentaSubCuentaQuery;
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
                        Modal = "rubro",
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
                                Modal = "titulo",
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
                                        Modal = "compuesta",
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
                                                Modal = "subCuenta",
                                                Debe = compuesta.Debe,
                                                Haber = compuesta.Haber,
                                            });
                                            var planCuentaSubCuentas = await this._planCuentaSubCuentaQuery.GetOneCompuestaId(compuesta.Id);
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
                                                        Modal = "",
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
        [HttpPost("crear-grupo")]
        public async Task<IActionResult> StoreGrupo(PlanCuentaGrupoCreateDto planCuentaGrupoCreateDto)
        {
            var insert = new PlanCuentaGrupo
            {
                Codigo = planCuentaGrupoCreateDto.Codigo,
                NombreCuenta = planCuentaGrupoCreateDto.Nombre,
                Nivel = planCuentaGrupoCreateDto.Nivel,
                Debe = 100

            };
            var nuevo = await this._planCuentasGrupoQuery.Store(insert);

            System.Console.WriteLine(nuevo.Id);
            return Json(
                new
                {
                    status = "ok",
                    message = "datos recibidos",
                    data = nuevo
                }
            );
        }
        [HttpPost("crear-rubro")]
        public async Task<IActionResult> StoreRubro(PlanCuentaRubroCreateDto planCuentaRubroCreateDto)
        {
            var insert = new PlanCuentaRubro
            {
                Codigo = $"{planCuentaRubroCreateDto.PlanCuentaGrupoId}{planCuentaRubroCreateDto.Codigo}",
                NombreCuenta = planCuentaRubroCreateDto.Nombre,
                Nivel = planCuentaRubroCreateDto.Nivel,
                PlanCuentaGrupoId = planCuentaRubroCreateDto.PlanCuentaGrupoId,
                Debe = 100

            };
            var nuevo = await this._planCuentaRubroQuery.Store(insert);

            System.Console.WriteLine(nuevo.Id);
            return Json(
                new
                {
                    status = "ok",
                    message = "datos recibidos",
                    data = nuevo
                }
            );
        }
        [HttpPost("crear-titulo")]
        public async Task<IActionResult> StoreTitulo(PlanCuentaTituloCreateDto planCuentaTituloCreateDto)
        {
            var insert = new PlanCuentaTitulo
            {
                Codigo = $"{planCuentaTituloCreateDto.PlanCuentaRubroId}{planCuentaTituloCreateDto.Codigo}",
                NombreCuenta = planCuentaTituloCreateDto.Nombre,
                Nivel = planCuentaTituloCreateDto.Nivel,
                PlanCuentaRubroId = planCuentaTituloCreateDto.PlanCuentaRubroId,
                Debe = 100

            };
            var nuevo = await this._planCuentaTituloQuery.Store(insert);

            System.Console.WriteLine(nuevo.Id);
            return Json(
                new
                {
                    status = "ok",
                    message = "datos recibidos",
                    data = nuevo
                }
            );
        }
        [HttpPost("crear-compuesta")]
        public async Task<IActionResult> StoreCompuesta(PlanCuentaCompuestaCreateDto planCuentaCompuestaCreateDto)
        {
            var insert = new PlanCuentaCompuesta
            {
                Codigo = $"{planCuentaCompuestaCreateDto.PlanCuentaTituloId}{planCuentaCompuestaCreateDto.Codigo}",
                NombreCuenta = planCuentaCompuestaCreateDto.Nombre,
                Nivel = planCuentaCompuestaCreateDto.Nivel,
                PlanCuentaTituloId = planCuentaCompuestaCreateDto.PlanCuentaTituloId,
                Debe = 100

            };
            var nuevo = await this._planCuentaCompuestaQuery.Store(insert);

            System.Console.WriteLine(nuevo.Id);
            return Json(
                new
                {
                    status = "ok",
                    message = "datos recibidos",
                    data = nuevo
                }
            );
        }
        [HttpPost("crear-subcuenta")]
        public async Task<IActionResult> StoreSubCuenta(PlanCuentaSubCuentaCreateDto planCuentaSubCuentaCreateDto)
        {
            var insert = new PlanCuentaSubCuenta
            {
                Codigo = $"{planCuentaSubCuentaCreateDto.PlanCuentaCompuestaId}{planCuentaSubCuentaCreateDto.Codigo}",
                NombreCuenta = planCuentaSubCuentaCreateDto.Nombre,
                Nivel = planCuentaSubCuentaCreateDto.Nivel,
                PlanCuentaCompuestaId = planCuentaSubCuentaCreateDto.PlanCuentaCompuestaId,
                Debe = 100

            };
            var nuevo = await this._planCuentaSubCuentaQuery.Store(insert);

            System.Console.WriteLine(nuevo.Id);
            return Json(
                new
                {
                    status = "ok",
                    message = "datos recibidos",
                    data = nuevo
                }
            );
        }

    }
}