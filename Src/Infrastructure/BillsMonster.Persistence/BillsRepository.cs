using BillsMonster.Application.Interfaces.Data;
using BillsMonster.Domain.Entities;
using BillsMonster.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using BillsMonster.Application.Interfaces;

namespace BillsMonster.Persistence
{
    public class BillsRepository : IBillsRepository
    {
        
        private readonly CancellationToken cancellationToken;
        private readonly IBillsMonsterDbContext context;

        public BillsRepository(IOptions<Settings> settings, CancellationToken cancellationToken = default)
        {
            
            this.cancellationToken = cancellationToken;
            context = new BillsMonsterDbContext(settings);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var filter = Builders<Bill>.Filter.Eq(x => x.Id, Guid.NewGuid());
                var res = await context.Bills.DeleteOneAsync(filter, cancellationToken: cancellationToken);
                return res.IsAcknowledged && res.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<Bill>> GetAllAsync()
        {
            try
            {
                return await context.Bills.Find(_ => true).ToListAsync();
                // var filter = FilterDefinition<Bill>.Empty;
                // var res = await _context.Bills.FindAsync<Bill>(filter, cancellationToken: cancellationToken);
                // return await res.ToListAsync();
            }
            catch (Exception ex)
            {
                // logger.LogError(ex,);
                throw ex;
            }
        }

        public async Task InsertAsync(Bill bill)
        {
            try
            {
                var insertOptions = new InsertOneOptions();
                insertOptions.BypassDocumentValidation = false;
                await context.Bills.InsertOneAsync(bill, insertOptions, cancellationToken);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> UpdateAsync(Bill bill)
        {
            try
            {
                var filter = Builders<Bill>.Filter.Eq(x=>x.Id, bill.Id);
                //var update = Builders<Bill>.Update
                //    .Set(x=>x.GroupId, bill.GroupId)
                //    .Set(x => x.GroupId, bill.GroupId)
                //    .Set(x => x.Image, bill.Image)
                //    .Set(x => x.Note, bill.Note)
                //    .Set(x => x.PaidAt, bill.PaidAt)
                //    .Set(x => x.ReceivedAt, bill.ReceivedAt)
                //    .Set(x => x.RecordTime, bill.RecordTime)
                //    .Set(x => x.ReferenceId, bill.ReferenceId)
                //    .Set(x => x.Reminders, bill.Reminders)
                //    .Set(x =>x.Title, bill.Title);
                //var res = await context.Bills.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);              

                var res = await context.Bills.ReplaceOneAsync(filter, bill, new UpdateOptions() { IsUpsert = true}, cancellationToken);
                return res.IsAcknowledged && res.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Bill> FindAsync(Guid id)
        {
            // var filter = Builders<Bill>.Filter.Eq(x => x.Id, Guid.NewGuid());// FilterDefinition<Bill>.Empty;            
            // var res = await Collection.FindAsync(filter, cancellationToken: cancellationToken);
            // return await res.FirstOrDefaultAsync(cancellationToken);
            try
            {
                var bill = await context.Bills.Find(x => x.InternalId == Bill.GetInternalId(id) || x.Id == id).FirstOrDefaultAsync();
                return bill;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
