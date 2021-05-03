using System.Threading.Tasks;
using System;
using MassTransit;
using ResSys.Logistic.Contracts;
using ResSys.Common;
using ResSys.FilmCatalog.Service.Entities;
using ResSys.FilmCatalog.Service.Clients;
using ResSys.FilmCatalog.Contracts;
using ResSys.FilmCatalog.Service.Dtos;
using ResSys.FilmCatalog.Service.Interfaces;


namespace ResSys.FilmCatalog.Service.Consumers
{
    /// <summary>
    /// Consumer that handles a message of SupplyFilm
    /// </summary>
    public class SupplyFilmConsumer : IConsumer<SupplyFilm>
    {
        private readonly IFilmService filmService;

        public SupplyFilmConsumer(IFilmService filmService)
        {
            this.filmService = filmService;
        }

        public async Task Consume(ConsumeContext<SupplyFilm> context)
        {
            var msg = context.Message;

            CreateFilmDto dto = new CreateFilmDto(msg.EIDR, msg.Name, msg.Description, msg.Rating, msg.PublishDate, msg.AuthorRegNum, msg.Amount);

            await filmService.SupplyFilmAsync(dto, msg.TransactionId);
        }
    }
}