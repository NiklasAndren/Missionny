using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mission.WebUI.ViewModels
{
    public class ChangePassword
    {
        public string AdminUsername { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public ChangeUserPassword ChangeUserPwd { get; set; }
    }

    public class ChangeUserPassword
    {
        public string OtherUser { get; set; }
        public string NewPassword { get; set; }
    }
}