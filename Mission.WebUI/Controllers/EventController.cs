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
using System.Web.Security;
using System.Data.Entity.Validation;
using System.Text;


namespace Mission.WebUI.Controllers
{
    public class EventController : Controller
    {
        private IRepository<Event> _eventRepo;
        private IRepository<EventQuestion> _eventQuestionRepo;
        private IRepository<Answer> _answerRepository;
        private IRepository<User> _userRepository;
        private IRepository<Subscriber> _subScriberRepository;
        public EventController(IRepository<Event> eventRepo, IRepository<EventQuestion> eventQuestion, IRepository<Answer> answerRepository, IRepository<User> userRepository, IRepository<Subscriber> subScriberRepo)
        {
            _eventQuestionRepo = eventQuestion;
            _eventRepo = eventRepo;
            _answerRepository = answerRepository;
            _userRepository = userRepository;
            _subScriberRepository = subScriberRepo;
        }

        public ActionResult Index()
        {
            List<Event> AllEvents = _eventRepo.FindAll().OrderByDescending(p => p.Date).ToList();
            return View(AllEvents);
        }
        [AuthorizeAdmin]
        public ActionResult CreateEvent()
        {
            vm_EventUser vm = new vm_EventUser();
                vm.Event = new Event();
                      
            return View();
        }

        [HttpPost]
        [AuthorizeAdmin]
        public ActionResult CreateEvent(vm_EventUser vm)
        {
            vm.Event.ID = Guid.NewGuid();
            foreach (var eq in Event.InitialQuestion)
            {
                var question = new EventQuestion { ID = Guid.NewGuid(), Question = eq, EventID = vm.Event.ID, Date = DateTime.Now };
                _eventQuestionRepo.Save(question);
            }  
            vm.Event.Company = vm.Username;
            vm.Event.Description = HttpUtility.HtmlDecode(vm.Event.Description);
            CustomMembershipProvider cmp = new CustomMembershipProvider();
            var status = new MembershipCreateStatus();

            if (!string.IsNullOrEmpty(vm.Password))
            {
                User user = _userRepository.FindAll(u => u.UserName == vm.Username).FirstOrDefault();
                if (user != null)
                {
                    cmp.UpdateUser(vm.Username, vm.Password, vm.Email);
                }
                else {                   
                    cmp.CreateUser(vm.Username, vm.Password, vm.Email, "", "", true, null, out status);
                    User newUser = _userRepository.FindAll(u => u.UserName == vm.Username).FirstOrDefault();
                    newUser.UserEmailAddress = vm.Email;
                    newUser.Event.Add(vm.Event);
                    _userRepository.Save(newUser);
                }
            }
            else
            {
                _eventRepo.Save(vm.Event);
            }
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
            vm.EventQuestions = _eventQuestionRepo.FindAll(p => p.EventID == id).OrderBy(p => p.Date).ToList();
            return View(vm);
        }

        [HttpPost]
        [AuthorizeAdmin]
        public ActionResult CreateEventQuestion(EventQuestion eq)
        {
            eq.ID = Guid.NewGuid();
            eq.Date = DateTime.Now;
            _eventQuestionRepo.Save(eq);
            return RedirectToAction("CreateEventQuestion", "Event");
        }

        [AuthorizeAdmin]
        public ActionResult CreateAnswer(Guid id)
        {
            var vm = new vm_AnswerEventQuestion();
            vm.Questions = _eventQuestionRepo.FindAll(eq => eq.EventID == id).OrderBy(eq => eq.Question).ToList();
            vm.EventQuestionIDs = _eventQuestionRepo.FindAll(eq => eq.EventID == id).Select(e => e.ID.ToString()).Aggregate((i, j) => i + ";" + j);
            return View(vm);
        }
        [HttpPost]
        [AuthorizeAdmin]
        public ActionResult CreateAnswer(vm_AnswerEventQuestion Answer)
        {

            var eventQuestionIDs = Answer.EventQuestionIDs.Split(';').Select(e => new Guid(e)).ToList();
            var scoreArray = Answer.StringAnswers.ToCharArray().Select(c => int.Parse(c.ToString()));

            List<int> ar = new List<int>();
            for(int n =0; n < Answer.StringAnswers.Length; n++)
            {
                ar.Add(int.Parse(Answer.StringAnswers[n].ToString()));
            }            
            int i = 0;
            foreach (var id in eventQuestionIDs)
            {              
                var qAnswer = new Answer
                {
                    Age = Answer.Age,                 
                    Username = Answer.Username, 
                    Email = Answer.Email,
                    ID = Guid.NewGuid(),
                    Gender = Answer.Gender,
                    EventQuestionID = id,
                    Score = ar[i]
                };
                i++;
                try
                    {                       
                    _answerRepository.Save(qAnswer);
                    }
                    catch (DbEntityValidationException ex)
                    {
                    StringBuilder sb = new StringBuilder();

                    foreach (var failure in ex.EntityValidationErrors)
                    {
                        sb.AppendFormat("{0} failed validation\n",
                failure.Entry.Entity.GetType());

                        foreach (var error in failure.ValidationErrors)
                        {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                        }
                    }
                        throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb.ToString(), ex);
                }
            }
            if (!string.IsNullOrEmpty(Answer.Email))
            {
                Subscriber subscriber = _subScriberRepository.FindAll(s => s.Email == Answer.Email).FirstOrDefault();
                if(subscriber == null){
                    subscriber = new Subscriber();
                    subscriber.Email = Answer.Email;
                    subscriber.City = Answer.City;
                    subscriber.Name = Answer.Username;
                    subscriber.ID = Guid.NewGuid();
                    _subScriberRepository.Save(subscriber);
                }
            }
            return RedirectToAction("CreateAnswer", "Event");
        }

        public JsonResult StatisticsDetails(Guid id)
        {
            List<EventQuestion> eq = _eventQuestionRepo.FindAll(e => e.EventID == id).ToList();
            List<List<AnswerResult>> answers = new List<List<AnswerResult>>();
            int maleCount = eq.FirstOrDefault().Answers == null ? 0 : eq.FirstOrDefault().Answers.Where(q => q.Gender == 0).Count();
            int femaleCount = eq.FirstOrDefault().Answers == null ? 0 : eq.FirstOrDefault().Answers.Where(q => q.Gender == 1).Count();

            answers.Add((from e in (eq.SelectMany(e => e.Answers))
                        group e by new { e.AgeSpan }
                            into GenderGroup
                            select new AnswerResult
                                {
                                    AgeSpan = GenderGroup.Key.AgeSpan,
                                    mScore = GenderGroup.Where(g => g.Gender == 0).Select(g => g.Score).DefaultIfEmpty(0).Average(),
                                    fScore = GenderGroup.Where(g => g.Gender == 1).Select(g => g.Score).DefaultIfEmpty(0).Average(),
                                    mCount = maleCount,
                                    fCount = femaleCount
                                }).OrderBy(a => a.AgeSpan).ToList());
           
            foreach (var eventquestion in eq) {
                answers.Add((from e in (_answerRepository.FindAll(e => e.EventQuestionID == eventquestion.ID))
                             group e by new { e.AgeSpan }
                                 into GenderGroup
                                 select new AnswerResult
                                 {
                                     Question = eventquestion.Question,
                                     AgeSpan = GenderGroup.Key.AgeSpan,
                                     mScore = GenderGroup.Where(g => g.Gender == 0).Select(g => g.Score).DefaultIfEmpty(0).Average(),
                                     fScore = GenderGroup.Where(g => g.Gender == 1).Select(g => g.Score).DefaultIfEmpty(0).Average(),
                                     mCount = maleCount,
                                     fCount = femaleCount
                                 }).OrderBy(a => a.AgeSpan).ToList());
            }
            return Json(answers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult YearsStatistics()
        {
            List<Event> allEvents = _eventRepo.FindAll().ToList();
            List<List<AnswerResult>> answers = new List<List<AnswerResult>>();
            foreach(var Event in allEvents ){
                List<EventQuestion> eq = _eventQuestionRepo.FindAll(e => e.EventID == Event.ID).ToList();
                foreach (var eventQ in eq) { 
                  
                }
                
                
            }

            return Json(answers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EventStatistics(Guid id)
        {
            return View(id);
        }
        [AuthorizeAdmin]
        public ActionResult Overview() {
            List<Event> AllEvents = _eventRepo.FindAll().OrderByDescending(e => e.Date).ToList();
            return View (AllEvents);
        }

        public ActionResult SingleEvent(Guid id) {
            var Event = _eventRepo.FindByID(id);
            return View(Event);
        }

        public ActionResult OpenEvents() 
        {
            vm_EventUser eu = new vm_EventUser();
            List<Event> openEvents = _eventRepo.FindAll(e => e.OpenEvent == (int)OpenEvent.Open).ToList();
            return View(openEvents);
        }


        public ActionResult EventForCompany(string name)
        {
            User user = _userRepository.FindAll(u => u.UserName == name.ToLower()).FirstOrDefault();


            List<Event> companyEvents = _eventRepo.FindAll(e => e.UserID == user.ID).ToList();
            return View(companyEvents);
        }
    }


}
