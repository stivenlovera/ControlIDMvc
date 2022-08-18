using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ControlIDMvc.Models;

namespace ControlIDMvc.Controllers
{
    public class AreaController :Controller
    {
        private readonly DBContext _dbContext;
        public AreaController(DBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public ActionResult Index()
        {
            return View("~/Views/Area/Lista.cshtml");
        }
        public ActionResult Create()
        {
            return View("~/Views/Area/Create.cshtml");
        }
    }
}