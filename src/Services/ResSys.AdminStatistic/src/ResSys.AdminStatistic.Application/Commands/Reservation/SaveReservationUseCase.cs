
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Domain.Reservation;
using ResSys.Common;

namespace ResSys.AdminStatistic.Application.Commands.Reservations
{
    sealed class SaveReservationUseCase : ISaveReservationUseCase
    {
        private readonly IReservationWriteOnlyRepository writeOnlyRepository;
        private readonly IReservationReadOnlyRepository readOnlyRepository;

        public SaveReservationUseCase(
            IReservationWriteOnlyRepository writeOnlyRepository,
            IReservationReadOnlyRepository readOnlyRepository)
        {
            this.writeOnlyRepository = writeOnlyRepository;
            this.readOnlyRepository = readOnlyRepository;
        }

        public async Task Execute(Guid reservationId, DateTimeOffset reservationFrom, DateTimeOffset reservationTo, bool isActive)
        {
            var reservation = await readOnlyRepository.GetAsync(reservationId);

            if (reservation != null)
                return;

            reservation = new Reservation()
            {
                Id = reservationId,
                ReservationFrom = reservationFrom.DateTime,
                ReservationTo = reservationTo.DateTime,
                IsActive = isActive
            };

            await writeOnlyRepository.CreateAsync(reservation);
        }
    }
}