using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mission.Domain.Entities;

namespace Mission.WebUI.ViewModels
{
    public class vm_AboutAndCustomers
    {
        public List<Customers> Customers { get; set; }
        public AboutJesper AboutJesper { get; set; }
    }
}