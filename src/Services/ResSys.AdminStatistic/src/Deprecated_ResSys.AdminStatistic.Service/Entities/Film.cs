using System;
using ResSys.Common;

namespace ResSys.AdminStatistic.Service.Entities
{
    public class Film : IEntity
    {
        public Guid Id { get; set; }
        public string EIDR { get; set; }
        public Guid TransactionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public Guid AuthorId { get; set; }
        public int Amount { get; set; }

        public int AuthorRegNum { get; set; }
        public DateTime PublishDate { get; set; }
    }
}