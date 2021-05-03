
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Domain.BookCatalog;
using ResSys.Common;

namespace ResSys.AdminStatistic.Application.Commands.BookCatalog
{
    public sealed class UpdateBookAmountUseCase : IUpdateBookAmountUseCase
    {
        private readonly IBookWriteOnlyRepository writeOnlyRepository;
        private readonly IBookReadOnlyRepository readOnlyRepository;

        public UpdateBookAmountUseCase(
            IBookWriteOnlyRepository writeOnlyRepository,
            IBookReadOnlyRepository readOnlyRepository)
        {
            this.writeOnlyRepository = writeOnlyRepository;
            this.readOnlyRepository = readOnlyRepository;
        }

        public async Task Execute(Guid bookId, int amount)
        {
            var book = await readOnlyRepository.GetAsync(bookId);

            if (book == null)
                throw new BookNotFoundException($"The book with Id {bookId} does not exist.");

            book.Amount = amount;
            await writeOnlyRepository.UpdateAsync(book);
        }
    }
}
