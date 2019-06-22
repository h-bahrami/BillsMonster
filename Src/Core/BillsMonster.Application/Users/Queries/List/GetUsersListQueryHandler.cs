using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BillsMonster.Application.Interfaces.Data;
using System.Collections.Generic;
using AutoMapper;

namespace UsersMonster.Application.Users.Queries.Detail
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, IEnumerable<UserModel>>
    {
        private readonly IUsersRepository usersRepository;
        private readonly IMapper mapper;

        public GetUsersListQueryHandler(IUsersRepository repo, IMapper mapper)
        {
            this.usersRepository = repo;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<UserModel>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var users = await usersRepository.GetAllAsync();            
            return mapper.Map<IEnumerable<UserModel>>(users);
        }
    }
}

