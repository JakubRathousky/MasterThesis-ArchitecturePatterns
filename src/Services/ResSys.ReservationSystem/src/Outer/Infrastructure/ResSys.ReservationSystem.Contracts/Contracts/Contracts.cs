using System;
using System.Collections.Generic;

using ResSys.ReservationSystem.Contracts.Dtos;

namespace ResSys.ReservationSystem.Contracts
{
    public record ReservationCreated(
        Guid Id,
        IEnumerable<ReservationItem> Books,
        IEnumerable<ReservationItem> Films,
        DateTimeOffset ReservationFrom,
        DateTimeOffset ReservationTo);
    public record ReservationUpdated(Guid id, DateTimeOffset ReservationFrom, DateTimeOffset ReservationTo);
    public record ReservationDeleted(Guid Id);
}