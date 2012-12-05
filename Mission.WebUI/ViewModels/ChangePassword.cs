using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mission.WebUI.ViewModels
{
    public class ChangePassword
    {
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}