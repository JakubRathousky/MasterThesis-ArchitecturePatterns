using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Testing;
using Xunit;
using Moq;
using ResSys.Common;
using ResSys.Logistic.Contracts;
using ResSys.FilmCatalog.Service.Consumers;
using ResSys.FilmCatalog.Service.Interfaces;
using System.Net.Http;

namespace ResSys.FilmCatalog.Infrastructure.Consumers.Tests
{
    public class MassTransit
    {
        [Fact]
        public async Task BookCreatedConsumer()
        {
            var filmService = new Mock<IFilmService>();
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<SupplyFilmConsumer>(() => new SupplyFilmConsumer(filmService.Object));

            await harness.Start();
            try
            {
                SupplyFilm film = new SupplyFilm(Guid.NewGuid(), "IBAN1", "Kniha1", "Popis", 2, 15, DateTime.Now, 2);

                await harness.InputQueueSendEndpoint.Send<SupplyFilm>(film);

                Assert.True(await harness.Consumed.Any<SupplyFilm>());
                Assert.True(await consumerHarness.Consumed.Any<SupplyFilm>());
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}