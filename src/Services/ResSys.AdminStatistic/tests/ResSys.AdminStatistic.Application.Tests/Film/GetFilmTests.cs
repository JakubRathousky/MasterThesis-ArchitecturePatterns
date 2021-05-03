using Xunit;
using NSubstitute;
using System;
using System.Collections.Generic;

using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Application.Commands.FilmCatalog;
using ResSys.AdminStatistic.Domain.FilmCatalog;
using System.Linq;

namespace ResSys.AdminStatistic.Application.Tests
{
    public class GetFilmTests
    {
        private readonly IFilmWriteOnlyRepository writeOnlyRepository;
        private readonly IFilmReadOnlyRepository readOnlyRepository;
        public GetFilmTests()
        {
            readOnlyRepository = Substitute.For<IFilmReadOnlyRepository>();
            writeOnlyRepository = Substitute.For<IFilmWriteOnlyRepository>();
        }

        [Fact]
        public async void Get_all_Films()
        {
            var getUseCase = new GetFilmUseCase(
                readOnlyRepository
            );

            Film film = new Film()
            {
                Id = Guid.Parse("A3A12B10-21B3-46C5-92CE-C888CF9856D0"),
                EIDR = "EIDR1",
                Name = "Kniha 1",
                Description = "description 1",
                Rating = 10,
                AuthorId = Guid.Parse("A3A12B10-21B3-46C5-92CE-C888CF9856D0"),
                AuthorRegNum = 1,
                Amount = 5,
                PublishDate = DateTime.Parse("2021-04-24")
            };

            readOnlyRepository
                .GetAllAsync()
                .ReturnsForAnyArgs(new List<Film>() { film });

            var result = await getUseCase.Execute();


            Assert.Equal(result.Count(), 1);
            Assert.Equal(result.ElementAt(0).Id, film.Id);
            Assert.Equal(result.ElementAt(0).EIDR, film.EIDR);
            Assert.Equal(result.ElementAt(0).Name, film.Name);
            Assert.Equal(result.ElementAt(0).Description, film.Description);
            Assert.Equal(result.ElementAt(0).Rating, film.Rating);
            Assert.Equal(result.ElementAt(0).AuthorId, film.AuthorId);
            Assert.Equal(result.ElementAt(0).AuthorRegNum, film.AuthorRegNum);
            Assert.Equal(result.ElementAt(0).Amount, film.Amount);
            Assert.Equal(result.ElementAt(0).PublishDate, film.PublishDate);
        }
    }
}
