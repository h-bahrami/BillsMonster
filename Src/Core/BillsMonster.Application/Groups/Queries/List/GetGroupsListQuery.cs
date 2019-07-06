using MediatR;
using System.Collections.Generic;

namespace BillsMonster.Application.Groups.Queries.List
{
    public class GetGroupsListQuery : IRequest<IEnumerable<GroupDetailModel>>
    {        
    }
}
