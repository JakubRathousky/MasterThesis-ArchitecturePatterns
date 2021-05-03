using System;
using ResSys.Common;

namespace ResSys.ReservationSystem.Data.Entities
{
    public class ReservationItem : IEntity
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid FilmId { get; set; }
        public int Amount { get; set; }
    }
}