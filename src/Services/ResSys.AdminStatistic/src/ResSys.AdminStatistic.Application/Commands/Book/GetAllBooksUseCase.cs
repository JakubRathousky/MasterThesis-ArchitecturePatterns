using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Domain.BookCatalog;
using ResSys.Common;

namespace ResSys.AdminStatistic.Application.Commands.BookCatalog
{
    public sealed class GetAllBooksUseCase : IGetAllBooksUseCase
    {
        private readonly IBookReadOnlyRepository bookRepository;

        public GetAllBooksUseCase(
            IBookReadOnlyRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> Execute()
        {
            var books = await bookRepository.GetAllAsync();
            if (books == null)
                return new List<Book>();

            return books;
        }
    }
}
