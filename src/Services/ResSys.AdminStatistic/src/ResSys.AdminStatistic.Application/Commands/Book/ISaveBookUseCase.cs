
using System;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Application.Dtos;
using ResSys.AdminStatistic.Application.Results.BookCatalog;

namespace ResSys.AdminStatistic.Application.Commands.BookCatalog
{
    public interface ISaveBookUseCase
    {
        Task<BookResult> Execute(CreateBookDto dto);
    }
}