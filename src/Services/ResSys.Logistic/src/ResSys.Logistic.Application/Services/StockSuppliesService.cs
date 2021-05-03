using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResSys.Logistic.Application.Extensions;
using ResSys.Logistic.Application.Interfaces;
using ResSys.Logistic.Application.ViewModels;
using ResSys.Logistic.Domain;
using ResSys.Logistic.Domain.Interfaces;

namespace ResSys.Logistic.Application.Services
{
    public class StockSuppliesService : IStockSuppliesService
    {
        private readonly IStockSupplyRepository supplies;
        private readonly IStockSuppliesNotifier notifier;

        public StockSuppliesService(IStockSupplyRepository supplies, IStockSuppliesNotifier notifier)
        {
            this.supplies = supplies;
            this.notifier = notifier;
        }

        public async Task<IEnumerable<SupplyDto>> GetAllAsync()
        {
            var items = (await supplies.GetAllAsync())
                .Select(item => item.AsDto());

            return items;
        }

        public async Task<SupplyDto> GetByIdAsync(Guid id)
        {
            var item = (await supplies.GetAsync(id)).AsDto();

            return item;
        }
        public async Task Synchronize()
        {
            var dbSupplies = (await supplies.GetAllAsync());

            foreach (var supply in dbSupplies)
            {
                foreach (var film in supply.Films)
                {
                    if (film.FilmId == Guid.Empty || film.FilmId == null)
                        await notifier.FilmCreatedNotification(new SupplyFilmDto(film.Id, film.EIDR, film.FilmId, 
                            film.AuthorRegNum, film.Name, film.Description, film.Rating, film.Amount, film.PublishDate));
                }

                foreach (var book in supply.Books)
                {
                    if (book.BookId == Guid.Empty || book.BookId == null)
                        await notifier.BookCreatedNotification(new SupplyBookDto(book.Id, book.IBAN, book.BookId, 
                            book.AuthorRegNum, book.Name, book.Description, book.NumberOfPages, book.Amount, book.PublishDate));
                }
            }
        }

        public async Task<SupplyDto> CreateAsync(CreateSupplyDto createSupplyDto)
        {
            var item = new StockSupplies
            {
                Films = createSupplyDto.Films.Select(x => x.AsEntity()),
                Books = createSupplyDto.Books.Select(x => x.AsEntity()),
                StorageDate = DateTimeOffset.UtcNow
            };

            await supplies.CreateAsync(item);

            item = await supplies.GetAsync(item.Id);

            foreach (var film in item.Films)
            {
                await notifier.FilmCreatedNotification(new SupplyFilmDto(film.Id, film.EIDR, film.FilmId, 
                    film.AuthorRegNum, film.Name, film.Description, film.Rating, film.Amount, film.PublishDate));
            }
            foreach (var book in item.Books)
            {
                await notifier.BookCreatedNotification(new SupplyBookDto(book.Id, book.IBAN, book.BookId, 
                    book.AuthorRegNum, book.Name, book.Description, book.NumberOfPages, book.Amount, book.PublishDate));
            }

            return item.AsDto();
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = (await supplies.GetAsync(id));

            if (item == null)
                throw new ArgumentException();

            await supplies.RemoveAsync(id);
        }
    }
}