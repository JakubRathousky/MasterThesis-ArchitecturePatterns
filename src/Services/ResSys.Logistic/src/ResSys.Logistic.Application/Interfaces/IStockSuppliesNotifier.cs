using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSys.Logistic.Application.ViewModels;

namespace ResSys.Logistic.Application.Interfaces
{
    /// <summary>
    /// Notifier on stock supply change
    /// </summary>
    public interface IStockSuppliesNotifier
    {
        Task BookCreatedNotification(SupplyBookDto book);
        Task FilmCreatedNotification(SupplyFilmDto film);
    }
}