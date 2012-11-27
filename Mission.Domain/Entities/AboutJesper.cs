using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities.Abstract;

namespace Mission.Domain.Entities
{
    public class AboutJesper : IEntity
    {
        public Guid ID { get; set; }
        public string Content { get; set; }
    }
}
