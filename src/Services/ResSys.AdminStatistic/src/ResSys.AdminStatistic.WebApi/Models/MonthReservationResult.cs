using System;
using System.Collections.Generic;
using ResSys.AdminStatistic.Domain.FilmCatalog;

namespace ResSys.AdminStatistic.WebApi.Models
{
    /// <summary>
    /// A create for a month reservation to be sent to the viewmodel
    /// </summary>
    public class MonthReservationViewModel
    {
        public MonthReservationViewModel()
        {
            this.Count = new List<MonthReservationItem>();
        }
        public List<MonthReservationItem> Count { get; set; }
    }

    /// <summary>
    /// A create for a month reservation item to be sent to the viewmodel
    /// </summary>
    public class MonthReservationItem
    {
        public MonthReservationItem(int month, int number)
        {
            this.Month = month;
            this.Number = number;
        }
        public int Month { get; set; }
        public int Number { get; set; }
    }
}