using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class FlightContext : DbContext
    {     
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<PlaneType> PlaneTypes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=FlightDB;Trusted_Connection=True;ConnectRetryCount=0");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlaneType>().HasData(
                new
                {
                    flightID = (long)1,
                    planeTypeID = (long)1,
                    planeType = "Airbus A380"
                });

            modelBuilder.Entity<PlaneType>().HasData(
                new
                {
                    flightID = (long)2,
                    planeTypeID = (long)2,
                    planeType = "Boeing 747"
                });

            modelBuilder.Entity<PlaneType>().HasData(
                new
                {
                    flightID = (long)2,
                    planeTypeID = (long)2,
                    planeType = "Boeing 747"
                });
            modelBuilder.Entity<PlaneType>().HasData(
                new
                {
                    flightID = (long)2,
                    planeTypeID = (long)2,
                    planeType = "Antonov 124"
                });
           

            Seat seat1 = new Seat
            {
                seatID = (long)1,
                planeTypeID = (long)1,
                IsReserved = false,
                seatType = "fapados",
                price = 15000,
                Xcord = 1,
                Ycord = 1
            };
            Seat seat2 = new Seat
            {
                seatID = (long)2,
                planeTypeID = (long)1,
                IsReserved = false,
                seatType = "1. osztályú",
                price = 200000,
                Xcord = 20,
                Ycord = 20
            };
            modelBuilder.Entity<Seat>().HasData(seat1, seat2);

            Seat seat3 = new Seat
            {
                seatID = (long)3,
                planeTypeID = (long)2,
                IsReserved = false,
                seatType = "fapados",
                price = 15000,
                Xcord = 1,
                Ycord = 1
            };
            Seat seat4 = new Seat
            {
                seatID = (long)4,
                planeTypeID = (long)2,
                IsReserved = false,
                seatType = "1. osztályú",
                price = 200000,
                Xcord = 20,
                Ycord = 20
            };
            modelBuilder.Entity<Seat>().HasData(seat3, seat4);

            List<Seat> seatsList = new List<Seat>();
            seatsList.Add(seat1); seatsList.Add(seat2);

            List<Seat> seatsList2 = new List<Seat>();
            seatsList2.Add(seat3); seatsList2.Add(seat4);

            modelBuilder.Entity<Flight>().HasData(
                new
                {
                    flightID = (long)1,
                    businessID = (long)1,
                    planeTypeID = (long)1,
                    date = new DateTime(2018, 10, 4),
                    departure = "Delhi",
                    destination = "Budapest",
                    freeSeats = seatsList.Count,
                    numberofSeats = seatsList.Count,
                    status = "ok"
                });

            modelBuilder.Entity<Flight>().HasData(
                new
                {
                    flightID = (long)2,
                    businessID = (long)2,
                    planeTypeID = (long)2,
                    date = new DateTime(2018, 10, 5),
                    departure = "Moscow",
                    destination = "London",
                    freeSeats = seatsList2.Count,
                    numberofSeats = seatsList2.Count,
                    status = "ok"
                });
        }
    }
}
