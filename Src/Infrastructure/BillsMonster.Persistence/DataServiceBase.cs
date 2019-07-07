using BillsMonster.Domain.Infrastructure;
using MongoDB.Driver;
using System;

namespace BillsMonster.Persistence
{
    [Obsolete("Use IBillsMonsterDbContext instead", true)]
    public abstract class DataServiceBase<TObject>
    {
        // protected readonly IMongoClient MongoClient;
        // protected readonly IMongoDatabase Database;
        protected readonly IMongoCollection<TObject> Collection;

        public DataServiceBase(MongodbConnection settings, string collectionName)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);
            var database = mongoClient.GetDatabase(settings.Database);
            Collection = database.GetCollection<TObject>(collectionName);
        }
    }
}
