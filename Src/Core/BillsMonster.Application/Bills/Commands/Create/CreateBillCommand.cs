using BillsMonster.Application.Interfaces;
using MediatR;
using System;

namespace BillsMonster.Application.Bills.Commands.Create
{
    public partial class CreateBillCommand : IRequest
    {
        public Guid Id { get; set; }
        public DateTime RecordTime { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public string ReferenceId { get; set; }
        public float Amount { get; set; }
        public string Image { get; set; }
        public DateTime? ReceivedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? PaidAt { get; set; }
        public Guid GroupId { get; set; }
    }
}
