using System.Diagnostics;
using Microsoft.AspNetCore.Mvc; //Använder mvc-mönster
using Moment2.Models;           //Importrerar modellen // Importera modellen

namespace Moment2.Controllers;  //

public class BMICalculatorController : Controller //Ärver av klassen Controller
{


    //Returnerar vyer

    public IActionResult Index()
    {
        return View();
    }

    // Visar en informationssida om BMI-kategorier
    public IActionResult Info()
    {
        return View();
    }

    // Visar BMI-resultatet
    public IActionResult Result()
    {
        return View();
    }


    //Metoder

    //Tar emot vikt och längd, beräknar BMI och skickar resultatet till vyn
    
    [HttpPost]  //Skickar resultat till vyn (Index) via HttpPost
    public IActionResult Calculate(double weight, double height)
    {
        //Kontroll om input är giltiga
        if (height <= 0 || weight <= 0)
        {
            ViewBag.Message = "Ange giltiga värden för vikt och längd!";
            return View("Index");
        }

        //Beräknar BMI
        double bmi = weight / (height * height);
        string category = GetBMICategory(bmi);

        //Skicka BMI till vyn med ViewBag
        ViewBag.BMI = bmi.ToString("0.00");

        //Skicka vikt och längd till vyn med ViewData
        ViewData["Weight"] = weight;
        ViewData["Height"] = height;

        //Spara BMI och kategori i sessionen för att kunna användas mellan sidbyten
        HttpContext.Session.SetString("LastBMI", bmi.ToString("0.00"));
        HttpContext.Session.SetString("BMICategory", category);

        //Skicka data till vyn via instans av modell
        return View("Result", new BMIModel { BMI = bmi, Category = category });
    }


    // Metod för att bestämma BMI-kategori baserat på BMI-värdet
    private string GetBMICategory(double bmi)
    {
        if (bmi < 18.5) return "Undervikt";
        if (bmi < 25) return "Normalvikt";
        if (bmi < 30) return "Övervikt";
        return "Fetma";
    }
}







