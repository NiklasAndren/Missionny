using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mission.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Mission.WebUI.ViewModels
{
    public class vm_EventUser
    {
        public Event Event { get; set; }
        [Required(ErrorMessage = "Fyll i företag")]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}