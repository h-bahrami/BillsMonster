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
    public class SearchBillsQuery : IRequest<List<BillModel>>
    {
        public Guid UserId { get; set; }
        public string SearchWord { get; set; }

        public class Handler : IRequestHandler<SearchBillsQuery, List<BillModel>>
        {
            private readonly IBillsMonsterDbContext dbContext;
            private readonly IMapper mapper;

            public Handler(IBillsMonsterDbContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<List<BillModel>> Handle(SearchBillsQuery request, CancellationToken cancellationToken)
            {
                var list = await dbContext.Bills.Where(x => x.Group.UserId == request.UserId && (
                    x.Title.Contains(request.SearchWord) || x.Note.Contains(request.SearchWord)))
                    .ProjectTo<BillModel>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

                return list;
            }
        }
    }
}
