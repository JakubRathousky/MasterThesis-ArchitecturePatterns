using System;
using System.Collections.Generic;
using ResSys.Common;

namespace ResSys.AdminStatistic.Service.Entities
{
    public class Reservation : IEntity
    {
        public Guid Id { get; set; }
        public ICollection<Film> Films { get; set; }
        public ICollection<Book> Books { get; set; }
        public DateTime ReservationFrom { get; set; }
        public DateTime ReservationTo { get; set; }
        public bool IsActive { get; set; }
    }
}