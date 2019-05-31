using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.ServiceModel.Data.Repositories
{
    public interface IRepository<T>
    {
        Task<long> CountAsync(Expression<Func<T, bool>> filter = null);
        Task<List<T>> GetAllAsync(int? offset = 0, int? limit = int.MaxValue, Expression<Func<T, bool>> filter = null, string[] orderby = null, string[] orderbydesc = null);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> exp);
        Task<T> GetByIdAsync(int id);
        Task<bool> InsertAsync(T o);
        Task<int> DeleteAsync(int id);
        Task<int> DeleteAsync(Expression<Func<T, bool>> exp);
        Task<int> UpdateAsync(T o);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);
    }
}
