using System;
using ResSys.Common;

namespace ResSys.AdminStatistic.Domain.FilmCatalog
{
    /// <summary>
    /// Film entity
    /// </summary>
    public class Film : IEntity
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Universal identifier
        /// </summary>
        public string EIDR { get; set; }

        /// <summary>
        /// Film title
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Film description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Film rating
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Id of the author
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Amount of films in stock
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Registration number of the author
        /// </summary>
        public int AuthorRegNum { get; set; }

        /// <summary>
        /// Date of publishing
        /// </summary>
        public DateTime PublishDate { get; set; }
    }
}