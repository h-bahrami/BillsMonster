using BillsMonster.Application.Exceptions;
using BillsMonster.Application.Interfaces;
using BillsMonster.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Groups.Queries.Detail
{
    public class GetGroupDetailQuery : IRequest<GroupDetailModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetGroupDetailQuery, GroupDetailModel>
        {
            private readonly IBillsMonsterDbContext dbContext;

            public Handler(IBillsMonsterDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<GroupDetailModel> Handle(GetGroupDetailQuery request, CancellationToken cancellationToken)
            {
                var entity = await dbContext.Groups.FindAsync(request.Id);
                if (entity == null)
                {
                    throw new NotFoundException(nameof(Group), request.Id);
                }                
                return (GroupDetailModel)entity;
            }
        }
    }
}
