using Xunit;
using NSubstitute;
using System;
using System.Collections.Generic;

using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Application.Commands.BookCatalog;
using ResSys.AdminStatistic.Domain.BookCatalog;
using System.Linq;

namespace ResSys.AdminStatistic.Application.Tests
{
    public class GetBookTests
    {

        private List<Guid> bookGuids = new List<Guid>();
        private List<Guid> authorGuids = new List<Guid>();
        private List<DateTimeOffset> publishDates = new List<DateTimeOffset>();
        private readonly IBookWriteOnlyRepository writeOnlyRepository;
        private readonly IBookReadOnlyRepository readOnlyRepository;
        public GetBookTests()
        {
            readOnlyRepository = Substitute.For<IBookReadOnlyRepository>();
            writeOnlyRepository = Substitute.For<IBookWriteOnlyRepository>();
        }

        [Fact]
        public async void Get_all_books()
        {
            var getUseCase = new GetAllBooksUseCase(
                readOnlyRepository
            );

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
                .GetAllAsync()
                .ReturnsForAnyArgs(new List<Book>() { book });

            var result = await getUseCase.Execute();


            Assert.Equal(result.Count(), 1);
            Assert.Equal(result.ElementAt(0).Id, book.Id);
            Assert.Equal(result.ElementAt(0).IBAN, book.IBAN);
            Assert.Equal(result.ElementAt(0).Name, book.Name);
            Assert.Equal(result.ElementAt(0).Description, book.Description);
            Assert.Equal(result.ElementAt(0).NumberOfPages, book.NumberOfPages);
            Assert.Equal(result.ElementAt(0).AuthorId, book.AuthorId);
            Assert.Equal(result.ElementAt(0).AuthorRegNum, book.AuthorRegNum);
            Assert.Equal(result.ElementAt(0).Amount, book.Amount);
            Assert.Equal(result.ElementAt(0).PublishDate, book.PublishDate);
        }
    }
}
