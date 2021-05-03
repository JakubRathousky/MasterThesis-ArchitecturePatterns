using System;
using System.Collections.Generic;
using ResSys.Common;

namespace ResSys.AdminStatistic.Infrastructure.EntityFrameworkDataAccess.Entities
{
    public class Reservation : IEntity
    {
        public Guid Id { get; set; }
        public ICollection<ReservationItem> Reservations { get; set; }
        public DateTime ReservationFrom { get; set; }
        public DateTime ReservationTo { get; set; }
        public bool IsActive { get; set; }
    }
}