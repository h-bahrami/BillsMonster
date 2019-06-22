using BillsMonster.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillsMonster.Application.Interfaces.Data
{
    public interface IUsersRepository: IRepository<User>
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<IEnumerable<User>> GetAllAsync(string search);
    }
}
