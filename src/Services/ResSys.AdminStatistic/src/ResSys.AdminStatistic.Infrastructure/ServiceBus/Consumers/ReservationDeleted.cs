using System.Threading.Tasks;
using MassTransit;
using ResSys.AdminStatistic.Application.Commands.Reservations;
using ResSys.ReservationSystem.Contracts;

namespace ResSys.AdminStatistic.Infrastructure.ServiceBus.Consumers
{
    /// <summary>
    /// Handler for a consumer that processes creation of a reservation
    /// </summary>
    public class ReservationDeletedConsumer : IConsumer<ReservationDeleted>
    {
        private readonly IDeleteReservationUseCase useCase;

        public ReservationDeletedConsumer(IDeleteReservationUseCase useCase)
        {
            this.useCase = useCase;
        }

        public async Task Consume(ConsumeContext<ReservationDeleted> context)
        {
            var msg = context.Message;

            await this.useCase.Execute(msg.id);
        }
    }
}