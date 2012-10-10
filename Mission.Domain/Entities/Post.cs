using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities.Abstract;

namespace Mission.Domain.Entities
{

    public enum Type { News, Blog }
    public class Post : IEntity
    {
             
        public Guid ID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
        public int Type { get; set; }
        public virtual User User { get; set; }
        public Guid ? UserID { get; set; }
    }
}
