using System;

namespace ResSys.FilmCatalog.Contracts
{
    public class FilmSupplyTransactionConfirmation
    {
        public FilmSupplyTransactionConfirmation(Guid itemId, Guid transactionId)
        {
            this.ItemId = itemId;
            this.TransactionId = transactionId;
        }
        public FilmSupplyTransactionConfirmation()
        {
        }

        public Guid ItemId { get; set; }
        public Guid TransactionId { get; set; }
    }
}