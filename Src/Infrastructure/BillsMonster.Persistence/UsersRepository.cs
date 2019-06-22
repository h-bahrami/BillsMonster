using BillsMonster.Application.Interfaces;
using BillsMonster.Application.Interfaces.Data;
using BillsMonster.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Persistence
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IBillsMonsterDbContext dbContext;
        private readonly CancellationToken cancellationToken;

        public UsersRepository(IBillsMonsterDbContext dbContext, CancellationToken cancellationToken = default)
        {
            this.dbContext = dbContext;
            this.cancellationToken = cancellationToken;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var res = await dbContext.Users.DeleteOneAsync(
                Builders<User>.Filter.Eq(x => x.Id, id), cancellationToken);
            return res.IsAcknowledged && res.DeletedCount == 1;
        }

        public async Task<User> FindAsync(Guid id) =>
            await (await dbContext.Users.FindAsync(x => x.InternalId == User.GetInternalId(id) && x.Id == id)).FirstOrDefaultAsync();

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await (await dbContext.Users.FindAsync(_ => true)).ToListAsync();

        public async Task<IEnumerable<User>> GetAllAsync(string search) =>
            await (await dbContext.Users.FindAsync(x => x.Profile.Name.Contains(search) || x.Email.Contains(search))).ToListAsync();

        public async Task InsertAsync(User obj) =>
            await dbContext.Users.InsertOneAsync(obj, new InsertOneOptions() { BypassDocumentValidation = false }, cancellationToken);


        public async Task<bool> UpdateAsync(User obj)
        {
            var res = await dbContext.Users.ReplaceOneAsync(
                Builders<User>.Filter.Eq(x => x.Id, obj.Id), obj, new UpdateOptions() { IsUpsert = true }, cancellationToken);
            return res.IsAcknowledged && res.ModifiedCount > 0;
        }
    }
}
