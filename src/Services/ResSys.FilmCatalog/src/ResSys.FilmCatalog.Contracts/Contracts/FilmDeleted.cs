using System;

namespace ResSys.FilmCatalog.Contracts
{
    public class FilmDeleted
    {
        public FilmDeleted(Guid itemId)
        {
            this.ItemId = itemId;
        }
        public FilmDeleted()
        {
        }

        public Guid ItemId { get; set; }
    }
}