using BillsMonster.Application.Interfaces;
using BillsMonster.Domain.Entities;
using BillsMonster.Domain.Infrastructure;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BillsMonster.Persistence
{
    public class BillsMonsterDbContext : IBillsMonsterDbContext 
    {
        private readonly IMongoDatabase _database;

        public BillsMonsterDbContext(IOptions<MongodbConnection> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);            
        }

        public IMongoCollection<Bill> Bills
        {
            get
            {
                return _database.GetCollection<Bill>("bills");
            }
        }
        public IMongoCollection<Group> Groups
        {
            get
            {
                return _database.GetCollection<Group>("groups");
            }
        }
        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("users");
            }
        }
        
        public void Dispose()
        {
            
        }
    }
}
