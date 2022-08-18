using Microsoft.AspNetCore.Mvc;
namespace ControlIDMvc.Controllers
{
    public class ReglasAccesoController:Controller
    {
           private readonly DBContext _dbContext;
        public ReglasAccesoController(DBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public ActionResult Index()
        {
            return View("~/Views/ReglasAcceso/Lista.cshtml");
        }
        public ActionResult Create()
        {
            return View("~/Views/ReglasAcceso/Create.cshtml");
        }
    }
}