using System;
using System.ComponentModel.DataAnnotations;

namespace ResSys.FilmCatalog.Service.Dtos
{
    public record FilmDto(Guid Id, string EIDR, string Name, string Description, int Rating, int Amount, DateTimeOffset PublishDate, DateTimeOffset CreatedDate, int AuthorRegNum, Guid AuthorId);
    public record CreateFilmDto(string EIDR, [Required] string Name, string Description, [Range(0, 10)] int Rating, DateTimeOffset PublishDate, int AuthorRegNum, int Amount);
    public record UpdateFilmDto([Required] string Name, string Description, int Rating, DateTimeOffset PublishDate, int AuthorRegNum);
}