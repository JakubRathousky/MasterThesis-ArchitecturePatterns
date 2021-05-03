using System.Threading.Tasks;
using System;
using MassTransit;
using ResSys.Logistic.Contracts;
using ResSys.Common;
using ResSys.BookCatalog.Service.Entities;
using ResSys.BookCatalog.Contracts;
using ResSys.BookCatalog.Service.Clients;

namespace ResSys.BookCatalog.Service.Consumers
{
    /// <summary>
    /// Consumer that handles messages of type SupplyBook
    /// </summary>
    public class SupplyBookConsumer : IConsumer<SupplyBook>
    {
        private readonly IRepository<Book> repository;
        private readonly IRepository<Transaction> transRepository;
        private readonly AuthorClient authorClient;

        public SupplyBookConsumer(IRepository<Book> repository, IRepository<Transaction> transRepository, AuthorClient authorClient)
        {
            this.repository = repository;
            this.transRepository = transRepository;
            this.authorClient = authorClient;
        }

        public async Task Consume(ConsumeContext<SupplyBook> context)
        {
            var msg = context.Message;

            try
            {
                var transaction = await transRepository.GetAsync(x => x.TransactionId == msg.TransactionId);

                if (transaction != null)
                    return;

                var item = await repository.GetAsync(x => x.IBAN == msg.IBAN);

                if (item == null)
                {
                    var authorId = await authorClient.GetAuthorIdAsync(msg.AuthorRegNum);

                    if (!authorId.HasValue)
                        return;

                    item = new Book
                    {
                        AuthorId = authorId.Value,
                        IBAN = msg.IBAN,
                        Name = msg.Name,
                        Description = msg.Description,
                        NumberOfPages = msg.NumberOfPages,
                        AuthorRegNum = msg.AuthorRegNum,
                        PublishDate = msg.PublishDate,
                        Amount = msg.Amount
                    };

                    await repository.CreateAsync(item);

                    transaction = new Transaction(msg.TransactionId, item.Id);
                    await transRepository.CreateAsync(transaction);

                    await context.Publish(new BookCreated(msg.TransactionId, item.IBAN, item.Id, item.Name, item.Description, item.NumberOfPages,
                        item.Amount, item.AuthorRegNum, item.PublishDate, authorId.Value));
                }
                else
                {
                    item.Amount += msg.Amount;
                    await repository.UpdateAsync(item);
                    transaction = new Transaction(msg.TransactionId, item.Id);
                    await transRepository.CreateAsync(transaction);
                    await context.Publish(new BookAmountUpdated(item.Id, transaction.TransactionId, item.Amount));
                }

                await context.Publish(new BookSupplyTransactionConfirmation(item.Id, msg.TransactionId));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while proccesing supply request: " + ex.Message);
            }
        }
    }
}