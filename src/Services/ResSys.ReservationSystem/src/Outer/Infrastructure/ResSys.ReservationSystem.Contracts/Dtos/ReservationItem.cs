using System;

namespace ResSys.ReservationSystem.Contracts.Dtos
{
    public record ReservationItem(Guid Id, Guid? BookId, Guid? FilmdId, int Amount);
}