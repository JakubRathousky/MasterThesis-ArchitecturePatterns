using Xunit;
using NSubstitute;
using System;
using System.Collections.Generic;

using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Application.Commands.FilmCatalog;

namespace ResSys.AdminStatistic.Application.Tests
{
    public class SaveFilmTests
    {
        private List<DateTimeOffset> publishDates = new List<DateTimeOffset>();
        private readonly IFilmWriteOnlyRepository writeOnlyRepository;
        private readonly IFilmReadOnlyRepository readOnlyRepository;

        public SaveFilmTests()
        {
            readOnlyRepository = Substitute.For<IFilmReadOnlyRepository>();
            writeOnlyRepository = Substitute.For<IFilmWriteOnlyRepository>();
        }

        [Theory]
        [InlineData("A3A12B10-21B3-46C5-92CE-C888CF9856D0", "EIDR1", "Film 1", "description 1", 10, "A3A12B10-21B3-46C5-92CE-C888CF9856D0", 1, 5, "2021-04-24")]
        public async void Save_Films(string FilmId, string EIDR, string name, string description, int rating, string authorId, int authorRegNum, int amount, string publishDate)
        {
            var datetime = DateTimeOffset.Parse(publishDate);
            var filmIdGuid = Guid.Parse(FilmId);
            var authorIdGuid = Guid.Parse(authorId);

            var useCase = new SaveFilmUseCase(
                writeOnlyRepository,
                readOnlyRepository
            );


            var film = await useCase.Execute(filmIdGuid, EIDR, name, description, rating, authorIdGuid, authorRegNum, amount, datetime);


            Assert.Equal(film.Id, filmIdGuid);
            Assert.Equal(film.EIDR, EIDR);
            Assert.Equal(film.Name, name);
            Assert.Equal(film.Description, description);
            Assert.Equal(film.Rating, rating);
            Assert.Equal(film.AuthorId, authorIdGuid);
            Assert.Equal(film.AuthorRegNum, authorRegNum);
            Assert.Equal(film.Amount, amount);
            Assert.Equal(film.PublishDate, datetime);
        }
    }
}
