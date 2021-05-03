// using System.Threading.Tasks;
// using MassTransit;
// using ResSys.AdminStatistic.Service.Entities;
// using ResSys.Common;
// using System.Linq;
// using ResSys.FilmCatalog.Contracts;
// using System.Threading;

// namespace ResSys.AdminStatistic.Service.Consumers
// {
//     public class SupplyFilmConsumer : IConsumer<FilmCreated>
//     {
//         private readonly IRepository<StockSupplies> repository;
//         private readonly IPublishEndpoint publishEndpoint;

//         public SupplyFilmConsumer(IRepository<StockSupplies> repository, IPublishEndpoint publishEndpoint)
//         {
//             this.repository = repository;
//             this.publishEndpoint = publishEndpoint;
//         }

//         public async Task Consume(ConsumeContext<FilmCreated> context)
//         {
//             var msg = context.Message;

//             var item = await repository.GetAsync(x => x.Films.Any(y => y.TransactionId == msg.TransactionId));

//             if (item == null)
//                 return;

//             var film = item.Films.FirstOrDefault(x => x.TransactionId == msg.TransactionId);

//             film.Id = msg.ItemId;

//             await repository.UpdateAsync(item);
//         }
//     }
// }