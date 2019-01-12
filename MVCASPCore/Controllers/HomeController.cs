using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCASPCore.Models;
using MVCASPCore;
using Microsoft.AspNetCore.Http;




namespace MVCASPCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly cSharpContext _context;

        public HomeController(cSharpContext context)
        {
            _context = context;
        }

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

            //ViewData["Benefits"] = baseBenefits;
            //ViewData["PayCheck"] = basePay;
            //ViewData["PerPerson"] = baseDependant;
            //ViewData["Discount"] = discount * 100;
            //ViewData["AName"] = (1 - discount) * baseDependant;
            //ViewData["numChecks"] = 26;

            //var config = new EmailConfiguration();
            //config.SmtpPassword = "1q2ww3eee4rrrr";
            //config.SmtpPort = 465;
            //config.SmtpUsername = "contracthub749@gmail.com";
            //config.SmtpServer = "smtp.gmail.com";

            //var service = new EmailService(config);
            //var message = new EmailMessage();
            //var sender = new EmailAddress();
            //var reciever = new EmailAddress();

            //sender.Address = "contracthub749@gmail.com";
            //sender.Name = "contracthub749";
            //reciever.Address = "kepatoto@gmail.com";
            //reciever.Name = "kepatoto@gmail.com";

            //message.FromAddresses.Add(sender);
            //message.ToAddresses.Add(reciever);
            //message.Subject = "message";
            //message.Content = "content";

            //service.Send(message);

            return View();
        }

        [HttpPost]
        public IActionResult Contact(string email, string text)
        {
            //TODO make it so it uses the appsettings.json
            //TODO should I introduce a ticket system?
            
            if (email == null)
            {
                
                if (HttpContext.Session.GetInt32("AId") != null)
                {
                    //why can't I await here
                    var admin = _context.Admin.FirstOrDefault(a => a.AId == HttpContext.Session.GetInt32("AId"));
                    email = admin.Email;
                }
                else
                {
                    email = "kepatoto@gmail.com";
                    //return;//we faild, no email was entered maybe fix this later
                }
            }
            if(text == null)
            {
                text = "The customer did not fill anything in";
            }
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
            message.FromAddresses.Add(sender);
            
            //internal message
            reciever.Address = "kepatoto@gmail.com";
            reciever.Name = "kepa";
            message.ToAddresses.Add(reciever);

            message.Subject = "Question from " + email;
            message.Content = text + "\n\nrespond to " + email;

            service.Send(message);

            //external message
            reciever.Address = email;
            reciever.Name = email;
            message.ToAddresses.Add(reciever);

            message.Subject = "Thank your for contacting MVCASPCore!";
            message.Content = "We have recieved your message and have forwarded it along to Kepa, he will be with you shortly \n\nThank you, \nMVCASPCore";
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