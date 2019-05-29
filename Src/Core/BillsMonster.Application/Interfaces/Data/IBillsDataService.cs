using BillsMonster.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BillsMonster.Application.Interfaces.Data
{
    public interface IBillsDataService : IDataService<Bill>
    {
        Task<IEnumerable<Bill>> GetAllAsync();
    }
}
