using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Domain.FilmCatalog;

namespace ResSys.AdminStatistic.Application.Commands.FilmCatalog
{
    /// <summary>
    /// Updates amount of films in stock
    /// </summary>
    public interface IUpdateFilmAmountUseCase
    {
        Task Execute(Guid filmId, int amount);
    }
}
