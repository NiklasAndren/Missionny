
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mission.Domain.Repositories.Abstract;
using Mission.Domain.Entities;
using Mission.WebUI.Infrastructure;
using Mission.WebUI.ViewModels;
using System.Web.Script.Serialization;

namespace Mission.WebUI.Controllers
{
    public class EventController : Controller
    {
        private IRepository<Event> _eventRepo;
        private IRepository<EventQuestion> _eventQuestionRepo;
        private IRepository<Answer> _answerRepository;
        public EventController(IRepository<Event> eventRepo, IRepository<EventQuestion> eventQuestion, IRepository<Answer> answerRepository)
        {
            _eventQuestionRepo = eventQuestion;
            _eventRepo = eventRepo;
            _answerRepository = answerRepository;

        }

        public ActionResult Index()
        {
            List<Event> AllEvents = _eventRepo.FindAll().OrderByDescending(p => p.Date).ToList();
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

        [AuthorizeAdmin]
        public ActionResult Edit(Guid id)
        {
            Event events = _eventRepo.FindByID(id);
            return View(events);
        }

        [HttpPost]
        [AuthorizeAdmin]
        public ActionResult Edit(Event events)
        {
            events.Date = DateTime.Now;
            events.Description = HttpUtility.HtmlDecode(events.Description);
            _eventRepo.Save(events);

            return RedirectToAction("Index", "Event");
        }

        [AuthorizeAdmin]
        public ActionResult Delete(Guid id)
        {
            Event events = _eventRepo.FindByID(id);
            _eventRepo.Delete(events);
            return RedirectToAction("Index", "Event");
        }

        [AuthorizeAdmin]
        public ActionResult CreateEventQuestion(Guid id)
        {
            var vm = new vm_EventQuestion();
            vm.Event = _eventRepo.FindByID(id);
            vm.EventQuestions = _eventQuestionRepo.FindAll(p => p.EventID == id).ToList();

            return View(vm);
        }

        [HttpPost]
        [AuthorizeAdmin]
        public ActionResult CreateEventQuestion(EventQuestion eq)
        {
            eq.ID = Guid.NewGuid();
            _eventQuestionRepo.Save(eq);
            return RedirectToAction("Index", "Event");
        }

        [AuthorizeAdmin]
        public ActionResult CreateAnswer(Guid id)
        {
            var vm = new vm_AnswerEventQuestion();
            vm.Answers = _eventQuestionRepo.FindAll(eq => eq.EventID == id).Select(q => new AnswerVM { Question = q.Question, QuestionID = q.ID }).ToList();
            return View(vm);
        }
        [HttpPost]
        [AuthorizeAdmin]
        public ActionResult CreateAnswer(vm_AnswerEventQuestion Answer)
        {
            foreach (var answer in Answer.Answers)
            {
               
                var qAnswer = new Answer
                {
                    Age = Answer.Age,
                    EventQuestionID = answer.QuestionID,                 
                    Username = Answer.Username, 
                    ID = Guid.NewGuid(),
                    Gender = Answer.Gender,
                    Score = Int32.Parse(answer.Selected)
                };
                _answerRepository.Save(qAnswer);
            } 
            return View();
        }

        public JsonResult StatisticsDetails(Guid id)
        {
            var answers = from e in (_eventQuestionRepo.FindAll(e => e.EventID == id).SelectMany(e => e.Answers))
                          group e by new { e.AgeSpan } 
                          into GenderGroup
                              select new AnswerResult
                              {
                                  AgeSpan = GenderGroup.Key.AgeSpan,
                                  mScore = GenderGroup.Where(g => g.Gender == 0).Select(g => g.Score).DefaultIfEmpty(0).Average(),
                                  fScore = GenderGroup.Where(g => g.Gender == 1).Select(g => g.Score).DefaultIfEmpty(0).Average()
                              };

            return Json(answers, JsonRequestBehavior.AllowGet);

        }

        public ActionResult EventStatistics(Guid id)
        {
            return View(id);
        }
        [AuthorizeAdmin]
        public ActionResult Overview() {
            List<Event> AllEvents = _eventRepo.FindAll().ToList();
            return View (AllEvents);
        }

        public ActionResult SingleEvent(Guid id) {
            var Event = _eventRepo.FindByID(id);
            return View(Event);
        }


    }
}
