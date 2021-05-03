
using System.Threading.Tasks;
using MassTransit;

using ResSys.Logistic.Application.Interfaces;
using ResSys.Logistic.Application.ViewModels;

using ResSys.Logistic.Contracts;

namespace ResSys.Logistic.Infrastructure.Data.MassTransit.Notifier
{
    public class StockSuppliesNotifier : IStockSuppliesNotifier
    {

        private readonly IPublishEndpoint publishEndpoint;

        public StockSuppliesNotifier(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public async Task BookCreatedNotification(SupplyBookDto book)
        {
            await publishEndpoint.Publish(new SupplyBook(book.Id, book.IBAN, book.Name, book.Description, book.NumberOfPages, book.Amount, book.PublishDate, book.AuthorRegNum));
        }

        public async Task FilmCreatedNotification(SupplyFilmDto film)
        {
            await publishEndpoint.Publish(new SupplyFilm(film.Id, film.EIDR, film.Name, film.Description, film.Rating, film.Amount, film.PublishDate, film.AuthorRegNum));
        }
    }
}
