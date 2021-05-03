using Xunit;
using NSubstitute;
using System;
using System.Collections.Generic;
using ResSys.FilmCatalog.Service.Entities;
using ResSys.FilmCatalog.Service.Clients;
using ResSys.FilmCatalog.Service.Interfaces;
using ResSys.FilmCatalog.Service.Dtos;
using ResSys.Common;
using System.Transactions;
using ResSys.FilmCatalog;
using System.Net.Http;

namespace ResSys.AdminStatistic.Application.Tests
{
    public class FilmServiceTests
    {
        private List<DateTimeOffset> publishDates = new List<DateTimeOffset>();

        private readonly IRepository<FilmCatalog.Service.Entities.Transaction> transRepository;
        private readonly IRepository<Film> filmsRepository;

        private readonly IFilmNotifier notifier;
        private readonly IAuthorClient authorClient;
        public FilmServiceTests()
        {
            transRepository = Substitute.For<IRepository<FilmCatalog.Service.Entities.Transaction>>();
            filmsRepository = Substitute.For<IRepository<Film>>();
            notifier = Substitute.For<IFilmNotifier>();
            authorClient = Substitute.For<IAuthorClient>();
        }

        [Theory]
        [InlineData("A3A12B10-21B3-46C5-92CE-C888CF9856D0", "EIDR1", "Film 1", "description 1", 10, "A3A12B10-21B3-46C5-92CE-C888CF9856D0", 1, 5, "2021-04-24")]
        public async void SupplyFilmAsync_Success(string filmId, string eidr, string name, string description, int rating, string authorId, int authorRegNum, int amount, string publishDate)
        {
            var datetime = DateTimeOffset.Parse(publishDate);
            var filmIdGuid = Guid.Parse(filmId);
            var authorIdGuid = Guid.Parse(authorId);

            var useCase = new FilmService(
                filmsRepository,
                transRepository,
                notifier,
                authorClient
            );

            // transRepository
            //     .GetAsync(null)
            //     .ReturnsForAnyArgs();
            // filmsRepository
            //     .GetAsync(filmIdGuid)
            //     .ReturnsForAnyArgs(null);
            authorClient
                .GetAuthorIdAsync(authorRegNum)
                .ReturnsForAnyArgs(authorIdGuid);

            CreateFilmDto dto = new CreateFilmDto(
                eidr,
                name,
                description,
                rating,
                datetime,
                authorRegNum,
                amount
            );

            FilmDto film = await useCase.SupplyFilmAsync(dto, Guid.NewGuid());

            Assert.Equal(eidr, film.EIDR);
            Assert.Equal(name, film.Name);
            Assert.Equal(description, film.Description);
            Assert.Equal(rating, film.Rating);
            Assert.Equal(authorIdGuid, film.AuthorId);
            Assert.Equal(authorRegNum, film.AuthorRegNum);
            Assert.Equal(amount, film.Amount);
            Assert.Equal(datetime, film.PublishDate);
        }
        [Theory]
        [InlineData("A3A12B10-21B3-46C5-92CE-C888CF9856D0", "EIDR1", "Film 1", "description 1", 10, "A3A12B10-21B3-46C5-92CE-C888CF9856D0", 1, 5, "2021-04-24")]
        public async void SupplyFilmAsync_UpdateAmount(string filmId, string eidr, string name, string description, int rating, string authorId, int authorRegNum, int amount, string publishDate)
        {
            var datetime = DateTimeOffset.Parse(publishDate);
            var filmIdGuid = Guid.Parse(filmId);
            var authorIdGuid = Guid.Parse(authorId);
            var transactionIdGuid = Guid.NewGuid();

            var useCase = new FilmService(
                filmsRepository,
                transRepository,
                notifier,
                authorClient
            );

            var amount2 = 10;
            Film film = new Film();
            film.Id = filmIdGuid;
            film.Amount = amount2;

            // transRepository
            //     .GetAsync(transactionIdGuid)
            //     .Returns(new FilmCatalog.Service.Entities.Transaction(transactionIdGuid, Guid.NewGuid()));
            filmsRepository
                .GetAsync(x => x.EIDR == eidr)
                .ReturnsForAnyArgs(film);
            authorClient
                .GetAuthorIdAsync(authorRegNum)
                .ReturnsForAnyArgs(authorIdGuid);

            CreateFilmDto dto = new CreateFilmDto(
                eidr,
                name,
                description,
                rating,
                datetime,
                authorRegNum,
                amount
            );

            FilmDto film2 = await useCase.SupplyFilmAsync(dto, transactionIdGuid);

            Assert.Equal(film2.Amount, amount2 + dto.Amount);
        }
        [Theory]
        [InlineData("A3A12B10-21B3-46C5-92CE-C888CF9856D0", "EIDR1", "Film 1", "description 1", 10, "A3A12B10-21B3-46C5-92CE-C888CF9856D0", 1, 5, "2021-04-24")]
        public async void SupplyFilmAsync_ExistingTransaction(string filmId, string eidr, string name, string description, int rating, string authorId, int authorRegNum, int amount, string publishDate)
        {
            var datetime = DateTimeOffset.Parse(publishDate);
            var filmIdGuid = Guid.Parse(filmId);
            var authorIdGuid = Guid.Parse(authorId);
            var transactionIdGuid = Guid.NewGuid();

            var useCase = new FilmService(
                filmsRepository,
                transRepository,
                notifier,
                authorClient
            );

            transRepository
                .GetAsync(transactionIdGuid)
                .Returns(new FilmCatalog.Service.Entities.Transaction(transactionIdGuid, Guid.NewGuid()));
            // filmsRepository
            //     .GetAsync(filmIdGuid)
            //     .ReturnsForAnyArgs(null);
            authorClient
                .GetAuthorIdAsync(authorRegNum)
                .ReturnsForAnyArgs(authorIdGuid);

            CreateFilmDto dto = new CreateFilmDto(
                eidr,
                name,
                description,
                rating,
                datetime,
                authorRegNum,
                amount
            );

            FilmDto film = await useCase.SupplyFilmAsync(dto, transactionIdGuid);

            Assert.Null(film);
        }
    }
}
