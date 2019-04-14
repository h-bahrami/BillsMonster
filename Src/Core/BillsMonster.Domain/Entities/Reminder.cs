using System;
using System.Collections.Generic;
using System.Text;

namespace BillsMonster.Domain.Entities
{
    public class Reminder: Base
    {
        public ReminderType Type { get; set; }        
        public DateTime AlarmTime { get; set; }        
        public int RepeatInterval { get; set; }
    }
}
