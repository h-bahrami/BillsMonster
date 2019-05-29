using System;
using System.Collections.Generic;
using System.Text;

namespace BillsMonster.Domain.Infrastructure
{
    public class DbConfig
    {
        public string ConnectionString { get; set; }
        public string  Database { get; set; }
    }
}
