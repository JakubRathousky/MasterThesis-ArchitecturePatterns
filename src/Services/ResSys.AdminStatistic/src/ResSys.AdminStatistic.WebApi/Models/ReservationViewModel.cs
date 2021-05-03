using System;
using System.Collections.Generic;
using ResSys.AdminStatistic.Domain.FilmCatalog;

namespace ResSys.AdminStatistic.WebApi.Models
{
    /// <summary>
    /// Reservation view model
    /// </summary>

    public record ReservationViewModel(Guid ReservationId, DateTime ReservedFrom, DateTime ReservedTo, List<FilmViewModel> Films, List<BookViewModel> Books);
    public record FilmViewModel(string EIDR, string Name, Guid AuthorId, int AuthorRegNum, int Amount);
    public record BookViewModel(string IBAN, string Name, Guid AuthorId, int AuthorRegNum, int Amount);
}