
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mission.Domain.Repositories.Abstract;
using Mission.Domain.Entities;
using Mission.WebUI.Infrastructure;

namespace Mission.WebUI.Controllers
{
    public class EventController : Controller
    {
        private IRepository<Event> _eventRepo;
        public EventController(IRepository<Event> eventRepo)
        {
            _eventRepo = eventRepo;
        }

        public ActionResult Index()
        {

            List<Event> AllEvents = _eventRepo .FindAll().OrderByDescending(p => p.Date).ToList();

            return View(AllEvents);
        }
        [AuthorizeAdmin]
        public ActionResult CreateEvent()
        {

            Event NewEvent = new Event();

            return View();
        }

        

        [HttpPost]
        [AuthorizeAdmin]
        public ActionResult CreateEvent(Event newEvent)
        {
            newEvent.ID = Guid.NewGuid();
            HttpUtility.HtmlDecode(newEvent.Description);
            _eventRepo.Save(newEvent);

            return RedirectToAction("Index", "Event");
        }

        public ActionResult Edit(Guid id)
        {
            Event events = _eventRepo.FindByID(id);

            return View(events);
        }

        [HttpPost]
        public ActionResult Edit(Event events) {
            events.Date = DateTime.Now;
            HttpUtility.HtmlDecode(events.Description);
            _eventRepo.Save(events);
           
            return RedirectToAction("Index", "Event");
        }

        public ActionResult Delete(Guid id)
        {
            Event events = _eventRepo.FindByID(id);
            _eventRepo.Delete(events);
            return RedirectToAction("Index", "Event");
        }
    }
}
