using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Domain.FilmCatalog;

namespace ResSys.AdminStatistic.Application.Repositories
{
    /// <summary>
    /// Repository for obtaining films
    /// </summary>
    public interface IFilmReadOnlyRepository
    {
        Task<IReadOnlyCollection<Film>> GetAllAsync();
        Task<Film> GetAsync(Guid id);
        Task<Film> GetAsync(string EIDR);
        Task<Film> GetAsync(Func<Film, bool> filter);
    }
}
