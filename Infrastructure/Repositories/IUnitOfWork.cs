using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testProject.Infrastructure.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void Save();
    }
}