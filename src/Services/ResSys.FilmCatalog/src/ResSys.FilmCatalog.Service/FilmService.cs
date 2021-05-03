using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ResSys.FilmCatalog.Service.Interfaces;
using ResSys.FilmCatalog.Service.Entities;
using ResSys.FilmCatalog.Service.Dtos;
using ResSys.Common;
using ResSys.FilmCatalog.Service.Clients;
using ResSys.FilmCatalog.Service.Extensions;

namespace ResSys.FilmCatalog
{
    public class FilmService : IFilmService
    {

        private readonly IRepository<Transaction> transRepository;
        private readonly IRepository<Film> filmsRepository;
        private readonly IFilmNotifier notifier;
        private readonly AuthorClient authorClient;

        public FilmService(IRepository<Film> filmsRepository, IRepository<Transaction> transRepository, IFilmNotifier notifier, AuthorClient authorClient)
        {
            this.transRepository = transRepository;
            this.filmsRepository = filmsRepository;
            this.notifier = notifier;
            this.authorClient = authorClient;
        }

        public async Task<IEnumerable<FilmDto>> GetAllAsync()
        {
            var items = (await filmsRepository.GetAllAsync())
                .Select(item => item.AsDto());

            return items;
        }

        public async Task<FilmDto> FindFilmById(Guid id)
        {
            var item = (await filmsRepository.GetAsync(id)).AsDto();

            return item;
        }

        public async Task<FilmDto> SupplyFilmAsync(CreateFilmDto dto, Guid? transactionId)
        {
            Film item = new Film();
            try
            {
                var transaction = await transRepository.GetAsync(transactionId.HasValue ? transactionId.Value : Guid.Empty);

                if (transaction != null)
                    return null;

                // try to find film by unique identifier
                item = await filmsRepository.GetAsync(x => x.EIDR == dto.EIDR);

                // if film hasn't been created, we will create one, otherwise, we will increment the amount
                if (item == null)
                {
                    var authorId = await authorClient.GetAuthorIdAsync(dto.AuthorRegNum);

                    if (!authorId.HasValue)
                        return null;

                    item = new Film
                    {
                        Name = dto.Name,
                        EIDR = dto.EIDR,
                        Description = dto.Description,
                        Rating = dto.Rating,
                        AuthorRegNum = dto.AuthorRegNum,
                        PublishDate = dto.PublishDate,
                        AuthorId = authorId.Value,
                        Amount = dto.Amount
                    };
                    await filmsRepository.CreateAsync(item);
                    await notifier.FilmCreatedNotification(item);
                }
                else
                {
                    item.Amount += dto.Amount;

                    await filmsRepository.UpdateAsync(item);
                    await notifier.FilmAmountUpdatedNotification(item.Id, transactionId.Value, item.Amount);
                }

                if (transactionId.HasValue)
                {
                    transaction = new Transaction(transactionId.Value, item.Id);
                    await transRepository.CreateAsync(transaction);
                    await notifier.FilmSupplyTransactionConfirmationNotification(item.Id, transactionId.Value);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while proccesing supply request: " + ex.Message);
            }

            return item.AsDto();
        }

        public async Task UpdateFilmAsync(Guid id, UpdateFilmDto updateDTO)
        {
            var item = (await filmsRepository.GetAsync(id));

            if (item == null)
                return;

            item.Name = updateDTO.Name;
            item.Description = updateDTO.Description;
            item.PublishDate = updateDTO.PublishDate;
            item.Rating = updateDTO.Rating;

            await filmsRepository.UpdateAsync(item);

            await notifier.FilmUpdatedNotification(item);
        }
    }
}