using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Domain.BookCatalog;

namespace ResSys.AdminStatistic.Application.Commands.BookCatalog
{
    /// <summary>
    /// Updates amount of books in stock
    /// </summary>
    public interface IUpdateBookAmountUseCase
    {
        Task Execute(Guid bookId, int amount);
    }
}
