using System;
using System.Collections.Generic;
using System.Text;

namespace BillsMonster.Domain.Entities
{
    public class User: Base
    {        
        public string Email { get; set; }
        public UserProfile Profile { get; set; }        

        public User(): base()
        {            
        }
    }
}
