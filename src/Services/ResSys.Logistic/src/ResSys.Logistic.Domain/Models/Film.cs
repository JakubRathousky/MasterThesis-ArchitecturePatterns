using System;
using ResSys.Common;

namespace ResSys.Logistic.Domain
{
    public class Film : IEntity
    {
        // Id of transaction used for service-to-service communication
        public Guid Id { get; set; }
        public string EIDR { get; set; }
        // Id of film in external service, proof of film acceptance by service
        public Guid FilmId { get; set; }
        public int AuthorRegNum { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int Amount { get; set; }
        public DateTimeOffset PublishDate { get; set; }
    }
}