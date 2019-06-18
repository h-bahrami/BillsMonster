using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsMonster.Application.Bills.Commands.Update
{
    public partial class BillUpdateCommand : IRequest
    {
        public string Title { get; set; }
        public string Note { get; set; }
        public string ReferenceId { get; set; }
        public float Amount { get; set; }
        public string Image { get; set; }
        public DateTime? ReceivedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? PaidAt { get; set; }
        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }
    }
}
