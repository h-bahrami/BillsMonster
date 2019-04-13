using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using BillsMonster.Application.Interfaces;

namespace UsersMonster.Application.Users.Queries.List
{
    public class SearchUsersQuery : IRequest<List<UserModel>>
    {
        public Guid UserId { get; set; }
        public string SearchWord { get; set; }

        public class Handler : IRequestHandler<SearchUsersQuery, List<UserModel>>
        {
            private readonly IBillsMonsterDbContext dbContext;
            private readonly IMapper mapper;

            public Handler(IBillsMonsterDbContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<List<UserModel>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
            {
                var list = await dbContext.Users.Where(x => x.Id == request.UserId && x.Email.Contains(request.SearchWord))
                    .ProjectTo<UserModel>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

                return list;
            }
        }
    }
}
