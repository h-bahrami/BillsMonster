using BillsMonster.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsMonster.Domain.Entities
{
    public class Base
    {
        public Guid Id { get; set; }

        public DateTime RecordTime { get; set; }

        public Base()
        {
            Id = Guid.NewGuid();
            RecordTime = DateTime.Now;
        }
    }
}
