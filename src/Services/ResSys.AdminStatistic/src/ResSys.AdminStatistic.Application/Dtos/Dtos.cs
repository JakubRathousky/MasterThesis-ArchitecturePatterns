using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResSys.AdminStatistic.Application.Dtos
{
    public record CreateFilmDto(Guid FilmId, string EIDR, string Name, string Description,
            int Rating, Guid AuthorId, int AuthorRegNum, int Amount, DateTimeOffset PublishDate);
    public record CreateBookDto(Guid BookId, string IBAN, string Name,
            string Description, int NumberOfPages, Guid AuthorId,
            int AuthorRegNum, int Amount, DateTimeOffset PublishDate);
    public record FilmDto(string EIDR, string Name, Guid AuthorId, int AuthorRegNum, int Amount);
    public record BookDto(string IBAN, string Name, Guid AuthorId, int AuthorRegNum, int Amount);
    public record ReservationDto(Guid ReservationId, DateTime ReservedFrom, DateTime ReservedTo, List<FilmDto> Films, List<BookDto> Books);

}