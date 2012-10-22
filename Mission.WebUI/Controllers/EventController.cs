
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mission.Domain.Repositories.Abstract;
using Mission.Domain.Entities;
using Mission.WebUI.Infrastructure;
using Mission.WebUI.ViewModels;

namespace Mission.WebUI.Controllers
{
    public class EventController : Controller
    {
        private IRepository<Event> _eventRepo;
        private IRepository<EventQuestion> _eventQuestionRepo;
        public EventController(IRepository<Event> eventRepo, IRepository<EventQuestion> eventQuestion)
        {
            _eventQuestionRepo = eventQuestion;
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
            newEvent.Description = HttpUtility.HtmlDecode(newEvent.Description);
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
            events.Description = HttpUtility.HtmlDecode(events.Description);
            _eventRepo.Save(events);
           
            return RedirectToAction("Index", "Event");
        }

        public ActionResult Delete(Guid id)
        {
            Event events = _eventRepo.FindByID(id);
            _eventRepo.Delete(events);
            return RedirectToAction("Index", "Event");
        }

        public ActionResult CreateEventQuestion(Guid id) {
            var vm = new vm_EventQuestion();
            vm.Event = _eventRepo.FindByID(id);
            vm.EventQuestions = _eventQuestionRepo.FindAll(p => p.EventID == id).ToList();
            
            return View(vm);
        }

        [HttpPost]
        public ActionResult CreateEventQuestion(EventQuestion eq)
        {
            eq.ID = Guid.NewGuid();   
            _eventQuestionRepo.Save(eq);
            return RedirectToAction("Index", "Event");
        }
    }
}
