using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mission.Domain.Entities;

namespace Mission.WebUI.ViewModels
{
    public class vm_EventQuestion
    {
        public Event Event { get; set; }
        public EventQuestion EventQuestion { get; set; }
        public List<EventQuestion> EventQuestions { get; set; }
      
    }
}