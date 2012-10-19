using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities.Abstract;
using Mission.WebUI.Entities;

namespace Mission.Domain.Entities
{
    public class EventQuestion : IEntity
    {
        public Guid ID { get; set; }
        public string Question { get; set; }
        public Guid EventID { get; set; }
        public virtual Event Event { get; set;}
        public virtual ICollection<Answeres> Answeres { get; set; }
    }
}
