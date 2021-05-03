
using System;
using System.Threading.Tasks;

namespace ResSys.AdminStatistic.Application.Commands.Reservations
{
    /// <summary>
    /// Saves a reservation
    /// </summary>
    public interface IDeleteReservationUseCase
    {
        Task Execute(Guid reservationId);
    }
}