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
        public int Gender { get; set; }
        public int Age { get; set; }
        public List<AnswerVM> Answers { get; set; }
    }

    public class AnswerVM
    {
        public string Question { get; set; }
        public Guid QuestionID { get; set; }
        public string Selected { get; set; }
        public IEnumerable<SelectListItem> SelectList = new List<SelectListItem>
        {
            new SelectListItem {Text = "1" , Value = "1"},
            new SelectListItem {Text = "2" , Value = "2"},
            new SelectListItem {Text = "3" , Value = "3"},
            new SelectListItem {Text = "4" , Value = "4"},
            new SelectListItem {Text = "5" , Value = "5"}   
        };
    }
}