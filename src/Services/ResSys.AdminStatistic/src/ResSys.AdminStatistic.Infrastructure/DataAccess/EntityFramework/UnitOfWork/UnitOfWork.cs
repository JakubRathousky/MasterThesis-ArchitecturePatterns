using System.Collections.Generic;
// using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using ResSys.Common;
using ResSys.AdminStatistic.Infrastructure.EntityFrameworkDataAccess.Repositories;

namespace ResSys.AdminStatistic.Infrastructure.EntityFrameworkDataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly Context _context;

        public UnitOfWork(Context context)
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