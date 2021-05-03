using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Domain.FilmCatalog;

namespace ResSys.AdminStatistic.Application.Repositories
{
    /// <summary>
    /// Repository for modifying films
    /// </summary>
    public interface IFilmWriteOnlyRepository
    {
        Task CreateAsync(Film entity);
        Task UpdateAsync(Film entity);
        Task RemoveAsync(Guid id);
    }
}
