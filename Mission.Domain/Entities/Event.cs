using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Mission.Domain.Entities
{
    public enum OpenEvent { Closed, Open }
    public class Event : IEntity
    {
        public Guid ID { get; set; }
        public Guid? UserID { get; set; }
        public virtual User User { get; set; }
        [Required(ErrorMessage="Fyll i stad")]
        public String City { get; set; }
        public string Company { get; set; }
        [Required(ErrorMessage = "Fyll i datum")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Fyll i titel")]
        public String HeadLine { get; set; }
        public String Description { get; set; }
        public int OpenEvent { get; set; }
        public virtual ICollection<EventQuestion> EventQuestions { get; set; }
        public List<string> StaticQuestions { get; set {  } }
    }
}
