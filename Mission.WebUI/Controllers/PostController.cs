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
    public class PostController : Controller
    {
        private IRepository<Post> _postRepo;
        public PostController(IRepository<Post> postRepo)
        {
            _postRepo = postRepo;
        }

        //
        // GET: /News/


        public ActionResult Index() {

            List<Post> AllPosts = _postRepo.FindAll(p => p.Type == (int)Domain.Entities.Type.News).Include(p => p.User).OrderByDescending(p => p.Date).ToList();
    
            return View(AllPosts);
        }

        public ActionResult CreatePost(){

            var post = new Post();

            return View(post);
        }

        [HttpPost]
        public ActionResult CreatePost(Post post)
        {

            post.ID = Guid.NewGuid();
            post.Date = DateTime.Now;
            User dude = new User { UserName = "Jesper Caron", Role = (int)Role.Admin, ID = Guid.NewGuid() };
            post.User = dude;

            _postRepo.Save(post);
            
           

            return RedirectToAction("Index", "Post");
        }


    }
}
