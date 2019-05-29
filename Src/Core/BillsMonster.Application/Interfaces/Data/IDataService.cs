using BillsMonster.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillsMonster.Application.Interfaces.Data
{
    public interface IDataService<TObject>
    {
        Task InsertAsync(TObject bill);
        Task UpdateAsync(TObject bill);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<TObject>> Find();
    }
}
