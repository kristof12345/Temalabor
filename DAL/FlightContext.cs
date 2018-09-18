using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class FlightContext: DbContext
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Seat> Seats { get; set; }
        //public DbSet<ReserveSeat> ReserveSeats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=FlightDB;Trusted_Connection=True;");
        }
    }
}
