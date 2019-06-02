using BillsMonster.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillsMonster.Application.Interfaces.Data
{
    public interface IRepository<TObject>
    {
        Task InsertAsync(TObject bill);

        Task<TObject> FindAsync(Guid id);

        Task<bool> UpdateAsync(TObject bill);

        Task<bool> DeleteAsync(Guid id);

    }
}
