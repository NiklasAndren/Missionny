using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities.Abstract;

namespace Mission.Domain.Entities
{
    public class Customers : IEntity
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}
