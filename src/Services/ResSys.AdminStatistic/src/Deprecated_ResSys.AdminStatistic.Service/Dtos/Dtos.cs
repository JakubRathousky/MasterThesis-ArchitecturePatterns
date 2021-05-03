using System;
using System.Collections.Generic;

namespace ResSys.AdminStatistics.Service.Dtos
{
    public record ReservationItemDTO(Guid Id, Guid BookId, Guid FilmId, int Amount);
    public record ReservationDto(Guid Id, IEnumerable<ReservationItemDTO> Books, IEnumerable<ReservationItemDTO> Films, DateTimeOffset ReservationFronm, DateTimeOffset ReservationTo);
    public record CreateReservationDto(string Date, IEnumerable<ReservationItemDTO> Books, IEnumerable<ReservationItemDTO> Films);
}