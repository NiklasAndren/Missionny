using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities.Abstract;

namespace Mission.Domain.Entities
{
    public class SearchResult : IEntity
        {
        public Guid ID { get; set; }
        public string Title  { get; set; }
        public string Url { get; set; }
        public string Excerpt { get; set; }
        }
}
