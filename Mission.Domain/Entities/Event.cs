using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities.Abstract;

namespace Mission.Domain.Entities
{
    public class Event : IEntity
    {
        public Guid ID { get; set; }
        public String City { get; set; }
        public DateTime Date { get; set; }
        public String HeadLine { get; set; }
        public String Info { get; set; }
    }
}
