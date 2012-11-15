using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mission.Domain.Entities;
using System.Web.Mvc;

namespace Mission.WebUI.ViewModels
{
    public class vm_AnswerEventQuestion
    {   
        public string Username { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public string City { get; set; }
        public int Age { get; set; }
        public string StringAnswers { get; set; }
        public List<EventQuestion> Questions { get; set; }
        public string EventQuestionIDs { get; set; }

    }

    //public class AnswerVM
    //{
    //    public string Question { get; set; }
    //    public Guid QuestionID { get; set; }
    //}

    public class AnswerResult
    {
        public string AgeSpan { get; set; }
        public string Question { get; set; }
        public double mScore { get; set; }
        public double fScore { get; set; }
        public int mCount { get; set; }
        public int fCount { get; set; }
    }
}