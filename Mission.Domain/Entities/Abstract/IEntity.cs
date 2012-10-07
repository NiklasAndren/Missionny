using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mission.Domain.Entities.Abstract
{
    public interface IEntity
    {
        Guid ID { get; set; }
    }
}

