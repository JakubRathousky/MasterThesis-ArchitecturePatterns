using System;
using ResSys.Common;

namespace ResSys.BookCatalog.Service.Entities
{
    public class Book : IEntity
    {
        public Guid Id { get; set; }
        public string IBAN { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPages { get; set; }
        public Guid AuthorId { get; set; }
        public int AuthorRegNum { get; set; }
        public int Amount { get; set; }
        public DateTimeOffset PublishDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}