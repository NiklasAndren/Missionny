using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mission.Domain.Entities;
using Mission.Domain.Repositories.Abstract;

namespace Mission.WebUI.Controllers
{
    public class DefaultController : Controller

    {
        //
        // GET: /Default/
        private IRepository<User> _userrepo;       
        public DefaultController(IRepository<User> userrepo) 
        {
            _userrepo = userrepo;
        }

        public ActionResult Index()
        {
            List<User> users = _userrepo.FindAll().ToList();


            return View(users);
        }

    }
}
