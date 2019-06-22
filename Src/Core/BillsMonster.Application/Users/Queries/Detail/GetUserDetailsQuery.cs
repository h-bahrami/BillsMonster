using MediatR;
using System;
using BillsMonster.Application.Interfaces;

namespace UsersMonster.Application.Users.Queries.Detail
{
    public class GetUserDetailsQuery : IRequest<UserModel>
    {
        public Guid Id { get; set;}
    }
}
