using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
// using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ResSys.Common;
using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Domain.Reservation;
using ResSys.AdminStatistic.Infrastructure.EntityFrameworkDataAccess.Helpers;


namespace ResSys.AdminStatistic.Infrastructure.EntityFrameworkDataAccess.Repositories
{
    public class ReservationRepository : IReservationReadOnlyRepository, IReservationWriteOnlyRepository
    {
        protected Context context;


        public ReservationRepository(Context dataContext)
        {
            this.context = dataContext;
        }

        public async Task<IReadOnlyCollection<Reservation>> GetAllAsync(bool onlyActive = true)
        {
            var dbReservations = await context.Reservations.Where(x => !onlyActive || x.IsActive).ToListAsync();
            var reservations = new List<Reservation>();

            foreach (var dbReservation in dbReservations)
            {
                var domainReservation = dbReservation.AsDomain();
                await this.FillBooksAndFilmsAsync(domainReservation);
                reservations.Add(domainReservation);
            }
            return reservations;
        }

        public async Task<Reservation> GetAsync(Guid id)
        {
            var reservation = (await context.Reservations.FindAsync(id)).AsDomain();
            await this.FillBooksAndFilmsAsync(reservation);
            return reservation;
        }

        public async Task<Reservation> GetOneAsync(Func<Reservation, bool> filter)
        {
            var dbReservations = (await context.Reservations.ToListAsync())
            .Select(reservation => reservation.AsDomain());

            var reservations = new List<Reservation>();
            foreach (var reservation in dbReservations)
            {
                await this.FillBooksAndFilmsAsync(reservation);

                reservations.Add(reservation);
            }
            reservations.Where(filter);
            return reservations.SingleOrDefault();
        }

        public async Task CreateAsync(Reservation entity)
        {
            await context.Reservations.AddAsync(entity.AsEntity());
            context.SaveChanges();
        }

        public async Task UpdateAsync(Reservation entity)
        {
            var reservation = context.Reservations.SingleOrDefault(x => x.Id == entity.Id);

            if (reservation != null)
            {
                reservation.IsActive = entity.IsActive;
                context.Entry(reservation).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(Guid id)
        {
            Entities.Reservation entityToDelete = await context.Reservations.FindAsync(id);
            context.Reservations.Remove(entityToDelete);
            await context.SaveChangesAsync();
        }

        private async Task FillBooksAndFilmsAsync(Reservation reservation)
        {
            if (reservation?.Id != null)
            {
                var bookIds = await context.ReservedItems.Where(x => x.ReservationId == reservation.Id && x.BookId != null).Select(x => x.BookId).ToListAsync();
                var filmIds = await context.ReservedItems.Where(x => x.ReservationId == reservation.Id && x.FilmId != null).Select(x => x.FilmId).ToListAsync();

                var books = await context.Books.Where(x => bookIds.Contains(x.Id)).ToListAsync();
                var films = await context.Films.Where(x => filmIds.Contains(x.Id)).ToListAsync();

                reservation.Books = books.Select(x => x.AsDomain());
                reservation.Films = films.Select(x => x.AsDomain());
            }
        }
    }
}