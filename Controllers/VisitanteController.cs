using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ControlIDMvc.Controllers
{
     [Authorize]
    public class VisitanteController : Controller
    {
        private readonly DBContext _dbContext;
        public VisitanteController(DBContext dbContext)
        {

            this._dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View("~/Views/Visitante/Lista.cshtml");
        }

       /*  public IActionResult Create()
        {
            var usuario = _dbContext.Usuario.First();
            System.Console.WriteLine("respuesta" + usuario.grupo_id);
            return View("~/Views/Visitante/Create.cshtml");
        } */

    }
}