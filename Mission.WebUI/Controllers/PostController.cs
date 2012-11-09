
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
using Mission.WebUI.ViewModels;

namespace Mission.WebUI.Controllers
{
    public class PostController : Controller
    {
        private IRepository<Post> _postRepo;
        private IRepository<Comment> _commentRepo;
        private IRepository<SearchResult> _searchRepo;
        public PostController(IRepository<Post> postRepo, IRepository<Comment> commentRepo, IRepository<SearchResult> searchRepo)
        {
            _postRepo = postRepo;
            _commentRepo = commentRepo;
            _searchRepo = searchRepo;
        }

        //
        // GET: /News/


        public ActionResult Index() {
            List<Post> AllPosts = _postRepo.FindAll(p => p.Type == (int)Domain.Entities.Type.News).OrderByDescending(p => p.Date).ToList();       
            return View(AllPosts);
        }

        [AuthorizeAdmin]
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
            post.Body = HttpUtility.HtmlDecode(post.Body);
            _postRepo.Save(post);           

            if (post.Type == 0)
            {
                return RedirectToAction("Index", "Post");
            }
            return RedirectToAction("Blog", "Post");
        }

        public ActionResult Blog()
        {
            BlogComments bc = new BlogComments();
            bc.Posts = _postRepo.FindAll(p => p.Type == (int)Domain.Entities.Type.Blog).OrderByDescending(p => p.Date).ToList();
            bc.BlogComment = _commentRepo.FindAll().OrderByDescending(p => p.Date).ToList();
            return View(bc);
        }

        [AuthorizeAdmin]
        public ActionResult Edit(Guid id) {
            Post post = _postRepo.FindByID(id);
            return View(post);
        }

        [HttpPost]
        [AuthorizeAdmin]
        public ActionResult Edit(Post post) {
            post.Date = DateTime.Now;
            post.Body = HttpUtility.HtmlDecode(post.Body);
            _postRepo.Save(post);
            if (post.Type == 0)
                return RedirectToAction("Index", "Post");
            else
                return RedirectToAction("Blog", "Post");        
        }

        [AuthorizeAdmin]
        public ActionResult Delete(Guid id)
        {
            Post post = _postRepo.FindByID(id);
            _postRepo.Delete(post);
            if (post.Type == 0)
                return RedirectToAction("Index", "Post");
            else
                return RedirectToAction("Blog", "Post");
        }


        [HttpPost]
        public ActionResult _CreateComment(FormCollection commentCollection)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment();
                comment.PostID = new Guid(commentCollection[0]);
                comment.Name = commentCollection[1];
                comment.Body = commentCollection[2];
                comment.ID = Guid.NewGuid();
                comment.Date = DateTime.Now;

                _commentRepo.Save(comment);
                return RedirectToAction("Blog", "Post");
            }
            return PartialView();
        }

        [AuthorizeAdmin]
        public ActionResult DeleteComment(Guid id)
        {
            Comment comment = _commentRepo.FindByID(id);
            _commentRepo.Delete(comment);
            return RedirectToAction("Blog", "Post");
        }

        public ActionResult Search(string search)
        {
            List<SearchResult> searchResult = new List<SearchResult>();

            if (!string.IsNullOrEmpty(search))
            {

                var posts = _postRepo.FindAll().Where(p => p.Title.Contains(search.ToLower()) || p.Body.Contains(search.ToLower())).ToList();
                
                foreach (var post in posts)
                {
                    SearchResult searchTest = new SearchResult();
                    searchTest.Title = post.Title;
                    searchTest.Excerpt = post.Body;
                    searchTest.Url = "/Post/index/" + post.ID;
                    searchResult.Add(searchTest);
                }
            }
            ViewBag.SearchWord = search;
            return View(searchResult);
        }


    }   
}
