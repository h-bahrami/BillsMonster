using BillsMonster.Common;
using System;

namespace BillsMonster.Infrastructure
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
        public int CurrentYear =>  DateTime.UtcNow.Year;
        public int CurrentMonth => DateTime.UtcNow.Month;
        public int CurrentDay => DateTime.UtcNow.Day;
    }
}
