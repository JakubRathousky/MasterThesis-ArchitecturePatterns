using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Testing;
using Xunit;
using Moq;
using ResSys.Common;
using ResSys.FilmCatalog.Contracts;
using ResSys.BookCatalog.Contracts;
using ResSys.Logistic.Infrastructure.Data.MassTransit.Consumers;
using ResSys.Logistic.Domain.Interfaces;
using System.Net.Http;

namespace ResSys.Logistic.Infrastructure.Data.Consumers.Tests
{

    public class MassTransit
    {
        [Fact]
        public async Task FilmSupplyTransactionConfirmationConsumer()
        {
            var repository = new Mock<IStockSupplyRepository>();
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<FilmSupplyTransactionConfirmationConsumer>(() => new FilmSupplyTransactionConfirmationConsumer(repository.Object));

            await harness.Start();
            try
            {
                FilmSupplyTransactionConfirmation conf = new FilmSupplyTransactionConfirmation(Guid.NewGuid(), Guid.NewGuid());

                await harness.InputQueueSendEndpoint.Send<FilmSupplyTransactionConfirmation>(conf);

                Assert.True(await harness.Consumed.Any<FilmSupplyTransactionConfirmation>());
                Assert.True(await consumerHarness.Consumed.Any<FilmSupplyTransactionConfirmation>());
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Fact]
        public async Task SupplyBookConsumer()
        {
            var repository = new Mock<IStockSupplyRepository>();
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<SupplyBookConsumer>(() => new SupplyBookConsumer(repository.Object));

            await harness.Start();
            try
            {
                BookSupplyTransactionConfirmation conf = new BookSupplyTransactionConfirmation(Guid.NewGuid(), Guid.NewGuid());

                await harness.InputQueueSendEndpoint.Send<BookSupplyTransactionConfirmation>(conf);

                Assert.True(await harness.Consumed.Any<BookSupplyTransactionConfirmation>());
                Assert.True(await consumerHarness.Consumed.Any<BookSupplyTransactionConfirmation>());
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}