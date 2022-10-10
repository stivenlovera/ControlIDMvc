using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ControlIDMvc.Models;
using Microsoft.AspNetCore.Authorization;

namespace ControlIDMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    //[Authorize(Roles = "admin")]
    public IActionResult Index()
    {
        return View();
    }
    [Authorize(Roles = "admin")]
    public IActionResult Personas()
    {
        return View();
    }
    [Authorize(Roles = "admin")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        System.Console.WriteLine("aki en error");
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
