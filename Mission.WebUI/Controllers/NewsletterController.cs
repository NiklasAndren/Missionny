using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mission.Domain.Entities;
using Mission.Domain.Repositories.Abstract;
using System.Net.Mail;

namespace Mission.WebUI.Controllers
{
    public class NewsletterController : Controller
    {
        private IRepository<Newsletter> _newsletterRepo;
         public NewsletterController(IRepository<Newsletter> newsletterRepo)
        {
            _newsletterRepo = newsletterRepo;
        }

        public ActionResult SendMail()
        {
            var newsletter = new Newsletter();

            return View(newsletter);
        }

        [HttpPost]
        public ActionResult SendMail(Newsletter newsletter)
        {
            newsletter.ID = Guid.NewGuid();
            newsletter.Date = DateTime.Now;

            _newsletterRepo.Save(newsletter);

            List<string> emails = new List<string>();

            emails.Add("kaktus46@hotmail.com");
            emails.Add("niklas.andren@hotmail.com");
            emails.Add("rizz@live.se");

            foreach (var email in emails)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("jctest1337@gmail.com");
                    mail.To.Add(email);
                    mail.Subject = newsletter.Subject;
                    mail.Body = newsletter.Message;

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("jctest1337@gmail.com", "jctest123456");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                    ViewBag.StatusMessage = "mail Send";
                }
                catch (Exception ex)
                {
                    ViewBag.StatusMessage = ex;
                }
            }
          
            return View();
            
        }
    }
}
