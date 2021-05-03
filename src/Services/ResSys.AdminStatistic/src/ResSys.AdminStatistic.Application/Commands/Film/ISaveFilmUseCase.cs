
using System;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Application.Dtos;
using ResSys.AdminStatistic.Application.Results.FilmCatalog;

namespace ResSys.AdminStatistic.Application.Commands.FilmCatalog
{
    /// <summary>
    /// Stores a film in the database
    /// </summary>
    public interface ISaveFilmUseCase
    {
        Task<FilmResult> Execute(CreateFilmDto dto);
    }
}