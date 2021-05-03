using System.Linq;
using ResSys.ReservationSystem.Service.Dtos;
using ResSys.ReservationSystem.Data.Entities;

namespace ResSys.ReservationSystem.Service.Extensions
{
    /// <summary>
    /// Entity-Domain mappers
    /// </summary>
    public static class Mappers
    {
        public static ReservationDto AsDto(this Reservation item)
        {
            if (item == null)
                return null;
            return new ReservationDto(item.Id, item.Books.Select(x => x.AsDto()), item.Films.Select(x => x.AsDto()), 
                item.ReservationFrom, item.ReservationTo, item.CreatedDate, item.IsActive);
        }

        public static ReservationItemDto AsDto(this ReservationItem item)
        {
            if (item == null)
                return null;
            return new ReservationItemDto(item.Id, item.BookId, item.FilmId, item.Amount);
        }
    }
}