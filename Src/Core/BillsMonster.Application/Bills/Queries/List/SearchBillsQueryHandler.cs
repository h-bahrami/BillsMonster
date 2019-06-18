using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BillsMonster.Application.Interfaces.Data;

namespace BillsMonster.Application.Bills.Queries.List
{
    public class SearchBillsQueryHandler : IRequestHandler<SearchBillsQuery, IEnumerable<BillDto>>
    {
        private readonly IBillsRepository billsRepo;
        private readonly IMapper mapper;

        public SearchBillsQueryHandler(IBillsRepository repo, IMapper mapper)
        {
            this.billsRepo = repo;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BillDto>> Handle(SearchBillsQuery request, CancellationToken cancellationToken)
        {
            var list = await billsRepo.GetBillsByAsync(request.UserId, request.SearchWord);
            return mapper.Map<IEnumerable<BillDto>>(list);
        }
    }
}

