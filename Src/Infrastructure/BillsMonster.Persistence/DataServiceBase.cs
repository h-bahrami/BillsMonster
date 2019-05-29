using BillsMonster.Domain.Infrastructure;
using MongoDB.Driver;

namespace BillsMonster.Persistence
{
    public abstract class DataServiceBase<TObject>
    {
        // protected readonly IMongoClient MongoClient;
        // protected readonly IMongoDatabase Database;
        protected readonly IMongoCollection<TObject> Collection;

        public DataServiceBase(DbConfig dbConfig, string collectionName)
        {
            var mongoClient = new MongoClient(dbConfig.ConnectionString);
            var database = mongoClient.GetDatabase(dbConfig.Database);
            Collection = database.GetCollection<TObject>(collectionName);
        }
    }
}
