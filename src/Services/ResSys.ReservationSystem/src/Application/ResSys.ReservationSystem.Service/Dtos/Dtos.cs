using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResSys.ReservationSystem.Service.Dtos
{
    public record ReservationItemDto(Guid Id, Guid BookId, Guid FilmId, int Amount);
    public record ReservationDto(Guid Id, IEnumerable<ReservationItemDto> Books, IEnumerable<ReservationItemDto> Films, 
        DateTimeOffset ReservationFrom, DateTimeOffset ReservationTo, DateTimeOffset CreatedDate, bool IsActive);
    public record CreateReservationDto(string Date, IEnumerable<ReservationItemDto> Books, IEnumerable<ReservationItemDto> Films);
}