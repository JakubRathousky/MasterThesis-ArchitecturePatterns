using System;

namespace ResSys.FilmCatalog.Contracts
{
    public class FilmAmountUpdated
    {
        public FilmAmountUpdated(Guid itemId, Guid transactionId, int amount)
        {
            this.ItemId = itemId;
            this.Amount = amount;
            this.TransactionId = transactionId;
        }
        public FilmAmountUpdated()
        {
        }

        public Guid ItemId { get; set; }
        public int Amount { get; set; }
        public Guid TransactionId { get; set; }
    }
}