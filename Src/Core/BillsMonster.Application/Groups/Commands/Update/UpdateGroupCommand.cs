using BillsMonster.Application.Exceptions;
using BillsMonster.Application.Interfaces;
using MediatR;
using System;

namespace BillsMonster.Application.Groups.Commands.Update
{
    public partial class UpdateGroupCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
    }
}
