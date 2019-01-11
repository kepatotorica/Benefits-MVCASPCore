﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCASPCore.Models;
using MVCASPCore;



namespace MVCASPCore.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult About()
        {
            //change these to effect the about page

            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {

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

            var config = new EmailConfiguration();
            config.SmtpPassword = "1q2ww3eee4rrrr";
            config.SmtpPort = 465;
            config.SmtpUsername = "contracthub749@gmail.com";
            config.SmtpServer = "smtp.gmail.com";

            var service = new EmailService(config);
            var message = new EmailMessage();
            var sender = new EmailAddress();
            var reciever = new EmailAddress();

            sender.Address = "contracthub749@gmail.com";
            sender.Name = "contracthub749";
            reciever.Address = "kepatoto@gmail.com";
            reciever.Name = "kepatoto";

            message.FromAddresses.Add(sender);
            message.ToAddresses.Add(reciever);
            message.Subject = "message";
            message.Content = "content";

            service.Send(message);

            return View();
        }

        [HttpPost]
        public IActionResult Contact(string FName, string LName)
        {
            //TODO make it so it uses the appsettings.json
            var config = new EmailConfiguration();
            config.SmtpPassword = "1q2ww3eee4rrrr";
            config.SmtpPort = 465;
            config.SmtpUsername = "contracthub749@gmail.com";
            config.SmtpServer = "smtp.gmail.com";

            var service = new EmailService(config);
            var message = new EmailMessage();
            var sender = new EmailAddress();
            var reciever = new EmailAddress();

            sender.Address = "contracthub749@gmail.com";
            sender.Name = "contracthub749";
            reciever.Address = "kepatoto@gmail.com";
            reciever.Name = "kepatoto";

            message.FromAddresses.Add(sender);
            message.ToAddresses.Add(reciever);
            message.Subject = "message";
            message.Content = "content";

            service.Send(message);
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