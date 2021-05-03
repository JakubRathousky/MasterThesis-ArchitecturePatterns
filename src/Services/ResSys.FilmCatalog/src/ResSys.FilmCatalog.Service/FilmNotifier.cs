using MassTransit;
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
using ResSys.FilmCatalog.Contracts;

namespace ResSys.FilmCatalog
{
    public class FilmNotifier : IFilmNotifier
    {
        private readonly IPublishEndpoint publishEndpoint;


        public FilmNotifier(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public async Task FilmUpdatedNotification(Film film)
        {
            await publishEndpoint.Publish(new FilmUpdated(film.Id, film.Name, film.Description, film.Rating, film.PublishDate));
        }
        public async Task FilmAmountUpdatedNotification(Guid filmId, Guid transactionId, int amount)
        {
            await publishEndpoint.Publish(new FilmAmountUpdated(filmId, transactionId, amount));
        }
        public async Task FilmSupplyTransactionConfirmationNotification(Guid itemId, Guid transactionId)
        {
            await publishEndpoint.Publish(new FilmSupplyTransactionConfirmation(itemId, transactionId));
        }
        public async Task FilmCreatedNotification(Film film)
        {
            await publishEndpoint.Publish(new FilmCreated(film.EIDR, film.Id, film.Name, film.Description, film.Rating, film.Amount, film.AuthorRegNum, film.PublishDate, film.AuthorId));
        }
    }
}
