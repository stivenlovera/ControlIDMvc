using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Dtos.RolUsuario;
using ControlIDMvc.Dtos.Usuario;
using ControlIDMvc.Querys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ControlIDMvc.Controllers
{
    [Route("roles")]
    public class RolController : Controller
    {
        private readonly ILogger<RolController> _logger;
        private readonly PersonaQuery _personaQuery;
        private readonly RolQuery _rolQuery;
        private readonly RolesUsuarioQuery _rolesUsuarioQuery;
        private readonly UsuarioQuery _usuarioQuery;

        public RolController(
            ILogger<RolController> logger,
            PersonaQuery personaQuery,
            RolQuery rolQuery,
            RolesUsuarioQuery rolesUsuarioQuery,
            UsuarioQuery usuarioQuery
            )
        {
            _logger = logger;
            this._personaQuery = personaQuery;
            this._rolQuery = rolQuery;
            this._rolesUsuarioQuery = rolesUsuarioQuery;
            this._usuarioQuery = usuarioQuery;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var personas = await this._personaQuery.GetAll();
            ViewData["personas"] = personas;
            var roles = await this._rolQuery.GetAll();
            ViewData["roles"] = roles;
            return View("~/Views/Roles/Lista.cshtml");
        }

        [HttpPost("data-table-persona")]
        public ActionResult DataTablePersona()
        {
            var dataTable = this._personaQuery.DataTable(Request);
            return Json(dataTable);
        }

        [HttpPost("data-table-rol")]
        public ActionResult DataTableRol()
        {
            var dataTable = this._personaQuery.DataTable(Request);
            return Json(dataTable);
        }

        [HttpPost("store")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Store(UserRolesCreateDto usuarioRolesCreateDto)
        {
            if (!ModelState.IsValid)
            {
                System.Console.WriteLine(ModelState.ErrorCount);
                return Json(ModelState);
            }
            if (usuarioRolesCreateDto.Id==0)
            {
                UsuarioCreateDto usaurio = new UsuarioCreateDto
                {
                    Password = usuarioRolesCreateDto.Password,
                    User = usuarioRolesCreateDto.User,
                    PersonaId = usuarioRolesCreateDto.PersonaId
                };
                var nuevo_usuario = await this._usuarioQuery.Store(usaurio);
                foreach (var rol in usuarioRolesCreateDto.RolIds)
                {
                    RolUsuarioCreateDto rolUsuarioCreateDto = new RolUsuarioCreateDto()
                    {
                        RolId = Convert.ToInt32(rol),
                        UsuarioId = nuevo_usuario.Id
                    };
                    await this._rolesUsuarioQuery.Store(rolUsuarioCreateDto);
                }
            }
            else
            {
                var removerRoles = await this._rolesUsuarioQuery.DeleteUsuarioId(usuarioRolesCreateDto.Id);
                foreach (var rol in usuarioRolesCreateDto.RolIds)
                {
                    RolUsuarioCreateDto rolUsuarioCreateDto = new RolUsuarioCreateDto()
                    {
                        RolId = Convert.ToInt32(rol),
                        UsuarioId = usuarioRolesCreateDto.Id
                    };
                    await this._rolesUsuarioQuery.Store(rolUsuarioCreateDto);
                }
            }
            return Json(new
            {
                status = "ok",
                message = "guardado correctamente"
            });
        }

        [HttpGet("show/{id:int}")]
        public async Task<ActionResult> Show(int id)
        {
            var persona = await this._personaQuery.Show(id);
            var usuario = await this._usuarioQuery.ShowPersonaId(persona.Id);
            if (usuario == null)
            {
                usuario = new UsuarioDto()
                {
                    User = "",
                    PersonaId = 0,
                    Password = "",
                    Id = 0
                };
            }
            else
            {
                var userData = usuario;
            }
            var rolesUsuario = await this._rolesUsuarioQuery.GetRolesId(usuario.Id);
            return Json(new
            {
                status = "ok",
                message = "guardado correctamente",
                data = new
                {
                    id = usuario.Id,
                    PersonaId = persona.Id,
                    nombre = persona.Nombre,
                    apellido = persona.Apellido,
                    direccion = persona.Dirrecion,
                    ci = persona.Ci,
                    //roles
                    usuario = usuario.User,
                    password = usuario.Password,
                    roles = rolesUsuario
                }
            });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}