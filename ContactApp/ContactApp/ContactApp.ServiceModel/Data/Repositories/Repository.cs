using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.ServiceModel.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private IDbConnection db;
        public Repository(IDBContext dbConnection)
        {
            this.db = dbConnection.GetConnectionFactory().OpenDbConnection();
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null) { return await db.CountAsync<T>(filter); }
            return await db.CountAsync<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await db.SelectAsync<T>();
        }


        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> exp)
        {
            return await db.SelectAsync<T>(exp);
        }

        public async Task<List<T>> GetAllAsync(int? offset = 0, int? limit = int.MaxValue, Expression<Func<T, bool>> filter = null, string[] orderby = null, string[] orderbydesc = null)
        {
            var query = BuildGenericQuery(filter).Limit(offset, limit);
            if (orderby != null)
            {
                query = query.OrderByFields(orderby);
            }
            if (orderbydesc != null)
            {
                query = query.OrderByFieldsDescending(orderbydesc);
            }
            return await db.SelectAsync(query);
        }


        public async Task<T> GetByIdAsync(int id)
        {
            var o = await db.SingleByIdAsync<T>(id);
            return o;
        }

        public async Task<bool> InsertAsync(T o)
        {
            return await db.SaveAsync<T>(o);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await db.DeleteByIdAsync<T>(id);
        }

        public async Task<int> DeleteAsync(Expression<Func<T, bool>> exp)
        {
            return await db.DeleteAsync<T>(exp);
        }

        public async Task<int> UpdateAsync(T o)
        {
            return await db.UpdateAsync(o);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
        {
            return await db.ExistsAsync(filter);
        }

        private SqlExpression<T> BuildGenericQuery(Expression<Func<T, bool>> filter)
        {
            var query = db.From<T>();
            return filter != null ? query.Where(filter) : query;
        }
    }
}
