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
    public class BookCreatedConsumer : IConsumer<BookCreated>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPublishEndpoint publishEndpoint;

        public BookCreatedConsumer(IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
        {
            this.unitOfWork = unitOfWork;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<BookCreated> context)
        {
            var bookRepository = this.unitOfWork.GetRepository<Book>();
            var msg = context.Message;

            var item = await bookRepository.GetAsync(x => x.Id == msg.ItemId);

            if (item != null)
                return;

            item = new Book()
            {
                Id = msg.ItemId,
                IBAN = msg.IBAN,
                Name = msg.Name,
                Description = msg.Description,
                NumberOfPages = msg.NumberOfPages,
                AuthorId = msg.AuthorId,
                AuthorRegNum = msg.AuthorRegNum,
                Amount = msg.Amount,
                PublishDate = msg.PublishDate.DateTime
            };
            await bookRepository.CreateAsync(item);
            unitOfWork.Complete();
        }
    }
}