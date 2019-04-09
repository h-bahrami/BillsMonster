using System;
using System.Collections.Generic;

namespace BillsMonster.Domain.Entities
{
    public class Group: Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set;}
        public Group? Parent { get; set; }
        public ICollection<Group> Children { get; set;}
        public ICollection<Bill> Bills { get; set; }

        public Group(): base()
        {
            Bills = new HashSet<Bill>();
            Children = new HashSet<Group>();
        }
    }
}
