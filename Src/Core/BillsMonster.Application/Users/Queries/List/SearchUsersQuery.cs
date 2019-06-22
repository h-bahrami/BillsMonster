using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using BillsMonster.Application.Interfaces;

namespace UsersMonster.Application.Users.Queries.List
{
    public class SearchUsersQuery : IRequest<IEnumerable<UserModel>>
    {
        public Guid UserId { get; set; }
        public string SearchWord { get; set; }
    }
}
