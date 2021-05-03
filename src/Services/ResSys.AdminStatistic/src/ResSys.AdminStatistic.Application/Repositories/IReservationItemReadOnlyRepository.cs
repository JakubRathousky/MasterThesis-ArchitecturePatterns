using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Domain.Reservation;

namespace ResSys.AdminStatistic.Application.Repositories
{
    /// <summary>
    /// Repository for obtaining information on reservation items
    /// </summary>
    public interface IReservationItemReadOnlyRepository
    {
        Task<bool> Exists(Guid id);
    }
}
