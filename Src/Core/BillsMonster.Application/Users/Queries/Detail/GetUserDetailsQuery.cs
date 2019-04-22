using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using BillsMonster.Application.Interfaces;
using BillsMonster.Application.Exceptions;
using BillsMonster.Domain.Entities;

namespace UsersMonster.Application.Users.Queries.Detail
{
    public class GetUserDetailsQuery : IRequest<UserModel>
    {
        public Guid Id { get; set;}

        public class Handler : IRequestHandler<GetUserDetailsQuery, UserModel>
        {
            private readonly IBillsMonsterDbContext dbContext;

            public Handler(IBillsMonsterDbContext dbContext)
            {
                this.dbContext = dbContext;
            }
            public async Task<UserModel> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
            {
                var entity =  await dbContext.Users.FindAsync(request.Id);
                if(entity == null)
                {
                    throw new NotFoundException(nameof(User), request.Id);
                }

                return (UserModel) entity;
            }
        }
    }
}
