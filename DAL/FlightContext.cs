using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;

namespace DAL
{
    public class FlightContext : DbContext
    {
        public DbSet<DAL.Flight> Flights { get; set; }
        public DbSet<DAL.Seat> Seats { get; set; }
        public DbSet<DAL.PlaneType> PlaneTypes { get; set; }
        public DbSet<DAL.Reservation> Reservations { get; set; }
        public DbSet<DAL.User> Users { get; set; }
        public DbSet<DAL.ReservationSeat> ReservationSeats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=FlightDB;Trusted_Connection=True;ConnectRetryCount=0");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DAL.User>().HasData(
                new
                {
                    userID = (long)1,
                    name = "Patyi Gábor",
                    userType = UserType.Customer,
                    password = "password",
                    isDeleted = false
                });

            modelBuilder.Entity<DAL.PlaneType>().HasData(
                new
                {
                    planeTypeID = (long)1,
                    planeType = "Airbus A380",
                    isDeleted = false
                });

            modelBuilder.Entity<DAL.PlaneType>().HasData(
                new
                {
                    planeTypeID = (long)2,
                    planeType = "Boeing 777",
                    isDeleted = false
                });

            modelBuilder.Entity<DAL.PlaneType>().HasData(
                new
                {
                    planeTypeID = (long)3,
                    planeType = "Boeing 747",
                    isDeleted = false
                });
            modelBuilder.Entity<DAL.PlaneType>().HasData(
                new
                {
                    planeTypeID = (long)4,
                    planeType = "Antonov 124",
                    isDeleted = false
                });


            DAL.Seat seat1 = new DAL.Seat
            {
                seatID = (long)1,
                planeTypeID = (long)1,
                seatType = DTO.SeatType.Normal,
                Xcord = 640,
                Ycord = 50,
                isDeleted = false
            };
            DAL.Seat seat2 = new DAL.Seat
            {
                seatID = (long)2,
                planeTypeID = (long)1,
                seatType = DTO.SeatType.Normal,
                Xcord = 640,
                Ycord = 100,
                isDeleted = false
            };
            modelBuilder.Entity<DAL.Seat>().HasData(seat1, seat2);

            DAL.Seat seat3 = new DAL.Seat
            {
                seatID = (long)3,
                planeTypeID = (long)2,
                seatType = DTO.SeatType.Normal,
                Xcord = 625,
                Ycord = 200,
                isDeleted = false
            };
            DAL.Seat seat4 = new DAL.Seat
            {
                seatID = (long)4,
                planeTypeID = (long)2,
                seatType = DTO.SeatType.Normal,
                Xcord = 655,
                Ycord = 200,
                isDeleted = false
            };
            modelBuilder.Entity<DAL.Seat>().HasData(seat3, seat4);

            modelBuilder.Entity<DAL.Flight>().HasData(
                new
                {
                    flightID = (long)1,
                    planeTypeID = (long)1,
                    date = new DateTime(2018, 10, 4),
                    departure = "Delhi",
                    destination = "Budapest",
                    status = "ok",
                    normalPrice = 10000,
                    firstClassPrice = 100000,
                    isDeleted = false
                });

            modelBuilder.Entity<DAL.Flight>().HasData(
                new
                {
                    flightID = (long)2,
                    planeTypeID = (long)2,
                    date = new DateTime(2018, 10, 5),
                    departure = "Moscow",
                    destination = "London",
                    status = "ok",
                    normalPrice = 20000,
                    firstClassPrice = 200000,
                    isDeleted = false
                });          
        }
    }
}
