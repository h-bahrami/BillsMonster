using MediatR;
using System;
using BillsMonster.Application.Interfaces;
using BillsMonster.Application.Exceptions;

namespace UsersMonster.Application.Users.Commands.Delete
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
