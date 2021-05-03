using System.Threading.Tasks;
using MassTransit;
using ResSys.AdminStatistic.Application.Commands.FilmCatalog;
using ResSys.AdminStatistic.Application.Dtos;
using ResSys.FilmCatalog.Contracts;

namespace ResSys.AdminStatistic.Infrastructure.ServiceBus.Consumers
{
    /// <summary>
    /// Handler for a consumer that processes update of a film
    /// </summary>
    public class FilmCreatedConsumer : IConsumer<FilmCreated>
    {
        private readonly ISaveFilmUseCase useCase;

        public FilmCreatedConsumer(ISaveFilmUseCase useCase)
        {
            this.useCase = useCase;
        }

        public async Task Consume(ConsumeContext<FilmCreated> context)
        {
            var msg = context.Message;

            if (
                !string.IsNullOrEmpty(msg.EIDR)
                && !string.IsNullOrEmpty(msg.Name)
            )
            {
                await this.useCase.Execute(new CreateFilmDto(msg.ItemId,
                msg.EIDR,
                msg.Name,
                msg.Description,
                msg.Rating,
                msg.AuthorId,
                msg.AuthorRegNum,
                msg.Amount,
                msg.PublishDate.DateTime
                ));
            };
        }
    }
}