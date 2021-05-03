using System.Collections.Generic;
// using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using ResSys.AdminStatistic.Infrastructure.EntityFrameworkDataAccess.Entities;

namespace ResSys.AdminStatistic.Infrastructure.EntityFrameworkDataAccess
{
    public class Context : DbContext
    {
        public Context(DbContextOptions context) : base(context)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationItem> ReservedItems { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Book>()
                .ToTable("Book");

            modelBuilder.Entity<Entities.Film>()
                .ToTable("Film");

            modelBuilder.Entity<Entities.Reservation>()
                .ToTable("Reservation");

            modelBuilder.Entity<Entities.ReservationItem>()
                .ToTable("ReservationItem");

            modelBuilder.Entity<Entities.Transaction>()
                .ToTable("Transaction");
        }
    }
}