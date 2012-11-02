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
            subscriber.ID = Guid.NewGuid();
            _subscriberRepo.Save(subscriber);
            var vm = new vm_PostSubscriber();
                vm.Post = _postRepo.FindAll().Take(3).ToList();
            return View(vm);
        }

    }
}
