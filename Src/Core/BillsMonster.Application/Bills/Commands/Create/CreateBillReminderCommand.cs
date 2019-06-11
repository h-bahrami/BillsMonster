using BillsMonster.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsMonster.Application.Bills.Commands.Create
{
    public partial class CreateBillReminderCommand : IRequest
    {
        public Guid BillId { get; set; }
        public string Title { get; set; }
        public ReminderType Type { get; set; }
        public DateTime AlarmTime { get; set; }
        public string SomeText { get; internal set; }
    }
}
