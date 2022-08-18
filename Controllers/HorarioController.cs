using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ControlIDMvc.Models;

namespace ControlIDMvc.Controllers
{
    public class HorarioController : Controller
    {
        private readonly DBContext _dbContext;
        public HorarioController(DBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public ActionResult Index()
        {
            return View("~/Views/Horario/Lista.cshtml");
        }
        public ActionResult Create()
        {
            return View("~/Views/Horario/Create.cshtml");
        }
    }
}