using ResSys.FilmCatalog.Service.Dtos;
using ResSys.FilmCatalog.Service.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace ResSys.FilmCatalog.Service.Interfaces
{
    public interface IFilmService
    {
        Task<IEnumerable<FilmDto>> GetAllAsync();
        Task<FilmDto> FindFilmById(Guid id);
        Task UpdateFilmAsync(Guid id, UpdateFilmDto updateDTO);
        Task<FilmDto> SupplyFilmAsync(CreateFilmDto dto, Guid? transactionId);
    }
}