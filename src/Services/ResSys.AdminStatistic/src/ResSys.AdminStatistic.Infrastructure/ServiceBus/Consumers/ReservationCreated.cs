using System.Threading.Tasks;
using MassTransit;
using ResSys.AdminStatistic.Application.Commands.Reservations;
using ResSys.ReservationSystem.Contracts;

namespace ResSys.AdminStatistic.Infrastructure.ServiceBus.Consumers
{
    /// <summary>
    /// Handler for a consumer that processes creation of a reservation
    /// </summary>
    public class ReservationCreatedConsumer : IConsumer<ReservationCreated>
    {
        private readonly ISaveReservationUseCase useCase;
        private readonly ISaveReservationItemUseCase itemUseCase;
        private readonly IPublishEndpoint publishEndpoint;

        public ReservationCreatedConsumer(IPublishEndpoint publishEndpoint, ISaveReservationUseCase useCase, ISaveReservationItemUseCase itemUseCase)
        {
            this.useCase = useCase;
            this.publishEndpoint = publishEndpoint;
            this.itemUseCase = itemUseCase;
        }

        public async Task Consume(ConsumeContext<ReservationCreated> context)
        {
            var msg = context.Message;

            await this.useCase.Execute(msg.Id, msg.ReservationFrom, msg.ReservationTo, true);

            foreach (var book in msg.Books)
            {
                await itemUseCase.Execute(book.Id, msg.Id, book.Amount, book.BookId, null);
            }

            foreach (var film in msg.Films)
            {
                await itemUseCase.Execute(film.Id, msg.Id, film.Amount, null, film.FilmdId);
            }
        }
    }
}