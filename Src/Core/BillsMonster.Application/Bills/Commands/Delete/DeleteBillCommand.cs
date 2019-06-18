using MediatR;
using System;
using System.Text;

namespace BillsMonster.Application.Bills.Commands.Delete
{
    public class DeleteBillCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
