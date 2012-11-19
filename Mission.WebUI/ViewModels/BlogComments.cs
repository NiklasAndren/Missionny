using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mission.Domain.Entities;

namespace Mission.WebUI.ViewModels
{
    public class BlogComments
    {
        public List<Post> Posts { get; set; }
        public List<Comment> BlogComment { get; set; }
        public Dictionary<int,int> ArkivCount { get; set; }
    }

    public class ArkivModel
    {
        public int Year { get; set; }
        public string Month { get; set; }
        public int Count { get; set; }
    }


}