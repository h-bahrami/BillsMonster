using BillsMonster.Application.Interfaces.Data;
using BillsMonster.Domain.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MongoDB.Driver;
using BillsMonster.Application.Interfaces;

namespace BillsMonster.Persistence
{
    public class BillsRepository : IBillsRepository
    {
        private readonly CancellationToken cancellationToken;
        private readonly IBillsMonsterDbContext context;

        public BillsRepository(IBillsMonsterDbContext dbContext, CancellationToken cancellationToken = default)
        {
            this.cancellationToken = cancellationToken;
            context = dbContext;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var filter = Builders<Bill>.Filter.Eq(x => x.Id, id);
            var res = await context.Bills.DeleteOneAsync(filter, cancellationToken: cancellationToken);
            return res.IsAcknowledged && res.DeletedCount > 0;
        }

        public async Task<IEnumerable<Bill>> GetAllAsync() => await context.Bills.Find(_ => true).ToListAsync();

        public async Task InsertAsync(Bill bill)
        {
            var insertOptions = new InsertOneOptions();
            insertOptions.BypassDocumentValidation = false;
            await context.Bills.InsertOneAsync(bill, insertOptions, cancellationToken);

        }

        public async Task<bool> UpdateAsync(Bill bill)
        {
            var filter = Builders<Bill>.Filter.Eq(x => x.Id, bill.Id);
            var res = await context.Bills.ReplaceOneAsync(filter, bill, new UpdateOptions() { IsUpsert = true }, cancellationToken);
            return res.IsAcknowledged && res.ModifiedCount > 0;

        }

        public async Task<Bill> FindAsync(Guid id)
        {
            var bill = await context.Bills.FindAsync(x => x.InternalId == Bill.GetInternalId(id) || x.Id == id);
            return bill.FirstOrDefault();

        }

        public async Task<IEnumerable<Bill>> GetBillsByAsync(Guid userId)
        {
            var asyncCursor = await context.Bills.FindAsync(x => x.UserId == userId);
            return await asyncCursor.ToListAsync();
        }

        public async Task<IEnumerable<Bill>> GetBillsByAsync(Guid userId, Guid groupId)
        {
            var asyncCursor = await context.Bills.FindAsync(x => x.UserId == userId && x.GroupId == groupId);
            return await asyncCursor.ToListAsync();
        }

        public async Task<IEnumerable<Bill>> GetBillsByAsync(Guid userId, string searchWord)
        {
            var asyncCursor = await context.Bills.FindAsync(x => x.UserId == userId &&
            x.Note.Contains(searchWord) || x.Title.Contains(searchWord));
            return await asyncCursor.ToListAsync();
        }

        public async Task<IEnumerable<Reminder>> GetRemindersAsync(Guid billId)
        {
            var bill = await this.FindAsync(billId);
            return bill.Reminders;
        }

        public async Task<bool> AddReminderAsync(Guid billId, Reminder reminder)
        {
            var bill = await this.FindAsync(billId);
            bill.Reminders.Add(reminder);
            return await this.UpdateAsync(bill);
        }

        public async Task<bool> DeleteReminderAsync(Guid billId, Guid reminderId)
        {
            var bill = await this.FindAsync(billId);
            var succeed = bill.Reminders.Remove(bill.Reminders.SingleOrDefault(x => x.Id == reminderId));
            return succeed ? await this.UpdateAsync(bill) : false;
        }
    }
}
