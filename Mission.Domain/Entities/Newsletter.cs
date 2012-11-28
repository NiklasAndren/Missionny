using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities.Abstract;

namespace Mission.Domain.Entities
{
    public class Newsletter : IEntity
    {
        public Guid ID { get; set; }
        public String Subject { get; set; }
        public String Message { get; set; }
        public DateTime Date { get; set; }
    }
}
