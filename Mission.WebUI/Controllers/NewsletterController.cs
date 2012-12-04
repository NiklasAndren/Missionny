using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mission.Domain.Entities;
using Mission.Domain.Repositories.Abstract;
using System.Net.Mail;
using Mission.WebUI.ViewModels;

namespace Mission.WebUI.Controllers
{
    public class NewsletterController : Controller
    {
        private IRepository<Newsletter> _newsletterRepo;
        private IRepository<Subscriber> _subscriberRepo;
        public NewsletterController(IRepository<Newsletter> newsletterRepo, IRepository<Subscriber> subscriberRepo)
        {
            _newsletterRepo = newsletterRepo;
        }

        public ActionResult SendMail()
        {
            var sendmail = new vm_SendMail();

            return View(sendmail);
        }

        [HttpPost]
        public ActionResult SendMail(vm_SendMail sendmail)
        {
            if (!string.IsNullOrEmpty(sendmail.Subscriber.Name) && !string.IsNullOrEmpty(sendmail.Subscriber.Email))
            {
                var newsub = new Subscriber();

                newsub.ID = Guid.NewGuid();
                newsub.Name = sendmail.Subscriber.Name;
                newsub.Email = sendmail.Subscriber.Email;
                newsub.City = sendmail.Subscriber.City;
                newsub.SubscriberType = sendmail.Subscriber.SubscriberType;

                _subscriberRepo.Save(newsub);
            }

            if (!string.IsNullOrEmpty(sendmail.Newsletter.Subject) && !string.IsNullOrEmpty(sendmail.Newsletter.Message))
            {
                sendmail.Newsletter.ID = Guid.NewGuid();
                sendmail.Newsletter.Date = DateTime.Now;

                _newsletterRepo.Save(sendmail.Newsletter);

                List<Subscriber> emails = _subscriberRepo.FindAll(e => e.SubscriberType == sendmail.Subscriber.SubscriberType).ToList();

                foreach (var email in emails)
                {
                    try
                    {
                        MailMessage mail = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                        mail.From = new MailAddress("jctest1337@gmail.com");
                        mail.To.Add(email.Email);
                        mail.Subject = sendmail.Newsletter.Subject;
                        mail.Body = sendmail.Newsletter.Message;

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
            }
            return View();
            
        }
    }
}
