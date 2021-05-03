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
    public class FilmCreatedConsumer : IConsumer<FilmCreated>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPublishEndpoint publishEndpoint;

        public FilmCreatedConsumer(IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
        {
            this.unitOfWork = unitOfWork;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<FilmCreated> context)
        {
            var filmRepository = this.unitOfWork.GetRepository<Film>();
            var msg = context.Message;

            var item = await filmRepository.GetAsync(x => x.Id == msg.ItemId);

            if (item != null)
                return;

            item = new Film
            {
                Id = msg.ItemId,
                Name = msg.Name,
                EIDR = msg.EIDR,
                Description = msg.Description,
                Rating = msg.Rating,
                AuthorRegNum = msg.AuthorRegNum,
                PublishDate = msg.PublishDate.DateTime,
                AuthorId = msg.AuthorId,
                Amount = msg.Amount
            };
            await filmRepository.CreateAsync(item);
        }
    }
}