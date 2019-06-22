using MediatR;
using System;

namespace BillsMonster.Application.Groups.Queries.Detail
{
    public class GetGroupDetailQuery : IRequest<GroupDetailModel>
    {
        public Guid Id { get; set; }
    }
}
