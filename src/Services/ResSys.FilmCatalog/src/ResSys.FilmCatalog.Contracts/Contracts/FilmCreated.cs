using System;

namespace ResSys.FilmCatalog.Contracts
{
    public class FilmCreated
    {
        public FilmCreated(string EIDR, Guid itemId, string name, string description, int rating, int amount, int authorRegNum, DateTimeOffset publishDate, Guid authorId)
        {
            this.EIDR = EIDR;
            this.ItemId = itemId;
            this.Name = name;
            this.Description = description;
            this.Rating = rating;
            this.Amount = amount;
            this.PublishDate = publishDate;
            this.AuthorRegNum = authorRegNum;
            this.AuthorId = authorId;
        }
        public FilmCreated()
        {
        }
        public Guid TransactionId { get; set; }
        public string EIDR { get; set; }
        public Guid ItemId { get; set; }
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int Amount { get; set; }
        public int AuthorRegNum { get; set; }
        public DateTimeOffset PublishDate { get; set; }

    }
}