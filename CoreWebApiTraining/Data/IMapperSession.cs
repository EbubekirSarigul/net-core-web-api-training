using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiTraining.Data
{
    public interface IMapperSession
    {
        void BeginTransaction();
        Task Commit();
        Task Rollback();
        void CloseTransaction();
        Task Save<T>(T entity);
        Task Delete<T>(T entity);
        Task<List<T>> GetAll<T>();
    }
}
