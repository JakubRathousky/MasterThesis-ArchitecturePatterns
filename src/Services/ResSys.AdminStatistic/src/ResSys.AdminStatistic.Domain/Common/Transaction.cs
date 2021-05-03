using System;
using ResSys.Common;

namespace ResSys.AdminStatistic.Domain.Transaction
{
    /// <summary>
    /// Transaction entity of storing an item
    /// </summary>
    public class Transaction : IEntity
    {
        public Guid Id { get; set; }
        public Guid TransactionId { get; set; }
        public Guid ItemId { get; set; }
    }
}