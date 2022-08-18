using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ControlIDMvc.Models;

namespace ControlIDMvc.Controllers;

public class PersonaController : Controller
{
    private readonly DBContext _dbContext;
    public PersonaController(DBContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public ActionResult Index()
    {
        return View("~/Views/Persona/Lista.cshtml");
    }
    public ActionResult Create()
    {
        /*  var usuario = _dbContext.Usuario.First();
         System.Console.WriteLine("respuesta"+usuario.grupo_id);  */
        return View("~/Views/Persona/Create.cshtml");
    }
     // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase postedFile)
        {
            System.Console.WriteLine("store");
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
}
