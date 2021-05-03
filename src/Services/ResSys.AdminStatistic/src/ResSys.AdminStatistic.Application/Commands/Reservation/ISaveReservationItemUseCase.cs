
using System;
using System.Threading.Tasks;

namespace ResSys.AdminStatistic.Application.Commands.Reservations
{
    /// <summary>
    /// Saves item of a reservation
    /// </summary>
    public interface ISaveReservationItemUseCase
    {
        Task Execute(Guid reservationItemId, Guid reservationId, int amount, Guid? bookId, Guid? filmId);
    }
}