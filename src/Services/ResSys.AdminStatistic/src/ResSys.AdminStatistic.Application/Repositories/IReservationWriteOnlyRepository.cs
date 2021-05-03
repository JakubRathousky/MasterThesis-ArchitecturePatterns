using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Domain.Reservation;

namespace ResSys.AdminStatistic.Application.Repositories
{
    /// <summary>
    /// Repository for modifying reservations
    /// </summary>
    public interface IReservationWriteOnlyRepository
    {
        Task CreateAsync(Reservation entity);
        Task UpdateAsync(Reservation entity);
        Task RemoveAsync(Guid id);
    }
}
