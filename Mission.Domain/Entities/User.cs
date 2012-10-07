using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities.Abstract;

namespace Mission.Domain.Entities
{
    public enum Role { User, Admin }

    public class User : IEntity
   
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public Guid ID { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
