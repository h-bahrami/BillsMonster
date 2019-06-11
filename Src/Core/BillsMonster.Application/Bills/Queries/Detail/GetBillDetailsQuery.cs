using BillsMonster.Application.Exceptions;
using BillsMonster.Application.Interfaces;
using BillsMonster.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Bills.Queries.Detail
{
    public class GetBillDetailsQuery : IRequest<BillDto>
    {
        public Guid Id { get; set;}

        public class Handler : IRequestHandler<GetBillDetailsQuery, BillDto>
        {
            private readonly IBillsMonsterDbContext dbContext;

            public Handler(IBillsMonsterDbContext dbContext)
            {
                this.dbContext = dbContext;
            }
            public async Task<BillDto> Handle(GetBillDetailsQuery request, CancellationToken cancellationToken)
            {
                var entity =  await dbContext.Bills.FindAsync(request.Id);
                if(entity == null)
                {
                    throw new NotFoundException(nameof(Bill), request.Id);
                }

                return (BillDto) entity;
            }
        }
    }
}
