using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Domain.BookCatalog;

namespace ResSys.AdminStatistic.Application.Commands.BookCatalog
{
    /// <summary>
    /// Gets list of all books
    /// </summary>
    public interface IGetAllBooksUseCase
    {
        Task<IEnumerable<Book>> Execute();
    }
}
