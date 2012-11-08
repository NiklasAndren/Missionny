using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Mission.Domain.Entities
{
    public class Subscriber : IEntity
    {
        public Guid ID { get; set; }
        [Required(ErrorMessage="fyll i namn")]
        public string Name { get; set; }
        [Required(ErrorMessage="Fyll i email")]
        public string Email { get; set; }
        public String City { get; set; }
    }
}
