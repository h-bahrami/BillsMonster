using System;
using System.Collections.Generic;
using System.Text;

namespace BillsMonster.Domain.Entities
{
    public class User: Base
    {        
        public string Email { get; set; }
        public ICollection<Group> BillGroups { get; set; }

        public User(): base()
        {
            BillGroups = new HashSet<Group>();
        }
    }
}
