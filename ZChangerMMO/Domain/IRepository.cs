using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ZChangerMMO.Domain
{
    public interface IUoW : IDisposable
    {
        IEmailRepository Emails { get; }
        IDeviceRepository Devices { get; }
        void Commit();
    }

    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, string include = "");
        TEntity Get(object id);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
