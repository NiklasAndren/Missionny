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

    public enum AgeSpan
    {
        e18_26 = 26,
        e27_36 = 36,
        e37_46 = 46,
        e47_56 = 56,
        e57_65 = 65,
        e66 = 66
    }
    
    public class Answer : IEntity
    {

     


        public Guid ID { get; set; }
        public string Username { get; set; }
        public int Gender { get; set; }
        public int Age { get; set; }
        [Range(1, 5)]
        public int Score { get; set; }
        public Guid EventQuestionID { get; set; }
        public virtual EventQuestion EventQuestion { get; set; }
        public AgeSpan AgeSpan
        {
            get
            {
                if (Age <= 26)
                    return AgeSpan.e18_26;
                else if (Age <= 36)
                    return AgeSpan.e27_36;
                else if (Age <= 46)
                    return AgeSpan.e37_46;
                else if (Age <= 56)
                    return AgeSpan.e47_56;
                else if (Age <= 65)
                    return AgeSpan.e57_65;
                else
                    return AgeSpan.e66;
            }
        }




    }
}
