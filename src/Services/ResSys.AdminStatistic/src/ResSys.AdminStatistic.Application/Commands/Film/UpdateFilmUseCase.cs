
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Domain.FilmCatalog;
using ResSys.Common;

namespace ResSys.AdminStatistic.Application.Commands.FilmCatalog
{
    public sealed class UpdateFilmAmountUseCase : IUpdateFilmAmountUseCase
    {
        private readonly IFilmWriteOnlyRepository writeOnlyRepository;
        private readonly IFilmReadOnlyRepository readOnlyRepository;

        public UpdateFilmAmountUseCase(
            IFilmWriteOnlyRepository writeOnlyRepository,
            IFilmReadOnlyRepository readOnlyRepository)
        {
            this.writeOnlyRepository = writeOnlyRepository;
            this.readOnlyRepository = readOnlyRepository;
        }

        public async Task Execute(Guid filmId, int amount)
        {
            var film = await readOnlyRepository.GetAsync(filmId);

            if (film == null)
                throw new FilmNotFoundException($"The film with Id {filmId} does not exist.");

            film.Amount = amount;
            await writeOnlyRepository.UpdateAsync(film);
        }
    }
}
