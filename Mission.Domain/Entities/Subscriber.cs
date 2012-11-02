using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities.Abstract;

namespace Mission.Domain.Entities
{
    public class Subscriber : IEntity
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public String City { get; set; }
    }
}
