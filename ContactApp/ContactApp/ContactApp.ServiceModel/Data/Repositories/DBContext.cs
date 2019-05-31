using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.ServiceModel.Data.Repositories
{
    public interface IDBContext
    {
        OrmLiteConnectionFactory GetConnectionFactory();
    }

    public class DBContext : IDBContext
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        public DBContext(string connection)
        {
            this.dbFactory = new OrmLiteConnectionFactory(connection, SqlServerDialect.Provider);
        }

        public OrmLiteConnectionFactory GetConnectionFactory()
        {
            return dbFactory;
        }
    }
}
