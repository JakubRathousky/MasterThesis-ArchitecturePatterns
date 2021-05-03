using System.Threading.Tasks;
using MassTransit;
using ResSys.BookCatalog.Contracts;
using ResSys.AdminStatistic.Application.Commands.BookCatalog;
using ResSys.AdminStatistic.Application.Dtos;

namespace ResSys.AdminStatistic.Infrastructure.ServiceBus.Consumers
{
    /// <summary>
    /// Handler for a consumer that processes update of a book
    /// </summary>
    public class BookCreatedConsumer : IConsumer<BookCreated>
    {
        private readonly ISaveBookUseCase useCase;

        public BookCreatedConsumer(ISaveBookUseCase useCase)
        {
            this.useCase = useCase;
        }

        public async Task Consume(ConsumeContext<BookCreated> context)
        {
            var msg = context.Message;

            if (
                !string.IsNullOrEmpty(msg.IBAN)
                && !string.IsNullOrEmpty(msg.Name)
            )
            {
                await this.useCase.Execute(new CreateBookDto(msg.ItemId,
                msg.IBAN,
                msg.Name,
                msg.Description,
                msg.NumberOfPages,
                msg.AuthorId,
                msg.AuthorRegNum,
                msg.Amount,
                msg.PublishDate.DateTime));
            };
        }
    }
}