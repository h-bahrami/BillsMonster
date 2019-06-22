using BillsMonster.Application.Interfaces;
using BillsMonster.Application.Interfaces.Data;
using BillsMonster.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace BillsMonster.Persistence
{
    public class GroupsReposiroty : IGroupsRepository
    {
        private readonly IBillsMonsterDbContext dbContext;
        private readonly CancellationToken cancellationToken;

        public GroupsReposiroty(IBillsMonsterDbContext dbContext, CancellationToken cancellationToken = default)
        {
            this.dbContext = dbContext;
            this.cancellationToken = cancellationToken;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var filter = Builders<Group>.Filter.Eq(x => x.Id, id);
            var result = await dbContext.Groups.DeleteOneAsync(filter);
            return result.IsAcknowledged && result.DeletedCount == 1;
        }

        public async Task<Group> FindAsync(Guid id)
        {
            var group = await dbContext.Groups.FindAsync(x => x.InternalId == Group.GetInternalId(id) && x.Id == id);
            return await group.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Group>> GetGroups() => await (await dbContext.Groups.FindAsync(_ => true)).ToListAsync();

        public async Task<IEnumerable<Group>> GetGroups(string word)
        {
            IAsyncCursor<Group> asyncCursor = await dbContext.Groups.FindAsync(x => x.Title.Contains(word) && x.Description.Contains(word));
            return await asyncCursor.ToListAsync();
        }

        public async Task InsertAsync(Group obj)
        {
            await dbContext.Groups.InsertOneAsync(obj, new InsertOneOptions(){ BypassDocumentValidation = false }, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Group obj)
        {
            var filter = Builders<Group>.Filter.Eq(x=>x.Id, obj.Id);
            var res = await dbContext.Groups.ReplaceOneAsync(filter, obj, new UpdateOptions(){IsUpsert = true }, cancellationToken);
            return res.IsAcknowledged && res.ModifiedCount > 0;
        }
    }
}
