using System;
using System.Collections.Generic;
using System.Text;

namespace BillsMonster.Domain.Entities
{
    public class Reminder: Base
    {
        public string Text { get; set; }
        public ReminderType Type { get; set; }        
        public DateTime AlarmTime { get; set; }
    }
}
