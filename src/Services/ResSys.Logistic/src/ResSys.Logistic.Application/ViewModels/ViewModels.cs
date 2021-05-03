using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ResSys.Logistic.Domain;

namespace ResSys.Logistic.Application.ViewModels
{
    public record SupplyDto(Guid Id, IEnumerable<SupplyFilmDto> Films, IEnumerable<SupplyBookDto> Books, DateTimeOffset StorageDate, Boolean Synchronized);
    public record SupplyFilmDto(Guid Id, string EIDR, Guid FilmId, int AuthorRegNum, string Name, string Description, int Rating, int Amount, DateTimeOffset PublishDate);
    public record SupplyBookDto(Guid Id, string IBAN, Guid BookId, int AuthorRegNum, string Name, string Description, int NumberOfPages, int Amount, DateTimeOffset PublishDate);
    public record CreateSupplyDto(IEnumerable<CreateSupplyFilmDto> Films, IEnumerable<CreateSupplyBookDto> Books);
    public record CreateSupplyFilmDto(string EIDR, int AuthorRegNum, string Name, string Description, int Rating, int Amount, DateTimeOffset PublishDate);
    public record CreateSupplyBookDto(string IBAN, int AuthorRegNum, string Name, string Description, int NumberOfPages, int Amount, DateTimeOffset PublishDate);
}