
using System;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Application.Results.Statistics;

namespace ResSys.AdminStatistic.Application.Commands.Statistics
{
    /// <summary>
    /// Gets a list of all reservations per month
    /// </summary>
    public interface IGetReservationsPerMonthUseCase
    {
        Task<MonthReservationResult> Execute();
    }
}