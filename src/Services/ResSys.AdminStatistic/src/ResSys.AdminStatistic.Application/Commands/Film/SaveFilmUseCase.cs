
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Application.Dtos;
using ResSys.AdminStatistic.Application.Results.FilmCatalog;
using ResSys.AdminStatistic.Domain.FilmCatalog;
using ResSys.Common;

namespace ResSys.AdminStatistic.Application.Commands.FilmCatalog
{
    public sealed class SaveFilmUseCase : ISaveFilmUseCase
    {
        private readonly IFilmWriteOnlyRepository writeOnlyRepository;
        private readonly IFilmReadOnlyRepository readOnlyRepository;

        public SaveFilmUseCase(
            IFilmWriteOnlyRepository writeOnlyRepository,
            IFilmReadOnlyRepository readOnlyRepository)
        {
            this.writeOnlyRepository = writeOnlyRepository;
            this.readOnlyRepository = readOnlyRepository;
        }

        public async Task<FilmResult> Execute(CreateFilmDto dto)
        {
            var film = await readOnlyRepository.GetAsync(dto.EIDR);

            if (film != null)
                return null;

            film = new Film()
            {
                Id = dto.FilmId,
                EIDR = dto.EIDR,
                Name = dto.Name,
                Description = dto.Description,
                Rating = dto.Rating,
                AuthorId = dto.AuthorId,
                AuthorRegNum = dto.AuthorRegNum,
                Amount = dto.Amount,
                PublishDate = dto.PublishDate.DateTime
            };

            await writeOnlyRepository.CreateAsync(film);

            return new FilmResult(film);
        }
    }
}