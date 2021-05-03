using System;
using System.Threading.Tasks;
using ResSys.Common;
using ResSys.ReservationSystem.Service.Dtos;
using ResSys.ReservationSystem.Service.Interfaces;

namespace ResSys.ReservationSystem.Service.Interfaces
{
    public interface IReservationNotifier
    {
        Task ReservationCreatedNotification(ReservationDto reservationDTO);
        Task ReservationDeactivateNotification(Guid id);
    }
}
