using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mission.Domain.Entities;

namespace Mission.WebUI.ViewModels
{
    public class BlogComments
    {
        public List<Post> BlogPost { get; set; }
        public List<Post> BlogComment { get; set; }
    }
}