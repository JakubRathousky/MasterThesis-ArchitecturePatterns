using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResSys.AdminStatistic.Application.Commands.FilmCatalog;
using ResSys.AdminStatistic.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ResSys.AdminStatistic.WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGetAllFilmsUseCase _userCase;

        public HomeController(ILogger<HomeController> logger, IGetAllFilmsUseCase userCase)
        {
            _logger = logger;
            _userCase = userCase;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
