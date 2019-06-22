using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BillsMonster.Application.Exceptions;
using BillsMonster.Domain.Entities;
using BillsMonster.Application.Interfaces.Data;

namespace UsersMonster.Application.Users.Queries.Detail
{
    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserModel>
    {
        private readonly IUsersRepository usersRepository;

        public GetUserDetailsQueryHandler(IUsersRepository repo)
        {
            this.usersRepository = repo;
        }
        public async Task<UserModel> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await usersRepository.FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            return (UserModel)entity;
        }
    }
}

