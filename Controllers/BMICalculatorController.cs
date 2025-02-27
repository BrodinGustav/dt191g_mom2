using System.Diagnostics;
using Microsoft.AspNetCore.Mvc; //Använder mvc-mönster
using Microsoft.AspNetCore.Routing.Constraints;
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
        //Försöker hämta BMI och kategori från sessionen
        var lastBmi = HttpContext.Session.GetString("LastBMI");
        var bmiCategory = HttpContext.Session.GetString("BMICategory");

        if (string.IsNullOrEmpty(lastBmi) || string.IsNullOrEmpty(bmiCategory))
        {
            return View();
        }

        //Om sessiondata finns, skicka den till vyn
        decimal bmi = decimal.Parse(lastBmi);                               // Konverterar sträng till decimal
        return View(new BMIModel { BMI = bmi, Category = bmiCategory });

    }


    //Metoder

    //Tar emot vikt och längd, beräknar BMI och skickar resultatet till vyn

    [HttpPost]  //Skickar resultat till vyn (Index) via HttpPost
    public IActionResult Calculate(decimal weight, decimal height)
    {
        //Kontroll om input är giltiga
        if (height <= 0 || weight <= 0)
        {
            ViewBag.Message = "Ange giltiga värden för vikt och längd!";
            return View("Index"); //Skickar användaren tillbaka till vyn Index
        }

        //Beräknar BMI
        height = height / 100;                          //Omvandlar cm till meter
        decimal bmi = weight / (height * height);

        //Lagrar uträknad kategori i variabel
        string category = GetBMICategory(bmi);

        //Skicka BMI till vyn med ViewBag
        ViewBag.BMI = bmi.ToString("0.00");

        //Skicka vikt och längd till vyn med ViewData
        ViewData["Weight"] = weight;
        ViewData["Height"] = height;

        //Spara BMI och kategori i sessionen för att kunna användas mellan sidbyten
        HttpContext.Session.SetString("LastBMI", bmi.ToString("0.00"));
        HttpContext.Session.SetString("BMICategory", category);

        //Skicka data till vyn via instans av modell. Användaren skickas till vyn Result.
        return View("Result", new BMIModel { BMI = bmi, Category = category });
    }


    // Metod för att bestämma BMI-kategori baserat på BMI-värdet
    private string GetBMICategory(decimal bmi)
    {
        if (bmi < 18.5m) return "Undervikt";    //Använder "m" för att markerar att värdet är av typen decimal
        if (bmi < 25m) return "Normalvikt";
        if (bmi < 30m) return "Övervikt";
        return "Fetma";
    }
}







