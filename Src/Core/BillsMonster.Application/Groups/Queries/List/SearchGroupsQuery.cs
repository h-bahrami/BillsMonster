using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BillsMonster.Application.Interfaces.Data;

namespace BillsMonster.Application.Groups.Queries.List
{
    public class SearchGroupsQuery : IRequest<IEnumerable<GroupDetailModel>>
    {
        public Guid UserId { get; set; }
        public string SearchWord { get; set; }

        public class Handler : IRequestHandler<SearchGroupsQuery, IEnumerable<GroupDetailModel>>
        {
            private readonly IGroupsRepository repo;
            private readonly IMapper mapper;

            public Handler(IGroupsRepository repo, IMapper mapper)
            {
                this.repo = repo;
                this.mapper = mapper;
            }
            public async Task<IEnumerable<GroupDetailModel>> Handle(SearchGroupsQuery request, CancellationToken cancellationToken)
            {
                var list = await repo.GetGroups(request.SearchWord);
                return mapper.Map<IEnumerable<GroupDetailModel>>(list);
            }
        }
    }
}
