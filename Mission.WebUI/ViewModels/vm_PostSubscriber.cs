using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mission.Domain.Entities;

namespace Mission.WebUI.ViewModels
{
    public class vm_PostSubscriber
    {
        public List<Post> Post { get; set; }
        public Subscriber Subscriber { get; set; }
    }
}