using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mission.Domain.Entities;

namespace Mission.WebUI.ViewModels
{
    public class vm_SendMail
    {
        public Subscriber Subscriber { get; set; }
        public Newsletter Newsletter { get; set; }
    }
}