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

namespace BillsMonster.Application.Groups.Queries.List
{
    public class SearchGroupsQuery : IRequest<List<GroupDetailModel>>
    {
        public Guid UserId { get; set; }
        public string SearchWord { get; set; }

        public class Handler : IRequestHandler<SearchGroupsQuery, List<GroupDetailModel>>
        {
            private readonly IBillsMonsterDbContext dbContext;
            private readonly IMapper mapper;

            public Handler(IBillsMonsterDbContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }
            public async Task<List<GroupDetailModel>> Handle(SearchGroupsQuery request, CancellationToken cancellationToken)
            {
                var list = await dbContext.Groups.Where(x => x.UserId == request.UserId &&
                (x.Title.Contains(request.SearchWord) || x.Description.Contains(request.SearchWord)))
                    .ProjectTo<GroupDetailModel>(mapper.ConfigurationProvider).ToListAsync(cancellationToken);
                return list;
            }
        }
    }
}
