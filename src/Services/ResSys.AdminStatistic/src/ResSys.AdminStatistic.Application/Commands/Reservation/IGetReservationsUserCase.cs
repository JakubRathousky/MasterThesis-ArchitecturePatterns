
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Application.Dtos;

namespace ResSys.AdminStatistic.Application.Commands.Reservations
{
    /// <summary>
    /// returns list of reservations
    /// </summary>
    public interface IGetReservationsUserCase
    {
        Task<List<ReservationDto>> Execute();
    }
}