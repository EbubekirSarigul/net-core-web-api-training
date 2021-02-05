using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreWebApiTraining.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ISession session;

        public GenericRepository(ISession session)
        {
            this.session = session;
        }

        public async Task<bool> Add(T entity)
        {
            await session.SaveAsync(entity);
            await session.FlushAsync();
            return true;
        }

        public async Task<bool> Add(IEnumerable<T> items)
        {
            foreach (var entity in items)
            {
                await session.SaveAsync(entity);
            }
            await session.FlushAsync();
            return true;
        }

        public async Task<IEnumerable<T>> All()
        {
            return await session.Query<T>().ToListAsync();
        }

        public async Task<int> Count()
        {
            return await session.QueryOver<T>().RowCountAsync();
        }

        public async Task<bool> Delete(T entity)
        {
            await session.DeleteAsync(entity);
            await session.FlushAsync();
            return true;
        }

        public async Task<bool> Delete(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                await session.DeleteAsync(entity);
            }
            await session.FlushAsync();
            return true;
        }

        public async Task<IEnumerable<T>> findBy(Expression<Func<T, bool>> expression, int startIndex, int count)
        {
            var query = session.Query<T>().Where(expression);

            if (count > 0)
            {
                return await query.Skip(startIndex).Take(count).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<bool> Update(T entity)
        {
            await session.UpdateAsync(entity);
            await session.FlushAsync();

            return true;
        }
    }
}
