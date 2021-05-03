
using System;
using System.Threading.Tasks;

namespace ResSys.AdminStatistic.Application.Commands.Reservations
{
    /// <summary>
    /// Saves a reservation
    /// </summary>
    public interface ISaveReservationUseCase
    {
        Task Execute(Guid reservationId, DateTimeOffset reservationFrom, DateTimeOffset reservationTo, bool isActive);
    }
}