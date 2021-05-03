using System.Threading.Tasks;
using MassTransit;
using ResSys.AdminStatistic.Application.Commands.BookCatalog;
using ResSys.BookCatalog.Contracts;

namespace ResSys.AdminStatistic.Infrastructure.ServiceBus.Consumers
{
    /// <summary>
    /// Handler for a consumer that processes update of a book amount
    /// </summary>
    public class BookAmountUpdatedConsumer : IConsumer<BookAmountUpdated>
    {
        private readonly IUpdateBookAmountUseCase useCase;
        public BookAmountUpdatedConsumer(IUpdateBookAmountUseCase useCase)
        {
            this.useCase = useCase;
        }

        public async Task Consume(ConsumeContext<BookAmountUpdated> context)
        {
            var msg = context.Message;

            if (msg.ItemId != null)
            {
                await this.useCase.Execute(msg.ItemId, msg.Amount);
            }
        }
    }
}