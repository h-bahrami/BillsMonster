using System;
using System.Collections.Generic;
using System.Text;

namespace BillsMonster.Domain.Infrastructure
{
    public interface IMongodbConnection
    {
        string ConnectionString { get; set; }
        string Database { get; set; }
    }

    public class MongodbConnection : IMongodbConnection
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }


}
