using ResSys.Logistic.Domain;
using ResSys.Logistic.Application.ViewModels;
using System.Linq;
using System;


namespace ResSys.Logistic.Application.Extensions
{
    public static class Mappers
    {
        public static SupplyDto AsDto(this StockSupplies item)
        {
            if (item == null)
                return null;
            var dto = new SupplyDto(
                item.Id,
                item.Films.Select(x => new SupplyFilmDto(
                    x.Id,
                    x.EIDR,
                    x.FilmId,
                    x.AuthorRegNum,
                    x.Name,
                    x.Description,
                    x.Rating,
                    x.Amount,
                    x.PublishDate
                )),
                item.Books.Select(x => new SupplyBookDto(
                    x.Id,
                    x.IBAN,
                    x.BookId,
                    x.AuthorRegNum,
                    x.Name,
                    x.Description,
                    x.NumberOfPages,
                    x.Amount,
                    x.PublishDate
                )),
                item.StorageDate,
                !(item.Films.Any(x => x.FilmId == Guid.Empty) || item.Books.Any(x => x.BookId == Guid.Empty))
                );
            return dto;
        }

        public static Film AsEntity(this SupplyFilmDto filmDto)
        {
            if (filmDto == null)
                return null;

            return new Film()
            {
                Id = filmDto.Id,
                EIDR = filmDto.EIDR,
                FilmId = filmDto.FilmId,
                AuthorRegNum = filmDto.AuthorRegNum,
                Name = filmDto.Name,
                Description = filmDto.Description,
                Rating = filmDto.Rating,
                Amount = filmDto.Amount,
                PublishDate = filmDto.PublishDate
            };
        }

        public static Book AsEntity(this SupplyBookDto bookDto)
        {
            if (bookDto == null)
                return null;

            return new Book()
            {
                Id = bookDto.Id,
                IBAN = bookDto.IBAN,
                BookId = bookDto.BookId,
                AuthorRegNum = bookDto.AuthorRegNum,
                Name = bookDto.Name,
                Description = bookDto.Description,
                NumberOfPages = bookDto.NumberOfPages,
                Amount = bookDto.Amount,
                PublishDate = bookDto.PublishDate
            };
        }
        public static Film AsEntity(this CreateSupplyFilmDto filmDto)
        {
            if (filmDto == null)
                return null;

            return new Film()
            {
                Id = Guid.NewGuid(),
                EIDR = filmDto.EIDR,
                AuthorRegNum = filmDto.AuthorRegNum,
                Name = filmDto.Name,
                Description = filmDto.Description,
                Rating = filmDto.Rating,
                Amount = filmDto.Amount,
                PublishDate = filmDto.PublishDate
            };
        }

        public static Book AsEntity(this CreateSupplyBookDto bookDto)
        {
            if (bookDto == null)
                return null;

            return new Book()
            {
                Id = Guid.NewGuid(),
                IBAN = bookDto.IBAN,
                AuthorRegNum = bookDto.AuthorRegNum,
                Name = bookDto.Name,
                Description = bookDto.Description,
                NumberOfPages = bookDto.NumberOfPages,
                Amount = bookDto.Amount,
                PublishDate = bookDto.PublishDate
            };
        }
    }
}