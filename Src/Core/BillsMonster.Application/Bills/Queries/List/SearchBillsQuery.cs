using MediatR;
using System;
using System.Collections.Generic;

namespace BillsMonster.Application.Bills.Queries.List
{
    public class SearchBillsQuery : IRequest<IEnumerable<BillDto>>
    {
        public Guid UserId { get; set; }
        public string SearchWord { get; set; }
    }
}
