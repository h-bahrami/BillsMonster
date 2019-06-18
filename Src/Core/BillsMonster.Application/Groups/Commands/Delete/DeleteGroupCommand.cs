using BillsMonster.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;

namespace BillsMonster.Application.Groups.Commands.Delete
{
    public partial class DeleteGroupCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
