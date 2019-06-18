using MediatR;
using System;

namespace BillsMonster.Application.Bills.Commands.Delete
{
    public class DeleteBillReminderCommand: IRequest
    {
        public Guid BillId { get; set; }
        public Guid ReminderId { get; set; }
    }
}
