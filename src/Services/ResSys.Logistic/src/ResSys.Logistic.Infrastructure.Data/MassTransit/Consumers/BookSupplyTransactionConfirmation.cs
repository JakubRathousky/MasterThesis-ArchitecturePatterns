using System.Threading.Tasks;
using MassTransit;
using System.Linq;
using ResSys.BookCatalog.Contracts;
using ResSys.Logistic.Domain.Interfaces;

namespace ResSys.Logistic.Infrastructure.Data.MassTransit.Consumers
{
    public class SupplyBookConsumer : IConsumer<BookSupplyTransactionConfirmation>
    {
        private readonly IStockSupplyRepository repository;

        public SupplyBookConsumer(IStockSupplyRepository repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<BookSupplyTransactionConfirmation> context)
        {
            var msg = context.Message;

            var item = await repository.GetAsync(x => x.Books.Any(y => y.Id == msg.TransactionId));

            if (item == null)
                return;

            var book = item.Books.FirstOrDefault(x => x.Id == msg.TransactionId);

            await repository.UpdateOneAsync(item, "Books.Id", book.Id, "Books.$.BookId", msg.ItemId);
        }
    }
}