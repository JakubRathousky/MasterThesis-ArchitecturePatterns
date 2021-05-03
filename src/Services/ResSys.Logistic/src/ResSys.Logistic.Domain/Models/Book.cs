using System;
using ResSys.Common;

namespace ResSys.Logistic.Domain
{
    public class Book : IEntity
    {
        // Id of transaction used for service to service communication
        public Guid Id { get; set; }
        public string IBAN { get; set; }
        // Id of book in external service, proof of book accaptance by service
        public Guid BookId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPages { get; set; }
        public int AuthorRegNum { get; set; }
        public int Amount { get; set; }

        public DateTimeOffset PublishDate { get; set; }
    }
}