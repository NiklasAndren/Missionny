using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities.Abstract;

namespace Mission.Domain.Entities
{
    public enum Role { User, Admin, ContactPerson, Colleague }

    public class User : IEntity
   
    {
        public string UserName { get; set; }
        public int Role { get; set; }
        public Guid ID { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public string UserEmailAddress { get; set; }
        public string City { get; set; }
        public string ? PasswordHash { get; set; }
        public string ? Salt { get; set; }
    }
}
