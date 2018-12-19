using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCASPCore.Models;

namespace MVCASPCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult Test()
        {
            return View();
        }

        public IActionResult customers()
        {
            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            string user = "Kepa Totorica";
            ViewData["Message"] = "Kepa Totorica";
            List<Relative> temp = new List<Relative>();
            temp.Add(new Relative() { FName = "Julen", LName = "Totorica", Relation = "brother" });
            temp.Add(new Relative() { FName = "Mitxel", LName = "Totorica", Relation = "brother" });

            ViewData["Relatives"] = temp;
            ViewData["User"] = user;

            return View();
        }

        [HttpPost]
        public IActionResult Contact(string FName, string LName)
        {
            ViewData["Added"] = true;
            ViewData["NameAdded"] = FName;
            //Debug.Print("http://asdfg " + firstName + "\n");//seeing if I can get a value from Contact.cshtml into my view on a Post
            return Contact();
        }

            public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

//public class Relatives
//{

//    public string Name { get; set; }
//    public string Relation { get; set; }
//    public Relatives(string name, string relation)
//    {
//        Name = name;
//        Relation = relation;
//    }
//    String relativeName;
//    String relativeRelation;
//}
//sources I used to help me learn how to use c# mvc
//http://www.tutorialsteacher.com/mvc/viewdata-in-asp.net-mvc

//TODO look into Razor Pages