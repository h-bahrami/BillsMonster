using BillsMonster.Application.Interfaces;
using MediatR;
using System;

namespace BillsMonster.Application.Groups.Commands.Create
{
    public partial class CreateGroupCommand : IRequest<Guid>
    {
        // public GroupDetailModel Model { get; set; } 
        // public DateTime RecordTime { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
    }
}
