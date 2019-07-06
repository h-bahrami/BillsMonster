using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BillsMonster.Application.Interfaces.Data;

namespace BillsMonster.Application.Bills.Queries.List
{
    public class GetBillsListQuery : IRequest<List<BillDto>>
    {
        public Guid UserId { get; set; }

        public class Handler : IRequestHandler<GetBillsListQuery, IEnumerable<BillDto>>
        {
            private readonly IBillsRepository dbContext;
            private readonly IMapper mapper;

            public Handler(IBillsRepository dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<IEnumerable<BillDto>> Handle(GetBillsListQuery request, CancellationToken cancellationToken)
            {
                var list = await dbContext.GetBillsByAsync(request.UserId);
                return mapper.Map<IEnumerable<BillDto>>(list);
            }
        }
    }
}
