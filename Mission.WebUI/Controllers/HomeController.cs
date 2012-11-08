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

namespace Mission.WebUI.Controllers
{
    public class HomeController : Controller
    {
         private IRepository<Post> _postRepo;
         private IRepository<Subscriber> _subscriberRepo;
         public HomeController(IRepository<Post> postRepo, IRepository<Subscriber> subscriberRepo)
        {
            _subscriberRepo = subscriberRepo;
            _postRepo = postRepo;
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
        return View();
        }

    }
}
