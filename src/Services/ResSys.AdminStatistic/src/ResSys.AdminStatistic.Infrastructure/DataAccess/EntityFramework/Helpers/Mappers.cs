using System;
using ResSys.AdminStatistic.Domain.BookCatalog;
using ResSys.AdminStatistic.Domain.FilmCatalog;
using ResSys.AdminStatistic.Domain.Reservation;
using ResSys.Common;

namespace ResSys.AdminStatistic.Infrastructure.EntityFrameworkDataAccess.Helpers
{
    /// <summary>
    /// Přejmenovat na MAPPERS
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Converts a book from Entity model to Domain model
        /// </summary>
        public static Book AsDomain(this Entities.Book book)
        {
            if (book == null)
                return null;
            return new Book()
            {
                Id = book.Id,
                IBAN = book.IBAN,
                Name = book.Name,
                Description = book.Description,
                NumberOfPages = book.NumberOfPages,
                AuthorId = book.AuthorId,
                AuthorRegNum = book.AuthorRegNum,
                Amount = book.Amount,
                PublishDate = book.PublishDate
            };
        }

        /// <summary>
        /// Converts a book from Domain model to Entity model
        /// </summary>
        public static Entities.Book AsEntity(this Book book)
        {
            if (book == null)
                return null;
            return new Entities.Book()
            {
                Id = book.Id,
                IBAN = book.IBAN,
                Name = book.Name,
                Description = book.Description,
                NumberOfPages = book.NumberOfPages,
                AuthorId = book.AuthorId,
                AuthorRegNum = book.AuthorRegNum,
                Amount = book.Amount,
                PublishDate = book.PublishDate
            };
        }

        /// <summary>
        /// Converts a film from Entity model to Domain model
        /// </summary>
        public static Film AsDomain(this Entities.Film film)
        {
            if (film == null)
                return null;
            return new Film()
            {
                Id = film.Id,
                EIDR = film.EIDR,
                Name = film.Name,
                Description = film.Description,
                Rating = film.Rating,
                AuthorId = film.AuthorId,
                AuthorRegNum = film.AuthorRegNum,
                Amount = film.Amount,
                PublishDate = film.PublishDate
            };
        }

        /// <summary>
        /// Converts a film from Domain model to Entity model
        /// </summary>
        public static Entities.Film AsEntity(this Film film)
        {
            if (film == null)
                return null;
            return new Entities.Film()
            {
                Id = film.Id,
                EIDR = film.EIDR,
                Name = film.Name,
                Description = film.Description,
                Rating = film.Rating,
                AuthorId = film.AuthorId,
                AuthorRegNum = film.AuthorRegNum,
                Amount = film.Amount,
                PublishDate = film.PublishDate
            };
        }

        /// <summary>
        /// Converts a reservation from Entity model to Domain model
        /// </summary>
        public static Reservation AsDomain(this Entities.Reservation reservation)
        {
            if (reservation == null)
                return null;
            return new Reservation()
            {
                Id = reservation.Id,
                ReservationFrom = reservation.ReservationFrom,
                ReservationTo = reservation.ReservationTo,
                IsActive = reservation.IsActive
            };
        }

        /// <summary>
        /// Converts a reservation from Domain model to Entity model
        /// </summary>
        public static Entities.Reservation AsEntity(this Reservation reservation)
        {
            if (reservation == null)
                return null;
            return new Entities.Reservation()
            {
                Id = reservation.Id,
                ReservationFrom = reservation.ReservationFrom,
                ReservationTo = reservation.ReservationTo,
                IsActive = reservation.IsActive
            };
        }
    }
}

