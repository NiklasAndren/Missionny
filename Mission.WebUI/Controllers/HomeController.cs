using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mission.Domain.Entities.FakeData;
using Mission.Domain.Repositories;
using Mission.Domain.Entities;
using System.Data.Entity;
using Mission.Domain.Repositories.Abstract;
using Mission.WebUI.Infrastructure;
using Mission.WebUI.ViewModels;
using System.IO;


namespace Mission.WebUI.Controllers
{
    public class HomeController : Controller
    {
         private IRepository<Post> _postRepo;
         private IRepository<Subscriber> _subscriberRepo;
         private IRepository<Customers> _customerRepo;
         private IRepository<AboutJesper> _aboutJesperRepo;
         private IRepository<User> _userRepo;
         public HomeController(IRepository<Post> postRepo, IRepository<Subscriber> subscriberRepo, IRepository<Customers> customerRepo, IRepository<AboutJesper> aboutJesperRepo, IRepository<User> userRepo)
        {
            _subscriberRepo = subscriberRepo;
            _postRepo = postRepo;
            _customerRepo = customerRepo;
            _aboutJesperRepo = aboutJesperRepo;
            _userRepo = userRepo;
        }

        public ActionResult Index()
        {
            var vm = new vm_PostSubscriber();
                vm.Post = _postRepo.FindAll().Take(3).OrderByDescending(p => p.Date).ToList();
            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(Subscriber subscriber)
        {
            if (ModelState.IsValid)
            {
                var existing = _subscriberRepo.FindAll(s => s.Email == subscriber.Email);             
                    subscriber.ID = Guid.NewGuid();
                    _subscriberRepo.Save(subscriber);
                    ViewBag.SaveMessage = "Prenumeration påbörjad!";
                    var vm = new vm_PostSubscriber();
                    vm.Post = _postRepo.FindAll().Take(3).ToList();
                    return View(vm);               
            }
            else
            {
                ViewBag.SaveMessage = "Subscription unsuccessful";
                return View();
            }

           
        }

        [HttpPost]
        public ActionResult Unsubscribe(Subscriber subscriber)
        {
            if (ModelState.IsValid)
            {
                Subscriber unsubscriber = _subscriberRepo.FindAll(e => e.Email == subscriber.Email && e.Name == subscriber.Name).FirstOrDefault();

                if (unsubscriber == null)
                {
                    TempData["error"] = "Namn eller Email hittades inte";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _subscriberRepo.Delete(unsubscriber);
                    TempData["error"] = "Du är nu inte längre registrerad och kommer inte få nyhetsbrev längre!";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["error"] = "Hoppsan, där var det nåt som gick fel";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Lectures(){
            var lectures = _aboutJesperRepo.FindAll().FirstOrDefault();
            return View(lectures);
        }

        public ActionResult JesperCaron() {
            vm_AboutAndCustomers vm = new vm_AboutAndCustomers();
            vm.Customers = _customerRepo.FindAll().ToList();
            vm.AboutJesper = _aboutJesperRepo.FindAll().FirstOrDefault();
            return View(vm);
        }

        [HttpPost]
        [AuthorizeAdmin]
        [ValidateInput(false)]
        public ActionResult JesperCaron(Customers customer)
        {
            customer.ID = Guid.NewGuid();
            _customerRepo.Save(customer);
            return RedirectToAction("JesperCaron", "Home");
        }

        public ActionResult EditJesperCaron(Guid id)
        {
            var aboutjesper = _aboutJesperRepo.FindByID(id);
            return View(aboutjesper);
        }

        [HttpPost]
        [AuthorizeAdmin]
        [ValidateInput(false)]
        public ActionResult EditJesperCaron(AboutJesper aboutjesper)
        {
            _aboutJesperRepo.Save(aboutjesper);
            return RedirectToAction("JesperCaron", "Home");           
        }
        public ActionResult EditPromise()
        {
            var lectures = _aboutJesperRepo.FindAll().FirstOrDefault();
            return View (lectures);
        }
        [HttpPost]
        [AuthorizeAdmin]
        [ValidateInput(false)]
        public ActionResult EditPromise(AboutJesper aboutjesper)
        {
            _aboutJesperRepo.Save(aboutjesper);
            return RedirectToAction("Lectures", "Home");
        }
        
        public ActionResult EditInformation()
        {
            var lectures = _aboutJesperRepo.FindAll().FirstOrDefault();
            return View(lectures);
        }
        [HttpPost]
        [AuthorizeAdmin]
        [ValidateInput(false)]
        public ActionResult EditInformation(AboutJesper aboutjesper)
        {
            _aboutJesperRepo.Save(aboutjesper);
            return RedirectToAction("Lectures", "Home");
        }
        public ActionResult EditEducations()
        {
            var lectures = _aboutJesperRepo.FindAll().FirstOrDefault();
            return View(lectures);
        }
        [HttpPost]
        [AuthorizeAdmin]
        [ValidateInput(false)]
        public ActionResult EditEducations(AboutJesper aboutjesper)
        {
            _aboutJesperRepo.Save(aboutjesper);
            return RedirectToAction("Lectures", "Home");
        }

        public ActionResult ChangePassword(string username, string newPassword, string email) 
        {
            CustomMembershipProvider cmp = new CustomMembershipProvider();
            cmp.UpdateUser(username, newPassword, email);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult PressKit() {
            string[] filesindirectory = Directory.GetFiles(Server.MapPath("~/Content/Media/PressKit"));
            List<String> images = new List<string>(filesindirectory.Count());
            foreach (string item in filesindirectory)
            {
                images.Add(String.Format("~/Content/Media/PressKit/{0}", System.IO.Path.GetFileName(item)));
            }
            return View(images);
        }
    }
}
