using System;

namespace ResSys.Logistic.Contracts
{
    public record SupplyBook(Guid TransactionId, string IBAN, string Name, string Description, 
        int NumberOfPages, int Amount, DateTimeOffset PublishDate, int AuthorRegNum);
    public record SupplyFilm(Guid TransactionId, string EIDR, string Name, string Description, 
        int Rating, int Amount, DateTimeOffset PublishDate, int AuthorRegNum);
}
