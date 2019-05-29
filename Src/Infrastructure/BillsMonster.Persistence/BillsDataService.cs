using BillsMonster.Application.Interfaces.Data;
using BillsMonster.Domain.Entities;
using BillsMonster.Domain.Infrastructure;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BillsMonster.Persistence
{
    public class BillsDataService : DataServiceBase<Bill>, IBillsDataService
    {
        public BillsDataService(DbConfig dbConfig) : base(dbConfig, "bills")
        {
            
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var res = await Collection.DeleteOneAsync(new BsonDocument());
            return res.DeletedCount == 1;
        }

        public async Task<IEnumerable<Bill>> Find()
        {
            var res = await Collection.FindAsync(new BsonDocument());
            res.
        }

        public async Task<IEnumerable<Bill>> GetAllAsync()
        {
            var res = await Collection.FindAsync<Bill>();
        }

        public Task InsertAsync(Bill bill)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Bill bill)
        {
            throw new NotImplementedException();
        }
    }
}
