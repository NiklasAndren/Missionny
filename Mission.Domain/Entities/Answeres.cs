using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities;
using Mission.Domain.Entities.Abstract;

namespace Mission.WebUI.Entities
{
    public enum Gender { Male, Female }
    
    public class Answeres : IEntity
    {
        public Guid ID { get; set; }
        public string User { get; set; }
        public int Gender { get; set; }
        public int Age { get; set; }
        public int Score { get; set; }
        public Guid EventQuestionID { get; set; }
        public virtual EventQuestion EventQuestion { get; set; }
    }
}
