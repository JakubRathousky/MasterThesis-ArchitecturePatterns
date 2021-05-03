using System.Threading.Tasks;
using MassTransit;
using ResSys.Common;
using ResSys.AdminStatistic.Service.Entities;
using ResSys.AdminStatistic.Service.Data;
using System.Linq;
using ResSys.BookCatalog.Contracts;
using System.Threading;

namespace ResSys.AdminStatistic.Service.Consumers
{
    public class BookAmountUpdatedConsumer : IConsumer<BookAmountUpdated>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPublishEndpoint publishEndpoint;

        public BookAmountUpdatedConsumer(IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
        {
            this.unitOfWork = unitOfWork;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<BookAmountUpdated> context)
        {
            var bookRepository = unitOfWork.GetRepository<Book>();
            var msg = context.Message;

            var item = await bookRepository.GetAsync(x => x.Id == msg.ItemId);

            if (item == null)
                return;

            item.Amount = msg.Amount;

            await bookRepository.UpdateAsync(item);
            this.unitOfWork.Complete();
        }
    }
}