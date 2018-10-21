﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(FlightContext))]
    [Migration("20181008171349_CreateFlightDB")]
    partial class CreateFlightDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Flight", b =>
                {
                    b.Property<long>("flightID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("businessID");

                    b.Property<DateTime>("date");

                    b.Property<string>("departure");

                    b.Property<string>("destination");

                    b.Property<int>("freeSeats");

                    b.Property<int>("numberofSeats");

                    b.Property<long>("planeTypeID");

                    b.Property<string>("status");

                    b.HasKey("flightID");

                    b.HasIndex("planeTypeID");

                    b.ToTable("Flights");

                    b.HasData(
                        new { flightID = 1L, businessID = 1L, date = new DateTime(2018, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), departure = "Delhi", destination = "Budapest", freeSeats = 2, numberofSeats = 2, planeTypeID = 1L, status = "ok" },
                        new { flightID = 2L, businessID = 2L, date = new DateTime(2018, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), departure = "Moscow", destination = "London", freeSeats = 2, numberofSeats = 2, planeTypeID = 2L, status = "ok" }
                    );
                });

            modelBuilder.Entity("DAL.PlaneType", b =>
                {
                    b.Property<long>("planeTypeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("planeType");

                    b.HasKey("planeTypeID");

                    b.ToTable("PlaneTypes");

                    b.HasData(
                        new { planeTypeID = 1L, planeType = "Airbus A380" },
                        new { planeTypeID = 2L, planeType = "Boeing 777" },
                        new { planeTypeID = 3L, planeType = "Boeing 747" },
                        new { planeTypeID = 4L, planeType = "Antonov 124" }
                    );
                });

            modelBuilder.Entity("DAL.Reservation", b =>
                {
                    b.Property<long>("reservationID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("flightID");

                    b.Property<long?>("seatID");

                    b.Property<string>("user");

                    b.HasKey("reservationID");

                    b.HasIndex("flightID");

                    b.HasIndex("seatID");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("DAL.Seat", b =>
                {
                    b.Property<long>("seatID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsReserved");

                    b.Property<int>("Xcord");

                    b.Property<int>("Ycord");

                    b.Property<long>("businessID");

                    b.Property<long>("flightID");

                    b.Property<long>("planeTypeID");

                    b.Property<int>("price");

                    b.Property<string>("seatType");

                    b.HasKey("seatID");

                    b.HasIndex("planeTypeID");

                    b.ToTable("Seats");

                    b.HasData(
                        new { seatID = 1L, IsReserved = false, Xcord = 1, Ycord = 1, businessID = 1L, flightID = 1L, planeTypeID = 1L, price = 15000, seatType = "fapados" },
                        new { seatID = 2L, IsReserved = false, Xcord = 20, Ycord = 20, businessID = 2L, flightID = 1L, planeTypeID = 1L, price = 200000, seatType = "1. osztályú" },
                        new { seatID = 3L, IsReserved = false, Xcord = 1, Ycord = 1, businessID = 3L, flightID = 2L, planeTypeID = 2L, price = 15000, seatType = "fapados" },
                        new { seatID = 4L, IsReserved = false, Xcord = 20, Ycord = 20, businessID = 4L, flightID = 2L, planeTypeID = 2L, price = 200000, seatType = "1. osztályú" }
                    );
                });

            modelBuilder.Entity("DAL.User", b =>
                {
                    b.Property<long>("userID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("name");

                    b.Property<string>("password");

                    b.Property<int>("userType");

                    b.HasKey("userID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAL.Flight", b =>
                {
                    b.HasOne("DAL.PlaneType", "planeType")
                        .WithMany()
                        .HasForeignKey("planeTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.Reservation", b =>
                {
                    b.HasOne("DAL.Flight", "flight")
                        .WithMany()
                        .HasForeignKey("flightID");

                    b.HasOne("DAL.Seat", "seat")
                        .WithMany()
                        .HasForeignKey("seatID");
                });

            modelBuilder.Entity("DAL.Seat", b =>
                {
                    b.HasOne("DAL.PlaneType", "planeType")
                        .WithMany()
                        .HasForeignKey("planeTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
