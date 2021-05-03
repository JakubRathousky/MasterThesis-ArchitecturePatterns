using System;
// using System.Data.Entity;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Infrastructure.EntityFrameworkDataAccess.Entities;

namespace ResSys.AdminStatistic.Infrastructure.EntityFrameworkDataAccess.Repositories
{
    public class ReservationItemRepository : IReservationItemReadOnlyRepository, IReservationItemWriteOnlyRepository
    {
        protected Context context;


        public ReservationItemRepository(Context dataContext)
        {
            this.context = dataContext;
        }

        public async Task CreateBookReservationAsync(Guid id, Guid reservationId, Guid bookId, int amount)
        {
            var entity = new ReservationItem() { Id = id, ReservationId = reservationId, BookId = bookId, Amount = amount };

            await context.ReservedItems.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task CreateFilmReservationAsync(Guid id, Guid reservationId, Guid filmId, int amount)
        {
            var entity = new ReservationItem() { Id = id, ReservationId = reservationId, FilmId = filmId, Amount = amount };

            await context.ReservedItems.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await context.ReservedItems.AnyAsync(x => x.Id == id);
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}