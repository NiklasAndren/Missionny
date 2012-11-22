using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mission.Domain.Entities;

namespace Mission.WebUI.ViewModels
{
    public class BlogViewModel
    {
        public BlogComments Blogcomments { get; set; }
        public List<ArkivModel> Arkivmodel { get; set; }
        public BlogViewModel()
        {
            Blogcomments = new BlogComments();
        }
    }

    public class BlogComments
    {
        public List<Post> Posts { get; set; }
        public List<Comment> BlogComment { get; set; }
    }

    public class ArkivModel
    {
        public int Year { get; set; }
        public string Month { get; set; }
        public int Count { get; set; }
    }


}