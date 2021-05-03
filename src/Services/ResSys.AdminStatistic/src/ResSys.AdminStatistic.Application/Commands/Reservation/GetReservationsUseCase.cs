

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Application.Dtos;
using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Domain.Reservation;
using ResSys.Common;

namespace ResSys.AdminStatistic.Application.Commands.Reservations
{
    sealed class GetReservationsUserCase : IGetReservationsUserCase
    {
        private readonly IReservationItemWriteOnlyRepository writeOnlyRepository;
        private readonly IReservationReadOnlyRepository reservationReadOnlyRepository;
        private readonly IReservationItemReadOnlyRepository readOnlyRepository;

        public GetReservationsUserCase(
            IReservationItemWriteOnlyRepository writeOnlyRepository,
            IReservationReadOnlyRepository reservationReadOnlyRepository,
            IReservationItemReadOnlyRepository readOnlyRepository)
        {
            this.writeOnlyRepository = writeOnlyRepository;
            this.reservationReadOnlyRepository = reservationReadOnlyRepository;
            this.readOnlyRepository = readOnlyRepository;
        }

        public async Task<List<ReservationDto>> Execute()
        {
            var reservations = await reservationReadOnlyRepository.GetAllAsync();

            List<ReservationDto> reservationDtoList = new List<ReservationDto>();

            foreach (var reservation in reservations)
            {
                List<FilmDto> films = new List<FilmDto>();
                List<BookDto> books = new List<BookDto>();

                foreach (var film in reservation.Films)
                {
                    films.Add(new FilmDto(film.EIDR, film.Name, film.AuthorId, film.AuthorRegNum, film.Amount));
                }
                foreach (var book in reservation.Books)
                {
                    books.Add(new BookDto(book.IBAN, book.Name, book.AuthorId, book.AuthorRegNum, book.Amount));
                }

                ReservationDto reservationDto = new ReservationDto(
                    reservation.Id, reservation.ReservationFrom, reservation.ReservationTo, films, books
                );
                reservationDtoList.Add(reservationDto);
            }
            return reservationDtoList;
        }
    }
}