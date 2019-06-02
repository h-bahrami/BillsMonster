using BillsMonster.Application.Interfaces;
using BillsMonster.Domain.Entities;
using BillsMonster.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

namespace BillsMonster.Persistence
{
    public class BillsMonsterDbContext : IBillsMonsterDbContext 
    {
        private readonly IMongoDatabase _database;

        public BillsMonsterDbContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
            
        }

        public IMongoCollection<Bill> Bills
        {
            get
            {
                return _database.GetCollection<Bill>("Bills");
            }
        }
        public IMongoCollection<Group> Groups
        {
            get
            {
                return _database.GetCollection<Group>("Groups");
            }
        }
        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("Users");
            }
        }
        public IMongoCollection<Reminder> Reminders
        {
            get
            {
                return _database.GetCollection<Reminder>("Reminders");
            }
        }

        public void Dispose()
        {
            
        }
    }
}
