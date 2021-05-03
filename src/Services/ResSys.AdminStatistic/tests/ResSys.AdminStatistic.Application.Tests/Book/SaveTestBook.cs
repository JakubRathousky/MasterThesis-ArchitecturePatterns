using Xunit;
using NSubstitute;
using System;
using System.Collections.Generic;

using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Application.Commands.BookCatalog;

namespace ResSys.AdminStatistic.Application.Tests
{
    public class SaveBookTests
    {

        private List<Guid> bookGuids = new List<Guid>();
        private List<Guid> authorGuids = new List<Guid>();
        private List<DateTimeOffset> publishDates = new List<DateTimeOffset>();
        private readonly IBookWriteOnlyRepository writeOnlyRepository;
        private readonly IBookReadOnlyRepository readOnlyRepository;

        public SaveBookTests()
        {
            readOnlyRepository = Substitute.For<IBookReadOnlyRepository>();
            writeOnlyRepository = Substitute.For<IBookWriteOnlyRepository>();
        }

        [Theory]
        [InlineData("A3A12B10-21B3-46C5-92CE-C888CF9856D0", "IBAN1", "Kniha 1", "description 1", 10, "A3A12B10-21B3-46C5-92CE-C888CF9856D0", 1, 5, "2021-04-24")]
        public async void Save_books(string bookId, string IBAN, string name, string description, int numberOfPages, string authorId, int authorRegNum, int amount, string publishDate)
        {
            var datetime = DateTimeOffset.Parse(publishDate);
            var bookIdGuid = Guid.Parse(bookId);
            var authorIdGuid = Guid.Parse(authorId);

            var useCase = new SaveBookUseCase(
                writeOnlyRepository,
                readOnlyRepository
            );


            var book = await useCase.Execute(bookIdGuid, IBAN, name, description, numberOfPages, authorIdGuid, authorRegNum, amount, datetime);


            Assert.Equal(book.Id, bookIdGuid);
            Assert.Equal(book.IBAN, IBAN);
            Assert.Equal(book.Name, name);
            Assert.Equal(book.Description, description);
            Assert.Equal(book.NumberOfPages, numberOfPages);
            Assert.Equal(book.AuthorId, authorIdGuid);
            Assert.Equal(book.AuthorRegNum, authorRegNum);
            Assert.Equal(book.Amount, amount);
            Assert.Equal(book.PublishDate, datetime);
        }
    }
}
