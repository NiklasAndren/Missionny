﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities;
using Mission.Domain.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Mission.Domain.Entities
{
    public enum Gender { Male, Female }
    
    public class Answere : IEntity
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public int Gender { get; set; }
        public int Age { get; set; }
        public Guid EventQuestionID { get; set; }
        [Range(1, 5)]
        public int Score { get; set; }
        public virtual ICollection <EventQuestion> EventQuestion { get; set; }
    }
}
