using AutoMapper;
using AutoMapper.QueryableExtensions;
using BillsMonster.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BillsMonster.Application.Interfaces.Data;

namespace BillsMonster.Application.Groups.Queries.List
{
    public class GetGroupsListQuery : IRequest<IEnumerable<GroupDetailModel>>
    {        
        public class Handler : IRequestHandler<GetGroupsListQuery, IEnumerable<GroupDetailModel>>
        {
            private readonly IGroupsRepository dbContext;
            private readonly IMapper mapper;

            public Handler(IGroupsRepository dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }
            public async Task<IEnumerable<GroupDetailModel>> Handle(GetGroupsListQuery request, CancellationToken cancellationToken)
            {
                var list = await dbContext.GetGroups();
                return mapper.Map<IEnumerable<GroupDetailModel>>(list);
            }
        }
    }
}
