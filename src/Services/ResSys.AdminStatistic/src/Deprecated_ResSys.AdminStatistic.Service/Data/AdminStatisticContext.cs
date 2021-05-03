using System.Collections.Generic;
// using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

using ResSys.AdminStatistic.Service.Entities;
namespace ResSys.AdminStatistic.Service.Data
{
    public class AdminStatisticContext : DbContext
    {
        public AdminStatisticContext(DbContextOptions<AdminStatisticContext> context) : base(context)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationItem> ReservedItems { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}