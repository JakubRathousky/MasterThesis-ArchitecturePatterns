using System;
using ResSys.Common;

namespace ResSys.AdminStatistic.Infrastructure.EntityFrameworkDataAccess.Entities
{
    public class Transaction : IEntity
    {
        public Guid Id { get; set; }
        public Guid TransactionId { get; set; }
        public Guid ItemId { get; set; }
    }
}