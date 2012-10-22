using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mission.Domain.Entities;

namespace Mission.WebUI.ViewModels
{
    public class vm_AnswereEventQuestion
    {
        public Answere Answere { get; set; }
        public List<EventQuestion> EventQuestions { get; set; }
    }
}