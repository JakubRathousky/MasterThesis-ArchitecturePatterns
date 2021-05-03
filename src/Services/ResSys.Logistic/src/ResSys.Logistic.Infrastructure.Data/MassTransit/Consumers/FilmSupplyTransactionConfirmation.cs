using System.Threading.Tasks;
using MassTransit;
using System.Linq;
using ResSys.FilmCatalog.Contracts;
using ResSys.Logistic.Domain.Interfaces;

namespace ResSys.Logistic.Infrastructure.Data.MassTransit.Consumers
{
    public class FilmSupplyTransactionConfirmationConsumer : IConsumer<FilmSupplyTransactionConfirmation>
    {
        private readonly IStockSupplyRepository repository;
        private readonly IPublishEndpoint publishEndpoint;

        public FilmSupplyTransactionConfirmationConsumer(IStockSupplyRepository repository, IPublishEndpoint publishEndpoint)
        {
            this.repository = repository;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<FilmSupplyTransactionConfirmation> context)
        {
            var msg = context.Message;

            var item = await repository.GetAsync(x => x.Films.Any(y => y.Id == msg.TransactionId));

            if (item == null)
                return;

            var film = item.Films.FirstOrDefault(x => x.Id == msg.TransactionId);

            await repository.UpdateOneAsync(item, "Films.Id", film.Id, "Films.$.FilmId", msg.ItemId);
        }
    }
}