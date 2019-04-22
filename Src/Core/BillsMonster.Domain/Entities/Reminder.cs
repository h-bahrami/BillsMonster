using System;
using System.Collections.Generic;
using System.Text;

namespace BillsMonster.Domain.Entities
{
    public class Reminder: Base
    {
        public string Title { get; set; }
        public ReminderType Type { get; set; }        
        public DateTime AlarmTime { get; set; }

        public Guid BillId { get; set; }
        public Bill Bill { get; set; }

    }
}
