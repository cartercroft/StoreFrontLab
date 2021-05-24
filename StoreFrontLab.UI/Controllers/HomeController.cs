using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFrontLab.DATA.EF;
using System.Drawing;
using StoreFrontLab.UI.Utilities;
using StoreFrontLab.UI.Models;
using System.Net.Mail;
using System.Configuration;

namespace StoreFrontLab.UI.Controllers
{
    public class HomeController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                //Model state was invalid. Send the user back to the view to try again.
                return View(cvm);
            }

            //Build the message
            string message = $"From: {cvm.Name}<br />Subject: {cvm.Subject}<br />From Email: {cvm.Email}<br /><br />{cvm.Message}";

            //MailMessage - what sends the email
            //Arguments for this method were defined in the Web.config at the project level.
            MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["EmailUser"].ToString(), ConfigurationManager.AppSettings["EmailTo"].ToString(), cvm.Subject, message);

            //MailMessage Properties
            //Allow HTML formatting
            mm.IsBodyHtml = true;
            mm.Priority = MailPriority.High;

            //Respond to the sender and not the address the message was sent from
            mm.ReplyToList.Add(cvm.Email);

            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());

            //Client credentials
            client.Credentials = new NetworkCredential(
                ConfigurationManager.AppSettings["EmailUser"].ToString(),
                ConfigurationManager.AppSettings["EmailPass"].ToString()
                );

            //Try to send the email (POTENTIALLY DANGEROUS)
            try
            {
                //Attempt to send email
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"We're sorry, your request could not be completed. Please try again later.<br /><br />" +
                    $"Error Message:<br /> {ex.StackTrace}";
                return View(cvm);
            }

            //Email was sent successfully, route the user to a confirmation view.
            return View("EmailConfirmation", cvm);
        }
    }
}
