using ResSys.FilmCatalog.Service.Dtos;
using ResSys.FilmCatalog.Service.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace ResSys.FilmCatalog.Service.Interfaces
{
    public interface IAuthorClient
    {
        Task<Guid?> GetAuthorIdAsync(int authorRegNum);
    }
}