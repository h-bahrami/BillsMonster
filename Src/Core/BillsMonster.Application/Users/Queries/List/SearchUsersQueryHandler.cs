using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BillsMonster.Application.Interfaces.Data;

namespace UsersMonster.Application.Users.Queries.List
{
    public class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, IEnumerable<UserModel>>
    {
        private readonly IUsersRepository usersRepository;
        private readonly IMapper mapper;

        public SearchUsersQueryHandler(IUsersRepository repo, IMapper mapper)
        {
            this.usersRepository = repo;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserModel>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
        {
            var list = await usersRepository.GetAllAsync(request.SearchWord);
            return mapper.Map<IEnumerable<UserModel>>(list);
        }
    }
}

