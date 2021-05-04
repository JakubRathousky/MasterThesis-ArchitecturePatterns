using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Testing;
// using NUnit.Framework;
using Xunit;
using Moq;
using ResSys.Common;
using ResSys.BookCatalog.Service.Clients;
using ResSys.BookCatalog.Service.Entities;
using ResSys.Logistic.Contracts;
using System.Net.Http;

namespace ResSys.BookCatalog.Service.Consumers.Tests
{

    public class MassTransit
    {
        [Fact]
        public async Task BookCreatedConsumer()
        {
            var repository = new Mock<IRepository<Book>>();
            var transRepository = new Mock<IRepository<Transaction>>();
            var httpClient = new Mock<HttpClient>();
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<SupplyBookConsumer>(() => new SupplyBookConsumer(repository.Object, transRepository.Object, new AuthorClient(httpClient.Object)));

            await harness.Start();
            try
            {
                SupplyBook book = new SupplyBook(Guid.NewGuid(), "IBAN1", "Kniha1", "Popis", 2, 15, DateTime.Now, 2);

                await harness.InputQueueSendEndpoint.Send<SupplyBook>(book);

                Assert.True(await harness.Consumed.Any<SupplyBook>());
                Assert.True(await consumerHarness.Consumed.Any<SupplyBook>());
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}