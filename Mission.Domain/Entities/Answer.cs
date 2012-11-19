using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities;
using Mission.Domain.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Mission.Domain.Entities
{
    public enum Gender { Male, Female }   
    public class Answer : IEntity
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public int Gender { get; set; }
        public int Age { get; set; }
        public int Score { get; set; }
        public string Email { get; set; }
        public Guid EventQuestionID { get; set; }
        public virtual EventQuestion EventQuestion { get; set; }
        public string AgeSpan
        {
            get
            {
                if (Age <= 19)
                    return "0-19";
                else if (Age <= 29)
                    return "20-29";
                else if (Age <= 39)
                    return "30-39";
                else if (Age <= 49)
                    return "40-49";
                else if (Age <= 59)
                    return "50-59";
                else
                    return "60+";
            }
        }




    }
}
