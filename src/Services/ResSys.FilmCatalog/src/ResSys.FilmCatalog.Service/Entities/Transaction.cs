using System;
using ResSys.Common;

namespace ResSys.FilmCatalog.Service.Entities
{
    public class Transaction : IEntity
    {
        public Transaction(Guid transactionId, Guid itemId)
        {
            TransactionId = transactionId;
            ItemId = itemId;
        }

        public Guid Id { get; set; }
        public Guid TransactionId { get; set; }
        public Guid ItemId { get; set; }
    }
}