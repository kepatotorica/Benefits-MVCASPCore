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
        
        public IActionResult About()
        {
            ViewData["Message"] = "Simple benefit manager";
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            string user = "Kepa Totorica";
            ViewData["Message"] = user;

            return View();
        }

        [HttpPost]
        public IActionResult Contact(string FName, string LName)
        {
            ViewData["Added"] = true;
            ViewData["NameAdded"] = FName;
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

//sources I used to help me learn how to use c# mvc
//http://www.tutorialsteacher.com/mvc/viewdata-in-asp.net-mvc