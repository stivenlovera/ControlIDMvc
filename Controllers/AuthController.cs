using System.Linq.Expressions;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Dtos.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;    
namespace ControlIDMvc.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AuthController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> IdentityManager
        )
        {
            this._userManager = userManager;
            this._signInManager = IdentityManager;
        }
        [HttpGet]
        public ActionResult login()
        {
            return RedirectToPage("~/Views/Auth/Login");
        }
        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(SingIn singIn)
        {
            if (ModelState.IsValid)
            {
                var usuario = new IdentityUser()
                {
                    Email = singIn.usuario,
                    UserName = singIn.usuario
                };
                var resultado = await _userManager.CreateAsync(usuario, password: singIn.password);
                var claimsPersonalizados = new List<Claim>(){
                    new Claim(Constante.ClaimEmpresaId,usuario.Id)
                };
                await this._userManager.AddClaimsAsync(usuario, claimsPersonalizados);
                if (resultado.Succeeded)
                {
                    await _signInManager.SignInAsync(usuario, isPersistent: true);
                    return RedirectToAction("Persona", "Index");
                }
                else
                {
                    foreach (var error in resultado.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(singIn);
                }
            }
            return View(singIn);
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SingIn(SingIn singIn)
        {
            if (!ModelState.IsValid)
            {
                return View(singIn);
            }
            var resultado = await _signInManager.PasswordSignInAsync(
               singIn.usuario,
               singIn.password,
               singIn.recuerdame,
               lockoutOnFailure: false
            );
            if (resultado.Succeeded)
            {
                return RedirectToAction("Persona", "Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Nombre de usuario  o password es incorrecto");
                return View(singIn);
            }
        }
    }
    public static class Constante
    {
        public const string ClaimEmpresaId = "empresa1";
    }
}