using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Benefacts.Models;
using Benefacts;
using Microsoft.AspNetCore.Http;




namespace Benefacts.Controllers
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

            return View();
        }

        [HttpPost]
        public IActionResult Contact(string email, string text)
        {
            //TODO should I introduce a ticket system?
            
            if (email == null)
            {
                
                if (HttpContext.Session.GetInt32("AId") > -1)
                {
                    //why can't I await here
                    var admin = _context.Admin.FirstOrDefault(a => a.AId == HttpContext.Session.GetInt32("AId"));
                    if(admin.Email != null)
                    {
                        email = admin.Email;
                    }
                    else
                    {
                        return RedirectToAction("Failed", "Home");
                    }
                }
                else
                {
                    email = "kepatoto@gmail.com";
                }
            }
            if(text == null)
            {
                text = "The customer did not fill anything in";
                return RedirectToAction("Failed", "Home");
            }
            else
            {
                var config = new EmailConfiguration();
                var service = new EmailService(config);
                var message = new EmailMessage();
                var sender = new EmailAddress();
                var reciever = new EmailAddress();


                sender.Address = config.SmtpUsername;
                sender.Name = "Benefacts";
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

                message.Subject = "Thank your for contacting Benefacts!";
                message.Content = "We have recieved your message and have forwarded it along to Kepa, he will be with you shortly \n\nThank you, \nBenefacts";
                service.Send(message);
            }

            //return Contact();
            return RedirectToAction("Sent", "Home");
        }

        public IActionResult Sent()
        {
            return View();
        }

        public IActionResult Failed()
        {
            return View();
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