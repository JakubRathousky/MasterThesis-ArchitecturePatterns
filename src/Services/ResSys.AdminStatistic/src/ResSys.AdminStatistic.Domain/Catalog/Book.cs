using System;
using ResSys.Common;

namespace ResSys.AdminStatistic.Domain.BookCatalog
{
    /// <summary>
    /// Book entity
    /// </summary>
    public class Book : IEntity
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Book IBAN
        /// </summary>
        public string IBAN { get; set; }
        
        /// <summary>
        /// Book title
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Book description
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Number of printed pages
        /// </summary>
        public int NumberOfPages { get; set; }
        
        /// <summary>
        /// Identifier of the author
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Registration number of the author
        /// </summary>
        public int AuthorRegNum { get; set; }

        /// <summary>
        /// Amount of books of the same title in stock
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Date of publishing
        /// </summary>
        public DateTime PublishDate { get; set; }
    }
}