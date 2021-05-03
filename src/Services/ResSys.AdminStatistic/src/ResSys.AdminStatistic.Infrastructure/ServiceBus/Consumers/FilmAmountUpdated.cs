using System.Threading.Tasks;
using MassTransit;
using ResSys.AdminStatistic.Application.Commands.FilmCatalog;
using ResSys.FilmCatalog.Contracts;

namespace ResSys.AdminStatistic.Infrastructure.ServiceBus.Consumers
{
    /// <summary>
    /// Handler for a consumer that processes update of a film amount
    /// </summary>
    public class FilmAmountUpdatedConsumer : IConsumer<FilmAmountUpdated>
    {
        private readonly IUpdateFilmAmountUseCase useCase;

        public FilmAmountUpdatedConsumer(IUpdateFilmAmountUseCase useCase)
        {
            this.useCase = useCase;
        }

        public async Task Consume(ConsumeContext<FilmAmountUpdated> context)
        {
            var msg = context.Message;

            if (msg.ItemId != null)
            {
                await this.useCase.Execute(msg.ItemId, msg.Amount);
            }
        }
    }
}