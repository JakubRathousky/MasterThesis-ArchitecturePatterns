
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Application.Results.Statistics;
using ResSys.AdminStatistic.Domain.Reservation;
using ResSys.Common;

namespace ResSys.AdminStatistic.Application.Commands.Statistics
{
    public sealed class GetReservationsPerMonthUseCase : IGetReservationsPerMonthUseCase
    {
        private readonly IReservationReadOnlyRepository readOnlyRepository;

        public GetReservationsPerMonthUseCase(
            IReservationReadOnlyRepository readOnlyRepository)
        {
            this.readOnlyRepository = readOnlyRepository;
        }

        public async Task<MonthReservationResult> Execute()
        {
            var reservation = await readOnlyRepository.GetAllAsync(false);

            MonthReservationResult result = new MonthReservationResult();

            // Count number of created reservations by each month
            for (int i = 1; i <= 12; i++)
            {
                var reservationsThisMonth = reservation.Where(x => x.ReservationFrom.Month == i).Count();
                result.Count.Add(new MonthReservationItem(i, reservationsThisMonth));
            }

            return result;
        }
    }
}