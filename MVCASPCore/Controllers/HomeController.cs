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
    public class HomeController : Controller //what does inheriting the controller do?
    {
        private readonly cSharpContext _context;

        public HomeController(cSharpContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This serves the about page for the website, it is a descrition of the website as a whole
        /// </summary>
        /// <returns> A view that describes the website as a whole </returns>
        public IActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Returns a faq with a contact form on it
        /// </summary>
        /// <returns> a view containing a faq and a contact form</returns>
        [HttpGet]
        public IActionResult Contact()
        {
            double basePay = 2000;
            double baseBenefits = 1000;
            double baseDependant = 500;
            double discount = .1;

            return View();
        }

        /// <summary>
        /// This sends an email to the email provided in the contact form and a message
        /// from the same form. If an admin is logged in, that email will be used.
        /// This also sends me a message so I can find out who needs help, with what, and
        /// how to reach them
        /// </summary>
        /// <param name="email"> The email of the person asking for help. </param>
        /// <param name="text"> The message to be sent to kepatoto@gmail.com (me) </param>
        /// <returns> returns to an action with either a failure (Failed) or success (Sent) view </returns>
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

        /// <summary>
        /// Returns a view after an email has been sent sucessfully
        /// </summary>
        /// <returns> A view of a successful email send</returns>
        public IActionResult Sent()
        {
            return View();
        }

        /// <summary>
        /// Same as Sent() but when we fail
        /// </summary>
        /// <returns> A view when something went wrong during the email process</returns>
        public IActionResult Failed()
        {
            return View();
        }

        /// <summary>
        /// Returns a view containing my cookie policy
        /// </summary>
        /// <returns> A view with my cookies policy </returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Honestly I don't know what this is doing, it was auto generated. I would assume that 
        /// it's primary function is to catch errors, but if it is, it is not set up properly
        /// you can access this page from /Home/Error
        /// but it says that is shouldn't be used for development mode.
        /// So I assume it is the error page that a user would see if they were using the production
        /// app
        /// </summary>
        /// <returns> A view displaying an error </returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

//sources I used to help me learn how to use c# mvc
//http://www.tutorialsteacher.com/mvc/viewdata-in-asp.net-mvc