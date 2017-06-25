using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AntoineBlog.Models;
using System.Threading.Tasks;
using System.Net.Mail;

namespace AntoineBlog.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //GET
        public ActionResult Contact()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactMessage contact)
        {
            var es = new EmailService();
            var msg = new IdentityMessage();
            msg.Destination = ConfigurationManager.AppSettings["ContactEmail"];
            msg.Body = "You have been sent a message from " + contact.Name + " (" + contact.Email + ") with the following contents. <br/><br/>\"" + contact.Message + "\"";
            msg.Subject = "Message received through Antoine's blog";
            es.SendAsync(msg);

            //ViewBag.Message = "Your message was sent successfully. Thank you!";
            return Redirect("http://jantoine-site.azurewebsites.net");
        }

        [HttpGet]
        public ActionResult Contact2()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact2(EmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var from = model.FromEmail;
                    var email = new MailMessage(from, ConfigurationManager.AppSettings["emailto"])
                    {
                        Subject = model.Subject,
                        Body = $"<strong>{model.FromName}</strong> left you a message: {model.Body}. The user's email is <strong>{model.FromEmail}</strong>",
                        IsBodyHtml = true
                    };

                    var svc = new PersonalEmail();
                    await svc.SendAsync(email);
                    return RedirectToAction("Sent");
                    //return File("~/ContactEmail1.html", "text/html");
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    await Task.FromResult(0);
                }
            }
            return View(model);
        }

        public ActionResult Sent()
        {
            return View();
        }
    }
}