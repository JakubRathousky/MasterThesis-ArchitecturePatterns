using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Domain.BookCatalog;

namespace ResSys.AdminStatistic.Application.Repositories
{
    /// <summary>
    /// Repository for modifying books
    /// </summary>
    public interface IBookWriteOnlyRepository
    {
        Task CreateAsync(Book entity);
        Task UpdateAsync(Book entity);
        Task RemoveAsync(Guid id);
    }
}
