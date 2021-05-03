using System;
using System.ComponentModel.DataAnnotations;

namespace ResSys.BookCatalog.Service.Dtos
{
    public record BookDto(Guid Id, string IBAN, string Name, string Description, int NumberOfPages, int Amount, DateTimeOffset PublishDate, DateTimeOffset CreatedDate);
    public record CreateBookDto([Required] string Name, string IBAN, string Description, int NumberOfPages, int Amount, DateTimeOffset PublishDate);
    public record UpdateBookDto([Required] string Name, string IBAN, string Description, int NumberOfPages, int Amount, DateTimeOffset PublishDate);
}