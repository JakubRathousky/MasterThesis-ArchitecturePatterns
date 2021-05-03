using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Domain.FilmCatalog;

namespace ResSys.AdminStatistic.Application.Commands.FilmCatalog
{
    /// <summary>
    /// Gets list of all films
    /// </summary>
    public interface IGetAllFilmsUseCase
    {
        Task<IEnumerable<Film>> Execute();
    }
}
