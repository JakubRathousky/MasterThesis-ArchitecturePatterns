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
using ResSys.AdminStatistic.Domain.FilmCatalog;
using ResSys.AdminStatistic.Infrastructure.EntityFrameworkDataAccess.Helpers;

namespace ResSys.AdminStatistic.Infrastructure.EntityFrameworkDataAccess.Repositories
{
    public class FilmRepository : IFilmReadOnlyRepository, IFilmWriteOnlyRepository
    {
        protected Context context;

        public FilmRepository(Context dataContext)
        {
            this.context = dataContext;
        }

        public async Task<IReadOnlyCollection<Film>> GetAllAsync()
        {
            var dbFilms = await context.Films.ToListAsync();
            return dbFilms.Select(film => film.AsDomain()).ToList();
        }

        public async Task<Film> GetAsync(Guid id)
        {
            var film = await context.Films.FindAsync(id);
            return film.AsDomain();
        }

        public async Task<Film> GetAsync(string EIDR)
        {
            var film = await context.Films.FirstOrDefaultAsync(x => x.EIDR == EIDR);
            if (film == null)
                return null;
            var model = film.AsDomain();
            return model;
        }

        public async Task<Film> GetAsync(Func<Film, bool> filter)
        {
            var films = (await context.Films.ToListAsync()).Select(film => film.AsDomain()).ToList().Where(filter);
            return films.SingleOrDefault();
        }

        public async Task CreateAsync(Film entity)
        {
            await context.Films.AddAsync(entity.AsEntity());
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Film entity)
        {
            Entities.Film film = entity.AsEntity();
            context.Films.Attach(film);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            Entities.Film entityToDelete = await context.Films.FindAsync(id);
            context.Films.Remove(entityToDelete);
            await context.SaveChangesAsync();
        }
    }
}