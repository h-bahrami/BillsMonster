using BillsMonster.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace BillsMonster.Application.Bills.Queries.List
{
    public class GetBillsListQuery : IRequest<List<BillDto>>
    {
        public Guid UserId { get; set; }

        public class Handler : IRequestHandler<GetBillsListQuery, List<BillDto>>
        {
            private readonly IBillsMonsterDbContext dbContext;
            private readonly IMapper mapper;

            public Handler(IBillsMonsterDbContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<List<BillDto>> Handle(GetBillsListQuery request, CancellationToken cancellationToken)
            {
                var list = await dbContext.Bills.Where(x => x.Group.UserId == request.UserId)
                    .ProjectTo<BillDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

                return list;
            }
        }
    }
}
