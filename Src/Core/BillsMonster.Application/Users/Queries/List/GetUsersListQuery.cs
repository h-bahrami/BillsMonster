using MediatR;
using System;
using System.Collections.Generic;

namespace UsersMonster.Application.Users.Queries.Detail
{
    public class GetUsersListQuery : IRequest<IEnumerable<UserModel>>
    {
        public Guid UserId { get; set; }        
    }
}
