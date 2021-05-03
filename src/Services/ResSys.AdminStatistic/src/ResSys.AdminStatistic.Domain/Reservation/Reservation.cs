using System;
using System.Collections.Generic;
using ResSys.Common;

using ResSys.AdminStatistic.Domain.FilmCatalog;
using ResSys.AdminStatistic.Domain.BookCatalog;

namespace ResSys.AdminStatistic.Domain.Reservation
{
    /// <summary>
    /// Reservation entity
    /// </summary>
    public class Reservation : IEntity
    {
        public Guid Id { get; set; }

        /// <summary>
        /// List of films
        /// </summary>
        public IEnumerable<Film> Films { get; set; }

        /// <summary>
        /// List of books
        /// </summary>
        public IEnumerable<Book> Books { get; set; }

        /// <summary>
        /// Starting date of the reservation
        /// </summary>
        public DateTime ReservationFrom { get; set; }

        /// <summary>
        /// Ending date of the reservation
        /// </summary>
        public DateTime ReservationTo { get; set; }

        /// <summary>
        /// Indicator whether this reservation is still active
        /// </summary>
        public bool IsActive { get; set; }
    }
}