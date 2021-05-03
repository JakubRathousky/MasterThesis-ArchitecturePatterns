using System;
using ResSys.Common;

namespace ResSys.AdminStatistic.Infrastructure.ServiceBus.DTO
{
    public class ReservationItem : IEntity
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid FilmId { get; set; }
        public int Amount { get; set; }
    }
}