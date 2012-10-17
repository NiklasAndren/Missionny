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

namespace Mission.WebUI.Controllers
{
    public class HomeController : Controller
    {
         private IRepository<Post> _postRepo;
         public HomeController(IRepository<Post> postRepo)
        {
            _postRepo = postRepo;
        }

        public ActionResult Index()
        {
            List<Post> post = _postRepo.FindAll().Take(2).ToList();

            return View(post);
        }

    }
}
