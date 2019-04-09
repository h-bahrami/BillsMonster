using System;
using System.Collections.Generic;
using System.Text;

namespace BillsMonster.Domain.Entities
{
    public class Bill: Base
    {
        public string Title { get; set; }
        public string Note { get; set; }
        public string ReferenceId { get; set; }
        public float Amount { get; set; }
        public string Image { get; set; }
        public DateTime? ReceivedAt { get; set;}
        public DateTime? DueDate { get; set; }
        public DateTime? PaidAt { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set;}

        public Bill(): base()
        {
        }
    }
}
