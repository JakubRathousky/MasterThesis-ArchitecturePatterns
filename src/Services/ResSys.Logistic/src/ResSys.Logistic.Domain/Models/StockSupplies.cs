using System;
using System.Collections.Generic;
using ResSys.Common;

namespace ResSys.Logistic.Domain
{
    public class StockSupplies : IEntity
    {
        public Guid Id { get; set; }
        public IEnumerable<Film> Films { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public DateTimeOffset StorageDate { get; set; }
    }
}