using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreWebApiTraining.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> Add(T entity);
        Task<bool> Add(IEnumerable<T> items);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Delete(IEnumerable<T> entities);
        Task<IEnumerable<T>> All();
        Task<IEnumerable<T>> findBy(Expression<Func<T, bool>> expression, int startIndex, int count);
        Task<int> Count();
    }
}
