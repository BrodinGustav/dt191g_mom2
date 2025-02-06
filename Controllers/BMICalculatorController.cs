using System.Diagnostics;
using Microsoft.AspNetCore.Mvc; //Använder mvc-mönster
using Moment2.Models;

namespace Moment2.Controllers;  //

public class BMICalculatorController : Controller //Ärver av klassen Controller
{
    private readonly ILogger<BMICalculatorController> _logger;

    public BMICalculatorController(ILogger<BMICalculatorController> logger)
    {
        _logger = logger;
    }

    //Returnerar vyer

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Info()
    {
        return View();
    }

        public IActionResult Result()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
