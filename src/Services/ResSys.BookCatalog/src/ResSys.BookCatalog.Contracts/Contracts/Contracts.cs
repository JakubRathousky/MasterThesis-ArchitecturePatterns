using System;

namespace ResSys.BookCatalog.Contracts
{
    public record BookCreated(Guid TransactionId, string IBAN, Guid ItemId, string Name, string Description, int NumberOfPages, int Amount, int AuthorRegNum, DateTimeOffset PublishDate, Guid AuthorId);
    public record BookUpdated(Guid ItemId, string Name, string Description, int NumberOfPages, DateTimeOffset PublishDate);
    public record BookAmountUpdated(Guid ItemId, Guid TransactionId, int Amount);
    public record BookSupplyTransactionConfirmation(Guid ItemId, Guid TransactionId);

    public record BookDeleted(Guid ItemId);
}