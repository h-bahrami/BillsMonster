using MediatR;
using System;
using System.Collections.Generic;

namespace UsersMonster.Application.Users.Queries.List
{
    public class SearchUsersQuery : IRequest<IEnumerable<UserModel>>
    {
        public Guid UserId { get; set; }
        public string SearchWord { get; set; }
    }
}
