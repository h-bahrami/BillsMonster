using BillsMonster.Domain.Entities;
using MongoDB.Driver;
using System;

namespace BillsMonster.Application.Interfaces
{
    public interface IBillsMonsterDbContext: IDisposable
    {
        IMongoCollection<Bill> Bills { get; }

        IMongoCollection<Group> Groups { get; }

        IMongoCollection<User> Users { get; }
    }
}
