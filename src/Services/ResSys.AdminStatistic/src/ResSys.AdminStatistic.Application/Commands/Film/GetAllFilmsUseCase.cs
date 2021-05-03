using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Domain.FilmCatalog;
using ResSys.Common;

namespace ResSys.AdminStatistic.Application.Commands.FilmCatalog
{
    public sealed class GetAllFilmsUseCase : IGetAllFilmsUseCase
    {
        private readonly IFilmReadOnlyRepository filmRepository;

        public GetAllFilmsUseCase(
         IFilmReadOnlyRepository filmRepository)
        {
            this.filmRepository = filmRepository;
        }

        public async Task<IEnumerable<Film>> Execute()
        {
            var films = await filmRepository.GetAllAsync();
            if (films == null)
                return new List<Film>();

            return films;
        }
    }
}
