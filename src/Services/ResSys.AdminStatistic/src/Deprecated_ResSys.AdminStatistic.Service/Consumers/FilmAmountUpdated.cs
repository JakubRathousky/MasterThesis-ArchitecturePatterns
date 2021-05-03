using System.Threading.Tasks;
using MassTransit;
using ResSys.AdminStatistic.Service.Entities;
using ResSys.AdminStatistic.Service.Data;
using ResSys.Common;
using System.Linq;
using ResSys.FilmCatalog.Contracts;
using System.Threading;

namespace ResSys.AdminStatistic.Service.Consumers
{
    public class FilmAmountUpdatedConsumer : IConsumer<FilmAmountUpdated>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPublishEndpoint publishEndpoint;

        public FilmAmountUpdatedConsumer(IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
        {
            this.unitOfWork = unitOfWork;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<FilmAmountUpdated> context)
        {
            var filmRepository = this.unitOfWork.GetRepository<Film>();
            var msg = context.Message;

            var item = await filmRepository.GetAsync(x => x.Id == msg.ItemId);

            if (item == null)
                return;

            item.Amount = msg.Amount;

            await filmRepository.UpdateAsync(item);
        }
    }
}