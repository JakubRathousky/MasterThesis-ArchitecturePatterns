
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Domain.Reservation;
using ResSys.Common;

namespace ResSys.AdminStatistic.Application.Commands.Reservations
{
    sealed class SaveReservationItemUseCase : ISaveReservationItemUseCase
    {
        private readonly IReservationItemWriteOnlyRepository writeOnlyRepository;
        private readonly IReservationReadOnlyRepository reservationReadOnlyRepository;
        private readonly IReservationItemReadOnlyRepository readOnlyRepository;

        public SaveReservationItemUseCase(
            IReservationItemWriteOnlyRepository writeOnlyRepository,
            IReservationReadOnlyRepository reservationReadOnlyRepository,
            IReservationItemReadOnlyRepository readOnlyRepository)
        {
            this.writeOnlyRepository = writeOnlyRepository;
            this.reservationReadOnlyRepository = reservationReadOnlyRepository;
            this.readOnlyRepository = readOnlyRepository;
        }

        public async Task Execute(Guid reservationItemId, Guid reservationId, int amount, Guid? bookId, Guid? filmId)
        {
            var reservation = await reservationReadOnlyRepository.GetAsync(reservationId);

            if (reservation == null || await readOnlyRepository.Exists(reservationItemId))
                throw new Exception($"Reservation does not exists or item with specified Id: {reservationItemId} already exists");

            if (bookId.HasValue)
                await writeOnlyRepository.CreateBookReservationAsync(reservationItemId, reservationId, bookId.Value, amount);
            if (filmId.HasValue)
                await writeOnlyRepository.CreateFilmReservationAsync(reservationItemId, reservationId, filmId.Value, amount);
        }
    }
}