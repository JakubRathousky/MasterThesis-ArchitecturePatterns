using System;
using ResSys.Common;
using ResSys.AdminStatistic.Service.Repository;

namespace ResSys.AdminStatistic.Service.Data
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
        IRepository<T> GetRepository<T>() where T : class, IEntity;
    }
}