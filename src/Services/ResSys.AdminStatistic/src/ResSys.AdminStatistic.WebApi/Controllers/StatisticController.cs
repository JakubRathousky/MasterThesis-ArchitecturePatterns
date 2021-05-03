using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResSys.AdminStatistic.Application.Commands.Reservations;
using ResSys.AdminStatistic.Application.Commands.Statistics;
using ResSys.AdminStatistic.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ResSys.AdminStatistic.WebApi.Controllers
{
    [Route("statistic")]
    public class StatisticController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGetReservationsPerMonthUseCase distributionUseCase;
        private readonly IGetReservationsUserCase reservationListUseCase;
        public StatisticController(ILogger<HomeController> logger, IGetReservationsPerMonthUseCase distributionUseCase, IGetReservationsUserCase reservationListUseCase)
        {
            _logger = logger;
            this.distributionUseCase = distributionUseCase;
            this.reservationListUseCase = reservationListUseCase;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/statistic/monthDistribution")]
        public async Task<IActionResult> GetReservationMonthDates()
        {
            var data = await distributionUseCase.Execute();
            MonthReservationViewModel model = new MonthReservationViewModel();
            model.Count = data.Count.Select(x => new MonthReservationItem(x.Month, x.Number)).ToList();
            return View(model);
        }
        [Route("/statistic/reservations")]
        public async Task<IActionResult> GetReservations()
        {
            var reservationDtos = await reservationListUseCase.Execute();

            List<ReservationViewModel> reservationWMList = new List<ReservationViewModel>();

            foreach (var reservation in reservationDtos)
            {
                List<FilmViewModel> films = new List<FilmViewModel>();
                List<BookViewModel> books = new List<BookViewModel>();

                foreach (var film in reservation.Films)
                {
                    films.Add(new FilmViewModel(film.EIDR, film.Name, film.AuthorId, film.AuthorRegNum, film.Amount));
                }
                foreach (var book in reservation.Books)
                {
                    books.Add(new BookViewModel(book.IBAN, book.Name, book.AuthorId, book.AuthorRegNum, book.Amount));
                }

                ReservationViewModel reservationWM = new ReservationViewModel(
                    reservation.ReservationId, reservation.ReservedFrom, reservation.ReservedTo, films, books
                );
                reservationWMList.Add(reservationWM);
            }
            return View(reservationWMList);
        }
    }
}
