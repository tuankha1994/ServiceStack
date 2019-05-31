using ContactApp.ServiceModel.Data.Models;
using ServiceStack.OrmLite;
using System.Data;
using System.Reflection;

namespace ContactApp.ServiceModel.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed;
        private IDbConnection dbConnection;
        private IDbTransaction dbTransaction;

        public UnitOfWork(IDBContext dbContext)
        {
            var dbFactory = dbContext.GetConnectionFactory();
            dbConnection = dbFactory.OpenDbConnection();
            dbTransaction = dbConnection.OpenTransaction();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                dbConnection.Dispose();
                dbTransaction.Dispose();
                dbConnection = null;
            }
        }

        public bool Commit()
        {
            try
            {
                dbTransaction.Commit();
                return true;
            }
            catch
            {
                dbTransaction.Rollback();
                return false;
            }
        }
    }
}
