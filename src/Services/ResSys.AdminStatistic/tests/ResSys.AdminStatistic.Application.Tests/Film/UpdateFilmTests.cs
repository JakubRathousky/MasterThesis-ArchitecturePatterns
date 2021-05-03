using Xunit;
using NSubstitute;
using System;
using System.Collections.Generic;

using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Application.Commands.FilmCatalog;
using ResSys.AdminStatistic.Domain.FilmCatalog;
using ResSys.AdminStatistic.Application;

using System.Linq;

namespace ResSys.AdminStatistic.Application.Tests
{
    public class UpdateFilmTests
    {
        private readonly IFilmWriteOnlyRepository writeOnlyRepository;
        private readonly IFilmReadOnlyRepository readOnlyRepository;

        public UpdateFilmTests()
        {
            readOnlyRepository = Substitute.For<IFilmReadOnlyRepository>();
            writeOnlyRepository = Substitute.For<IFilmWriteOnlyRepository>();
        }

        [Fact]
        public async void Update_Films()
        {
            Film Film = new Film()
            {
                Id = Guid.Parse("A3A12B10-21B3-46C5-92CE-C888CF9856D0"),
                EIDR = "EIDR1",
                Name = "Film 1",
                Description = "description 1",
                Rating = 10,
                AuthorId = Guid.Parse("A3A12B10-21B3-46C5-92CE-C888CF9856D0"),
                AuthorRegNum = 1,
                Amount = 5,
                PublishDate = DateTime.Parse("2021-04-24")
            };

            readOnlyRepository
                .GetAsync(Film.Id)
                .Returns(Film);

            var useCase = new UpdateFilmAmountUseCase(
                writeOnlyRepository,
                readOnlyRepository
            );


            await useCase.Execute(Film.Id, 15);
        }



        [Fact]
        public async void Update_Films_not_found()
        {
            Film Film = new Film()
            {
                Id = Guid.Parse("A3A12B10-21B3-46C5-92CE-C888CF9856D0"),
                EIDR = "EIDR1",
                Name = "Film 1",
                Description = "description 1",
                Rating = 10,
                AuthorId = Guid.Parse("A3A12B10-21B3-46C5-92CE-C888CF9856D0"),
                AuthorRegNum = 1,
                Amount = 5,
                PublishDate = DateTime.Parse("2021-04-24")
            };

            readOnlyRepository
                .GetAsync(Film.Id)
                .Returns(Film);

            var useCase = new UpdateFilmAmountUseCase(
                writeOnlyRepository,
                readOnlyRepository
            );

            await Assert.ThrowsAsync<FilmNotFoundException>(() => useCase.Execute(Guid.NewGuid(), 15));
        }
    }
}
