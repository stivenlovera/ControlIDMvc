using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Dtos.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using ControlIDMvc.Dtos.Login;
using ControlIDMvc.Querys;

namespace ControlIDMvc.Controllers
{
    [Route("login")]
    public class LoginControllers : Controller
    {
        private readonly ILogger<LoginControllers> _logger;
        private readonly UsuarioQuery _usuarioQuery;
        private readonly RolesUsuarioQuery _rolesUsuarioQuery;

        public LoginControllers(
            ILogger<LoginControllers> logger,
            UsuarioQuery usuarioQuery,
            RolesUsuarioQuery rolesUsuarioQuery
        )
        {
            _logger = logger;
            this._usuarioQuery = usuarioQuery;
            this._rolesUsuarioQuery = rolesUsuarioQuery;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Login/Index.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Store(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Login/Index.cshtml");
            }

            var user = await this._usuarioQuery.Login(loginDto.User, loginDto.Password);
            if (user != null)
            {
                //verificar perfil
                string perfil="profile_user.jpg";
                if (user.Persona.perfil!=null)
                {
                    perfil=user.Persona.perfil.Path;
                }
                /*construyendo claves*/
                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Name,$"{user.Persona.Nombre} {user.Persona.Apellido}"),
                    new Claim("UsuarioId",user.Persona.Id.ToString()),
                    new Claim("Imagen",perfil),
                };
                /*obteiendo roles*/
                var roles = await this._rolesUsuarioQuery.GetRoles(user.Id);
                foreach (var rol in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }

                var claimsIndentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIndentity));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index));
        }
    }
}