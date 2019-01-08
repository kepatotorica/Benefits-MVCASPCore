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
            //change these to effect the about page
            double basePay = 2000;
            double baseBenefits = 1000;
            double baseDependant = 500;
            double discount = .1;

            ViewData["Benefits"] = baseBenefits;
            ViewData["PayCheck"] = basePay;
            ViewData["PerPerson"] = baseDependant;
            ViewData["Discount"] = discount * 100;
            ViewData["AName"] = (1 - discount) * baseDependant;
            ViewData["numChecks"] = 26;

            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            string user = "Kepa Totorica";
            ViewData["Message"] = user;

            double basePay = 2000;
            double baseBenefits = 1000;
            double baseDependant = 500;
            double discount = .1;

            ViewData["Benefits"] = baseBenefits;
            ViewData["PayCheck"] = basePay;
            ViewData["PerPerson"] = baseDependant;
            ViewData["Discount"] = discount * 100;
            ViewData["AName"] = (1 - discount) * baseDependant;
            ViewData["numChecks"] = 26;

            return View();
        }

        //[HttpPost]
        //public IActionResult Contact(string FName, string LName)
        //{
        //    ViewData["Added"] = true;
        //    ViewData["NameAdded"] = FName;
        //    return Contact();
        //}

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