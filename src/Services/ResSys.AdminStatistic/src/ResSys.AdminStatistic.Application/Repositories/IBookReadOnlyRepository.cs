using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Domain.BookCatalog;

namespace ResSys.AdminStatistic.Application.Repositories
{
    /// <summary>
    /// Repository for obtaining books
    /// </summary>
    public interface IBookReadOnlyRepository
    {
        Task<IReadOnlyCollection<Book>> GetAllAsync();
        Task<Book> GetAsync(Guid id);
        Task<Book> GetAsync(string IBAN);
        Task<Book> GetAsync(Func<Book, bool> filter);
    }
}
