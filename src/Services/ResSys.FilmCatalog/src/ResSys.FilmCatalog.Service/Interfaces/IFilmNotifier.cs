using System;
using System.Threading.Tasks;
using ResSys.Common;
using ResSys.FilmCatalog.Service.Entities;
using ResSys.FilmCatalog.Service.Interfaces;

namespace ResSys.FilmCatalog.Service.Interfaces
{
    public interface IFilmNotifier
    {

        Task FilmUpdatedNotification(Film film);
        Task FilmAmountUpdatedNotification(Guid filmId, Guid transactionId, int amount);
        Task FilmSupplyTransactionConfirmationNotification(Guid itemId, Guid transactionId);
        Task FilmCreatedNotification(Film film);
    }
}
