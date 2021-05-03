using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Domain.Reservation;

namespace ResSys.AdminStatistic.Application.Repositories
{
    /// <summary>
    /// Repository for modifying reservation items
    /// </summary>
    public interface IReservationItemWriteOnlyRepository
    {
        Task CreateBookReservationAsync(Guid id, Guid reservationId, Guid bookId, int amount);
        Task CreateFilmReservationAsync(Guid id, Guid reservationId, Guid filmId, int amount);
        Task RemoveAsync(Guid id);
    }
}
