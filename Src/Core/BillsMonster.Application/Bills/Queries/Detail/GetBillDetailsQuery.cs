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
    public class GetBillDetailsQuery : IRequest<BillModel>
    {
        public Guid Id { get; set;}

        public class Handler : IRequestHandler<GetBillDetailsQuery, BillModel>
        {
            private readonly IBillsMonsterDbContext dbContext;

            public Handler(IBillsMonsterDbContext dbContext)
            {
                this.dbContext = dbContext;
            }
            public async Task<BillModel> Handle(GetBillDetailsQuery request, CancellationToken cancellationToken)
            {
                var entity =  await dbContext.Bills.FindAsync(request.Id);
                if(entity == null)
                {
                    throw new NotFoundException(nameof(Bill), request.Id);
                }

                return (BillModel) entity;
            }
        }
    }
}
