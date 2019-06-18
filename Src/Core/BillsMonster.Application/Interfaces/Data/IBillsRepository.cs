using BillsMonster.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BillsMonster.Application.Interfaces.Data
{
    public interface IBillsRepository : IRepository<Bill>
    {
        Task<IEnumerable<Bill>> GetBillsByAsync(Guid userId);

        Task<IEnumerable<Bill>> GetBillsByAsync(Guid userId, Guid groupId);

        Task<IEnumerable<Bill>> GetBillsByAsync(Guid userId, string searchWord);

        Task<IEnumerable<Reminder>> GetRemindersAsync(Guid billId);

        Task<bool> AddReminderAsync(Guid billId, Reminder reminder);

        Task<bool> DeleteReminderAsync(Guid billId, Guid reminderId);
    }
}
