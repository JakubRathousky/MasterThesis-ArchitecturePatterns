using System;
using ResSys.Common;

namespace ResSys.AdminStatistic.Infrastructure.EntityFrameworkDataAccess.UnitOfWork
{
    /// <summary>
    /// Transactional wrapper
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
        IRepository<T> GetRepository<T>() where T : class, IEntity;
    }
}