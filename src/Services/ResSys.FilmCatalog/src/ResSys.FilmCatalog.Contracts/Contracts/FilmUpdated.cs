using System;

namespace ResSys.FilmCatalog.Contracts
{
    public class FilmUpdated
    {
        public FilmUpdated(Guid itemId, string name, string description, int rating, DateTimeOffset publishDate)
        {
            this.ItemId = itemId;
            this.Name = name;
            this.Description = description;
            this.Rating = rating;
            this.PublishDate = publishDate;
        }

        public FilmUpdated()
        {
        }

        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public DateTimeOffset PublishDate { get; set; }
    }
}