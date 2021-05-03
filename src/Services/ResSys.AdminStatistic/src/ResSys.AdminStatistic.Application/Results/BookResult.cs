using System;
using ResSys.AdminStatistic.Domain.BookCatalog;

namespace ResSys.AdminStatistic.Application.Results.BookCatalog
{
    /// <summary>
    /// A create for a book to be sent to the viewmodel
    /// </summary>
    public class BookResult
    {
        public Guid Id { get; set; }
        public string IBAN { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPages { get; set; }
        public Guid AuthorId { get; set; }
        public int AuthorRegNum { get; set; }
        public int Amount { get; set; }
        public DateTime PublishDate { get; set; }

        public BookResult(Book book)
        {
            this.Id = book.Id;
            this.IBAN = book.IBAN;
            this.Name = book.Name;
            this.Description = book.Description;
            this.NumberOfPages = book.NumberOfPages;
            this.AuthorId = book.AuthorId;
            this.AuthorRegNum = book.AuthorRegNum;
            this.Amount = book.Amount;
            this.PublishDate = book.PublishDate;
        }
    }
}