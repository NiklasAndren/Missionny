﻿
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

        public ActionResult Edit(Guid id)
        {
            Event events = _eventRepo.FindByID(id);
            return View(events);
        }

        [HttpPost]
        public ActionResult Edit(Event events)
        {
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

        public ActionResult CreateEventQuestion(Guid id)
        {
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

        public ActionResult CreateAnswer(Guid id)
        {
            var vm = new vm_AnswerEventQuestion();
            vm.Answers = _eventQuestionRepo.FindAll(eq => eq.EventID == id).Select(q => new AnswerVM { Question = q.Question, QuestionID = q.ID }).ToList();
            return View(vm);
        }
        [HttpPost]
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

        public ActionResult Statistics()
        {
            List<Event> events = _eventRepo.FindAll().ToList();

            return View(events);
        }

        public JsonResult StatisticsDetails(Guid id)
        {
            var answers = _eventQuestionRepo.FindAll(e => e.EventID == id).SelectMany(e => e.Answers).GroupBy(e => e.Age).ToList();

            return Json(answers);

        }

        public ActionResult EventStatistics(Guid id)
        {

            return View(id);

        }
        


    }
}
