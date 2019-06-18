using BillsMonster.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillsMonster.Application.Interfaces.Data
{
    public interface IGroupsRepository : IRepository<Group>
    {
        Task<IEnumerable<Group>> GetGroups();
        

    }
}
