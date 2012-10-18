using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities.Abstract;


namespace Mission.Domain.Entities
{
    public class Comment : IEntity
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public Guid BlogID { get; set; }
    }
}
