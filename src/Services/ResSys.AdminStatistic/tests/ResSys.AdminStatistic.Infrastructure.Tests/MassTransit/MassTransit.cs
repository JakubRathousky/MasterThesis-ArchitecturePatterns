using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Testing;
// using NUnit.Framework;
using Xunit;
using Moq;
using ResSys.AdminStatistic.Application.Commands.BookCatalog;
using ResSys.AdminStatistic.Application.Commands.FilmCatalog;
using ResSys.BookCatalog.Contracts;
using ResSys.FilmCatalog.Contracts;
using ResSys.AdminStatistic.Infrastructure.ServiceBus.Consumers;

namespace ResSys.AdminStatistic.Infrastructure.Tests
{

    public class MassTransit
    {
        [Fact]
        public async Task BookAmountUpdated()
        {
            var harness = new InMemoryTestHarness();
            var useCase = new Mock<IUpdateBookAmountUseCase>();
            var consumerHarness = harness.Consumer<BookAmountUpdatedConsumer>(() => new BookAmountUpdatedConsumer(useCase.Object));

            await harness.Start();
            try
            {
                BookAmountUpdated book = new BookAmountUpdated(Guid.NewGuid(), Guid.NewGuid(), 15);

                await harness.InputQueueSendEndpoint.Send<BookAmountUpdated>(book);

                Assert.True(await harness.Consumed.Any<BookAmountUpdated>());
                Assert.True(await consumerHarness.Consumed.Any<BookAmountUpdated>());
            }
            finally
            {
                await harness.Stop();
            }
        }
        [Fact]
        public async Task BookCreatedConsumer()
        {
            var harness = new InMemoryTestHarness();
            var useCase = new Mock<ISaveBookUseCase>();
            var consumerHarness = harness.Consumer<BookCreatedConsumer>(() => new BookCreatedConsumer(useCase.Object));

            await harness.Start();
            try
            {
                BookCreated book = new BookCreated(Guid.NewGuid(), "IBAN1", Guid.NewGuid(), "Kniha1", "Popis", 2, 15, 1, DateTime.Now, Guid.NewGuid());

                await harness.InputQueueSendEndpoint.Send<BookCreated>(book);

                Assert.True(await harness.Consumed.Any<BookCreated>());
                Assert.True(await consumerHarness.Consumed.Any<BookCreated>());
            }
            finally
            {
                await harness.Stop();
            }
        }


        [Fact]
        public async Task FilmAmountUpdatedConsumer()
        {
            var harness = new InMemoryTestHarness();
            var repository = new Mock<IUpdateFilmAmountUseCase>();
            var consumerHarness = harness.Consumer<FilmAmountUpdatedConsumer>(() => new FilmAmountUpdatedConsumer(repository.Object));
            var consumerHarness2 = harness.Consumer<FilmAmountUpdatedConsumer>(() => new FilmAmountUpdatedConsumer(repository.Object));

            await harness.Start();
            try
            {
                FilmAmountUpdated book = new FilmAmountUpdated(Guid.NewGuid(), Guid.NewGuid(), 2);

                await harness.InputQueueSendEndpoint.Send<FilmAmountUpdated>(book);

                Assert.True(await harness.Consumed.Any<FilmAmountUpdated>());
                Assert.True(await consumerHarness.Consumed.Any<FilmAmountUpdated>());
            }
            finally
            {
                await harness.Stop();
            }
        }
        [Fact]
        public async Task FilmCreatedConsumer()
        {
            var harness = new InMemoryTestHarness();
            var useCase = new Mock<ISaveFilmUseCase>();
            var consumerHarness = harness.Consumer<FilmCreatedConsumer>(() => new FilmCreatedConsumer(useCase.Object));

            await harness.Start();
            try
            {
                FilmCreated film = new FilmCreated("IBAN1", Guid.NewGuid(), "Kniha1", "Popis", 2, 15, 1, DateTime.Now, Guid.NewGuid());

                await harness.InputQueueSendEndpoint.Send<FilmCreated>(film);

                Assert.True(await harness.Consumed.Any<FilmCreated>());
                Assert.True(await consumerHarness.Consumed.Any<FilmCreated>());
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}