using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResSys.ReservationSystem.Data.Entities;
using ResSys.ReservationSystem.Service.Dtos;
using ResSys.ReservationSystem.Service.Extensions;
using ResSys.ReservationSystem.Contracts;
using ResSys.ReservationSystem.Service.Interfaces;

namespace ResSys.ReservationSystem.Service
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IReservationNotifier msgNotifier;

        public ReservationService(IReservationRepository reservationRepository, IReservationNotifier msgNotifier)
        {
            this.reservationRepository = reservationRepository;
            this.msgNotifier = msgNotifier;

            if (this.reservationRepository == null)
                throw new ArgumentNullException("Missing registered reference for IReservationRepository");
            if (this.msgNotifier == null)
                throw new ArgumentNullException("Missing registered reference for IReservationNotifier");
        }

        public async Task<IEnumerable<ReservationDto>> GetReservationsAsync()
        {
            var items = (await reservationRepository.GetAllAsync())
                .Select(item => item.AsDto());

            return items;
        }

        public async Task<ReservationDto> GetReservationByIdAsync(Guid id)
        {
            var item = (await reservationRepository.GetAsync(id)).AsDto();
            return item;
        }

        public async Task<ReservationDto> SaveReservationAsync(CreateReservationDto createReservationDto)
        {
            Reservation item = new Reservation
            {
                Films = createReservationDto.Films.Select(x => new Data.Entities.ReservationItem { Id = new Guid(), Amount = x.Amount, FilmId = x.FilmId }),
                Books = createReservationDto.Books.Select(x => new Data.Entities.ReservationItem { Id = new Guid(), Amount = x.Amount, BookId = x.BookId }),
                ReservationTo = DateTimeOffset.Parse(createReservationDto.Date),
                ReservationFrom = DateTimeOffset.UtcNow,
                CreatedDate = DateTimeOffset.UtcNow,
                IsActive = true
            };

            await reservationRepository.CreateAsync(item);

            var reservationDTO = new ReservationDto(item.Id,
                 item.Books.Select(x => new ReservationItemDto(x.Id, x.BookId, x.FilmId, x.Amount)),
                 item.Films.Select(x => new ReservationItemDto(x.Id, x.BookId, x.FilmId, x.Amount)),
                 item.ReservationFrom,
                 item.ReservationTo,
                 item.CreatedDate,
                 item.IsActive
            );

            await msgNotifier.ReservationCreatedNotification(reservationDTO);

            return reservationDTO;
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = (await reservationRepository.GetAsync(id));

            if (item == null)
                throw new ArgumentException();

            item.IsActive = false;
            await reservationRepository.UpdateAsync(item);

            await msgNotifier.ReservationDeactivateNotification(item.Id);
        }

        public async Task<int> GetAmountOfReservedBookAsync(Guid id, DateTimeOffset date)
        {
            var items = (await reservationRepository.GetAllAsync(t => t.IsActive && t.Books.Any(b => b.BookId == id)));

            var bookReservations = items.SelectMany(x => x.Books.Where(b => b.BookId == id));
            int amount = 0;

            foreach (var item in bookReservations)
            {
                amount += item.Amount;
            }

            return amount;
        }

        public async Task<int> GetAmountOfReservedFilmAsync(Guid id, DateTimeOffset date)
        {
            var items = (await reservationRepository.GetAllAsync(t => t.IsActive && t.Films.Any(b => b.FilmId == id)));

            var filmReservations = items.SelectMany(x => x.Films.Where(b => b.FilmId == id));

            int amount = 0;

            foreach (var item in filmReservations)
            {
                amount += item.Amount;
            }

            return amount;
        }
    }
}
