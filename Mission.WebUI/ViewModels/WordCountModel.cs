using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mission.Domain.Entities;
using PagedList;
using PagedList.Mvc;
namespace Mission.WebUI.ViewModels
{
    public class WordCountModel 
    {
        public List<Event> EventList { get; set; }
        public IPagedList<Words> Word { get; set; }
    }
}
