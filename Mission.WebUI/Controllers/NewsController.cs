using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mission.Domain.Repositories.Abstract;
using Mission.Domain.Entities;
using Mission.Domain.Repositories;
using Mission.Domain.Entities.FakeData;
using System.Data.Entity;

namespace Mission.WebUI.Controllers
{
    public class NewsController : Controller
    {
        private IRepository<Post> _postRepo;
        public NewsController(IRepository<Post> postRepo)
        {
            _postRepo = postRepo;
        }

        //
        // GET: /News/


        public ActionResult Index() {

            List<Post> AllPosts = _postRepo.FindAll(p => p.Type == (int)Domain.Entities.Type.News).Include(p => p.User).ToList();
    
            return View(AllPosts);
        }

        public ActionResult test(){
           
            return View();
        }


    }
}
