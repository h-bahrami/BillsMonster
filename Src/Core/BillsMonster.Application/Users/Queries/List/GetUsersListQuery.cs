using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using BillsMonster.Application.Interfaces;

namespace UsersMonster.Application.Users.Queries.Detail
{
    public class GetUsersListQuery : IRequest<IEnumerable<UserModel>>
    {
        public Guid UserId { get; set; }        
    }
}
