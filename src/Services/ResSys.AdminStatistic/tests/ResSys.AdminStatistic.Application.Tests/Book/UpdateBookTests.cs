using Xunit;
using NSubstitute;
using System;
using System.Collections.Generic;

using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Application.Commands.BookCatalog;
using ResSys.AdminStatistic.Domain.BookCatalog;
using ResSys.AdminStatistic.Application;

using System.Linq;

namespace ResSys.AdminStatistic.Application.Tests
{
    public class UpdateBookTests
    {

        private List<Guid> bookGuids = new List<Guid>();
        private List<Guid> authorGuids = new List<Guid>();
        private List<DateTimeOffset> publishDates = new List<DateTimeOffset>();
        private readonly IBookWriteOnlyRepository writeOnlyRepository;
        private readonly IBookReadOnlyRepository readOnlyRepository;

        public UpdateBookTests()
        {
            readOnlyRepository = Substitute.For<IBookReadOnlyRepository>();
            writeOnlyRepository = Substitute.For<IBookWriteOnlyRepository>();
        }

        [Theory]
        [InlineData("A3A12B10-21B3-46C5-92CE-C888CF9856D0", "IBAN1", "Kniha 1", "description 1", 10, "A3A12B10-21B3-46C5-92CE-C888CF9856D0", 1, 5, "2021-04-24")]
        public async void Update_books(string bookId, string IBAN, string name, string description, int numberOfPages, string authorId, int authorRegNum, int amount, string publishDate)
        {
            Book book = new Book()
            {
                Id = Guid.Parse("A3A12B10-21B3-46C5-92CE-C888CF9856D0"),
                IBAN = "IBAN1",
                Name = "Kniha 1",
                Description = "description 1",
                NumberOfPages = 10,
                AuthorId = Guid.Parse("A3A12B10-21B3-46C5-92CE-C888CF9856D0"),
                AuthorRegNum = 1,
                Amount = 5,
                PublishDate = DateTime.Parse("2021-04-24")
            };

            readOnlyRepository
                .GetAsync(book.Id)
                .Returns(book);

            var useCase = new UpdateBookAmountUseCase(
                writeOnlyRepository,
                readOnlyRepository
            );


            await useCase.Execute(book.Id, 15);
        }



        [Theory]
        [InlineData("A3A12B10-21B3-46C5-92CE-C888CF9856D0", "IBAN1", "Kniha 1", "description 1", 10, "A3A12B10-21B3-46C5-92CE-C888CF9856D0", 1, 5, "2021-04-24")]
        public async void Update_books_not_found(string bookId, string IBAN, string name, string description, int numberOfPages, string authorId, int authorRegNum, int amount, string publishDate)
        {
            Book book = new Book()
            {
                Id = Guid.Parse("A3A12B10-21B3-46C5-92CE-C888CF9856D0"),
                IBAN = "IBAN1",
                Name = "Kniha 1",
                Description = "description 1",
                NumberOfPages = 10,
                AuthorId = Guid.Parse("A3A12B10-21B3-46C5-92CE-C888CF9856D0"),
                AuthorRegNum = 1,
                Amount = 5,
                PublishDate = DateTime.Parse("2021-04-24")
            };

            readOnlyRepository
                .GetAsync(book.Id)
                .Returns(book);

            var useCase = new UpdateBookAmountUseCase(
                writeOnlyRepository,
                readOnlyRepository
            );

            await Assert.ThrowsAsync<BookNotFoundException>(() => useCase.Execute(Guid.NewGuid(), 15));
        }
    }
}
