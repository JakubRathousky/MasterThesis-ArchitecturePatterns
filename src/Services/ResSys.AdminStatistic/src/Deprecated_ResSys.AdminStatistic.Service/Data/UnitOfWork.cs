using System.Collections.Generic;
// using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using ResSys.Common;
using ResSys.AdminStatistic.Service.Repository;

namespace ResSys.AdminStatistic.Service.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AdminStatisticContext _context;

        public UnitOfWork(AdminStatisticContext context)
        {
            _context = context;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public IRepository<T> GetRepository<T>() where T : class, IEntity
        {
            return new MSSQLRepository<T>(this._context);
        }
    }
}