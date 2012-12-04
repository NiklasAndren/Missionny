
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
using PagedList.Mvc;
using PagedList;

namespace Mission.WebUI.Controllers
{
    public class EventController : Controller
    {
        private IRepository<Event> _eventRepo;
        private IRepository<EventQuestion> _eventQuestionRepo;
        private IRepository<Answer> _answerRepository;
        private IRepository<User> _userRepository;
        private IRepository<Subscriber> _subScriberRepository;
        private IRepository<Words> _wordRepository;
        public EventController(IRepository<Event> eventRepo, IRepository<EventQuestion> eventQuestion, IRepository<Answer> answerRepository, IRepository<User> userRepository, IRepository<Subscriber> subScriberRepo, IRepository<Words> wordRepository)
        {
            _eventQuestionRepo = eventQuestion;
            _eventRepo = eventRepo;
            _answerRepository = answerRepository;
            _userRepository = userRepository;
            _subScriberRepository = subScriberRepo;
            _wordRepository = wordRepository;
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
        [ValidateInput(false)]
        public ActionResult CreateEvent(vm_EventUser vm)
        {
            vm.Event.ID = Guid.NewGuid();
            foreach (var eq in Event.InitialQuestion)
            {
                var question = new EventQuestion { ID = Guid.NewGuid(), Question = eq, EventID = vm.Event.ID, Date = vm.Event.Date };
                _eventQuestionRepo.Save(question);
            }  
            vm.Event.Company = vm.Username.ToLower();
            vm.Event.Description = vm.Event.Description;//HttpUtility.HtmlDecode(vm.Event.Description);
            CustomMembershipProvider cmp = new CustomMembershipProvider();
            var status = new MembershipCreateStatus();

            if (!string.IsNullOrEmpty(vm.Password))
            {
                User user = _userRepository.FindAll(u => u.UserName == vm.Username.ToLower()).FirstOrDefault();
                if (user != null)
                {
                    cmp.UpdateUser(vm.Username, vm.Password, vm.Email);
                }
                else {                   
                    cmp.CreateUser(vm.Username.ToLower(), vm.Password, vm.Email, "", "", true, null, out status);
                    User newUser = _userRepository.FindAll(u => u.UserName == vm.Username.ToLower()).FirstOrDefault();
                    newUser.UserEmailAddress = vm.Email;
                    newUser.Event.Add(vm.Event);
                    _userRepository.Save(newUser);
                }
            }
            else
            {
                _eventRepo.Save(vm.Event);
            }
            return RedirectToAction("SingleEvent", "Event", new {vm.Event.ID });
        }

        [AuthorizeAdmin]
        public ActionResult Edit(Guid id)
        {
            Event events = _eventRepo.FindByID(id);
            return View(events);
        }

        [HttpPost]
        [AuthorizeAdmin]
        [ValidateInput(false)]
        public ActionResult Edit(Event events)
        {
            events.Date = events.Date;
            events.Description = HttpUtility.HtmlDecode(events.Description);
            _eventRepo.Save(events);
            return RedirectToAction("SingleEvent", "Event", new { events.ID });
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
            vm.EventQuestionIDs = _eventQuestionRepo.FindAll(eq => eq.EventID == id).OrderBy(e => e.Question).Select(e => e.ID.ToString()).Aggregate((i, j) => i + ";" + j);
            return View(vm);
        }
        [HttpPost]
        [AuthorizeAdmin]
        public ActionResult CreateAnswer(vm_AnswerEventQuestion Answer)
        {
            List<string> words = Answer.Words.Split(',').ToList();
            foreach (var singleword in words)
            {
                var lowerword = singleword.ToLower().Trim();
                var word = _wordRepository.FindAll(w => w.Word == lowerword).FirstOrDefault();
                if (word != null)
                {
                    word.WordCount = word.WordCount + 1;
                    _wordRepository.Save(word);
                }
                else
                {
                    var newword = new Words();
                    newword.WordCount = 1;
                    newword.ID = Guid.NewGuid();
                    newword.Word = lowerword;
                    _wordRepository.Save(newword);
                }
            }
            var eventQuestionIDs = Answer.EventQuestionIDs.Split(';').Select(e => new Guid(e)).ToList();
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
            List<EventQuestion> eq = _eventQuestionRepo.FindAll(e => e.EventID == id).OrderBy(e => e.Question).ToList();
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

        public ActionResult YearsStatistics() 
        {
            List<int> years = new List<int>();
                years = _eventRepo.FindAll().OrderBy(e => e.Date).Select(e => e.Date.Year).Distinct().ToList();
            return View(years);
        }

        public JsonResult StatisticsForYear(int id)
        { 
            if ( id == null) { id = DateTime.Now.Year; }
           List<EventQuestion> eq = _eventQuestionRepo.FindAll(e => e.Date.Year == id).OrderBy(e => e.Question).ToList();
           List<List<AnswerResult>> answers = new List<List<AnswerResult>>();
           var firstQuestionText = Event.InitialQuestion.Where(item => item.StartsWith("1.")).FirstOrDefault();
           int maleCount = eq.FirstOrDefault(q => q.Question == firstQuestionText).Answers == null ? 0 : eq.GroupBy(e => e.Question).FirstOrDefault(q => q.Key == firstQuestionText).SelectMany(e => e.Answers).Where(q => q.Gender == 0).Count();
           int femaleCount = eq.FirstOrDefault(q => q.Question == firstQuestionText).Answers == null ? 0 : eq.GroupBy(e => e.Question).FirstOrDefault(q => q.Key == firstQuestionText).SelectMany(e => e.Answers).Where(q => q.Gender == 1).Count();

           answers.Add((from e in (eq.SelectMany(e => e.Answers))
                        group e by new { e.AgeSpan }
                            into GenderGroup
                            select new AnswerResult
                            {
                                AgeSpan = GenderGroup.Key.AgeSpan,
                                mCount = maleCount,
                                fCount = femaleCount,
                                mScore = GenderGroup.Where(g => g.Gender == 0).Select(g => g.Score).DefaultIfEmpty(0).Average(),
                                fScore = GenderGroup.Where(g => g.Gender == 1).Select(g => g.Score).DefaultIfEmpty(0).Average(),
                            }).OrderBy(a => a.AgeSpan).ToList());

           foreach (var eventquestion in eq.GroupBy(e => e.Question))
           {            
               answers.Add((from e in (eventquestion.SelectMany(e => e.Answers))
                            group e by new {e.AgeSpan }
                                into GenderGroup
                                select new AnswerResult
                                {
                                    Question = eventquestion.Key,
                                    AgeSpan = GenderGroup.Key.AgeSpan,
                                    mScore = GenderGroup.Where(g => g.Gender == 0).Select(g => g.Score).DefaultIfEmpty(0).Average(),
                                    fScore = GenderGroup.Where(g => g.Gender == 1).Select(g => g.Score).DefaultIfEmpty(0).Average(),
                                }).OrderBy(a => a.AgeSpan).ToList());
           }
                
            return Json(answers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EventStatistics(Guid id)
        {
            return View(id);
        }
        [AuthorizeAdmin]
        public ActionResult Overview(int? page)
        {
            var pageNumber = page ?? 1;
            var wc = new WordCountModel();

            wc.EventList = _eventRepo.FindAll().OrderByDescending(e => e.Date).ToList();
            wc.Word = _wordRepository.FindAll().OrderByDescending(w => w.WordCount).ToPagedList(pageNumber, 5);
           
            return View (wc);
        }

        public ActionResult SingleEvent(Guid id) {
            var Event = _eventRepo.FindByID(id);
            return View(Event);
        }

        public ActionResult OpenEvents(int? page) 
        {
            var pagenumber = page ?? 1;
            vm_EventUser eu = new vm_EventUser();
            IPagedList<Event> openEvents = _eventRepo.FindAll(e => e.OpenEvent == (int)OpenEvent.Open).ToPagedList(pagenumber, 5);
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
