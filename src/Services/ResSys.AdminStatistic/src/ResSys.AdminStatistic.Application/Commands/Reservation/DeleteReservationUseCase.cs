
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Domain.Reservation;
using ResSys.Common;

namespace ResSys.AdminStatistic.Application.Commands.Reservations
{
    sealed class DeleteReservationUseCase : IDeleteReservationUseCase
    {
        private readonly IReservationWriteOnlyRepository writeOnlyRepository;
        private readonly IReservationReadOnlyRepository readOnlyRepository;

        public DeleteReservationUseCase(
            IReservationWriteOnlyRepository writeOnlyRepository,
            IReservationReadOnlyRepository readOnlyRepository)
        {
            this.writeOnlyRepository = writeOnlyRepository;
            this.readOnlyRepository = readOnlyRepository;
        }

        public async Task Execute(Guid reservationId)
        {
            var reservation = await readOnlyRepository.GetAsync(reservationId);

            if (reservation == null)
                return;

            reservation.IsActive = false;

            await writeOnlyRepository.UpdateAsync(reservation);
        }
    }
}