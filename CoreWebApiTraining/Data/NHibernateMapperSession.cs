using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiTraining.Data
{
    public class NHibernateMapperSession : IMapperSession
    {
        private readonly ISession _session;

        public ITransaction _transaction { get; set; }

        public NHibernateMapperSession(ISession session)
        {
            this._session = session;
        }

        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public void CloseTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public async Task Delete<T>(T entity)
        {
            await _session.DeleteAsync(entity);
        }

        public async Task<List<T>> GetAll<T>()
        {
            return await _session.Query<T>().ToListAsync();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }

        public async Task Save<T>(T entity)
        {
            await _session.SaveOrUpdateAsync(entity);
        }
    }
}
