using System;
using ResSys.AdminStatistic.Domain.FilmCatalog;

namespace ResSys.AdminStatistic.Application.Results.FilmCatalog
{
    /// <summary>
    /// A create for a film to be sent to the viewmodel
    /// </summary>
    public class FilmResult
    {
        public Guid Id { get; set; }
        public string EIDR { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public Guid AuthorId { get; set; }
        public int AuthorRegNum { get; set; }
        public int Amount { get; set; }
        public DateTime PublishDate { get; set; }

        public FilmResult(Film film)
        {
            this.Id = film.Id;
            this.EIDR = film.EIDR;
            this.Name = film.Name;
            this.Description = film.Description;
            this.Rating = film.Rating;
            this.AuthorId = film.AuthorId;
            this.AuthorRegNum = film.AuthorRegNum;
            this.Amount = film.Amount;
            this.PublishDate = film.PublishDate;
        }
    }
}