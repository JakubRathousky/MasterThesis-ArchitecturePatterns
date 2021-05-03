using System;
using System.Collections.Generic;
using ResSys.Common;

namespace ResSys.ReservationSystem.Data.Entities
{
    public class Reservation : IEntity
    {
        public Guid Id { get; set; }
        public IEnumerable<ReservationItem> Films { get; set; }
        public IEnumerable<ReservationItem> Books { get; set; }
        public DateTimeOffset ReservationFrom { get; set; }
        public DateTimeOffset ReservationTo { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}