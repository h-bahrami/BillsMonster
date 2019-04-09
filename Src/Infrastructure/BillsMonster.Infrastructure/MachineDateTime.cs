using BillsMonster.Common;
using System;

namespace BillsMonster.Infrastructure
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now { get { return DateTime.UtcNow; } }

        public int CurrentYear { get { return DateTime.UtcNow.Year; } }

        public int CurrentMonth { get { return DateTime.UtcNow.Month; } }

        public int CurrentDay { get { return DateTime.UtcNow.Day; } }      
    }
}
