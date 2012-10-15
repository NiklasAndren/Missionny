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
using Mission.WebUI.Infrastructure;

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
        [AuthorizeAdmin]
        public ActionResult CreatePost(Post post)
        {

            post.ID = Guid.NewGuid();
            post.Date = DateTime.Now;
            _postRepo.Save(post);
            

            if (post.Type == 0)
            {
                return RedirectToAction("Index", "Post");
            }

            return RedirectToAction("Blog", "Post");
        }

        public ActionResult Blog()
        {

            List<Post> AllPosts = _postRepo.FindAll(p => p.Type == (int)Domain.Entities.Type.Blog).Include(p => p.User).OrderByDescending(p => p.Date).ToList();

            return View(AllPosts);
        }

        
        public ActionResult Edit(Guid id) {
            Post post = _postRepo.FindByID(id);

            return View(post);
        }

        [HttpPost]
        public ActionResult Edit(Post post) {
            post.Date = DateTime.Now;
            _postRepo.Save(post);
            if (post.Type == 0)
                return RedirectToAction("Index", "Post");
            else
                return RedirectToAction("Blog", "Post");
        
        }

        public ActionResult Delete(Guid id)
        {
            Post post = _postRepo.FindByID(id);
            _postRepo.Delete(post);

            if (post.Type == 0)
                return RedirectToAction("Index", "Post");
            else
                return RedirectToAction("Blog", "Post");
        }


    }
}
