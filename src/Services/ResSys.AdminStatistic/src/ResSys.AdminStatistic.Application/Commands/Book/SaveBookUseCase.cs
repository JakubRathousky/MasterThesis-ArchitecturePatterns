
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Domain.BookCatalog;
using ResSys.AdminStatistic.Application.Results.BookCatalog;
using ResSys.AdminStatistic.Application.Dtos;
using ResSys.Common;

namespace ResSys.AdminStatistic.Application.Commands.BookCatalog
{
    public sealed class SaveBookUseCase : ISaveBookUseCase
    {
        private readonly IBookWriteOnlyRepository writeOnlyRepository;
        private readonly IBookReadOnlyRepository readOnlyRepository;

        public SaveBookUseCase(
            IBookWriteOnlyRepository writeOnlyRepository,
            IBookReadOnlyRepository readOnlyRepository)
        {
            this.writeOnlyRepository = writeOnlyRepository;
            this.readOnlyRepository = readOnlyRepository;
        }

        public async Task<BookResult> Execute(CreateBookDto dto)
        {
            var book = await readOnlyRepository.GetAsync(dto.IBAN);

            if (book != null)
                return null;

            book = new Book()
            {
                Id = dto.BookId,
                IBAN = dto.IBAN,
                Name = dto.Name,
                Description = dto.Description,
                NumberOfPages = dto.NumberOfPages,
                AuthorId = dto.AuthorId,
                AuthorRegNum = dto.AuthorRegNum,
                Amount = dto.Amount,
                PublishDate = dto.PublishDate.DateTime
            };

            await writeOnlyRepository.CreateAsync(book);

            return new BookResult(book);
        }
    }
}