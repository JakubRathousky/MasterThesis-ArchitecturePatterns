using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Domain.Reservation;

namespace ResSys.AdminStatistic.Application.Repositories
{
    /// <summary>
    /// Repository for obtaining information on reservations
    /// </summary>
    public interface IReservationReadOnlyRepository
    {
        Task<IReadOnlyCollection<Reservation>> GetAllAsync(bool onlyActive = true);
        Task<Reservation> GetAsync(Guid id);
        Task<Reservation> GetOneAsync(Func<Reservation, bool> filter);
    }
}
