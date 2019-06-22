using AutoMapper.QueryableExtensions;
using BillsMonster.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BillsMonster.Application.Groups.Queries.List
{
    public class GetGroupsListQuery : IRequest<IEnumerable<GroupDetailModel>>
    {        
    }
}
