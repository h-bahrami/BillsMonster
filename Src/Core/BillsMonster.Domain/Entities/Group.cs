using System;
using System.Collections.Generic;

namespace BillsMonster.Domain.Entities
{
    public class Group: Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set;}        
        public Guid UserId { get; set; }

        public Group(): base()
        {          
        }
    }
}
