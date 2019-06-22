using MediatR;
using System;
using System.Collections.Generic;

namespace BillsMonster.Application.Groups.Queries.List
{
    public class SearchGroupsQuery : IRequest<IEnumerable<GroupDetailModel>>
    {
        public Guid UserId { get; set; }
        public string SearchWord { get; set; }
    }
}
