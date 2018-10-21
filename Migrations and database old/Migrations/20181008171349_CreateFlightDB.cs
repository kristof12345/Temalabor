using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class CreateFlightDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlaneTypes",
                columns: table => new
                {
                    planeTypeID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    planeType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaneTypes", x => x.planeTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    userType = table.Column<int>(nullable: false),
                    password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userID);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    flightID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    businessID = table.Column<long>(nullable: false),
                    planeTypeID = table.Column<long>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    departure = table.Column<string>(nullable: true),
                    destination = table.Column<string>(nullable: true),
                    freeSeats = table.Column<int>(nullable: false),
                    numberofSeats = table.Column<int>(nullable: false),
                    status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.flightID);
                    table.ForeignKey(
                        name: "FK_Flights_PlaneTypes_planeTypeID",
                        column: x => x.planeTypeID,
                        principalTable: "PlaneTypes",
                        principalColumn: "planeTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    seatID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    businessID = table.Column<long>(nullable: false),
                    flightID = table.Column<long>(nullable: false),
                    planeTypeID = table.Column<long>(nullable: false),
                    IsReserved = table.Column<bool>(nullable: false),
                    seatType = table.Column<string>(nullable: true),
                    price = table.Column<int>(nullable: false),
                    Xcord = table.Column<int>(nullable: false),
                    Ycord = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.seatID);
                    table.ForeignKey(
                        name: "FK_Seats_PlaneTypes_planeTypeID",
                        column: x => x.planeTypeID,
                        principalTable: "PlaneTypes",
                        principalColumn: "planeTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    reservationID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    user = table.Column<string>(nullable: true),
                    flightID = table.Column<long>(nullable: true),
                    seatID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.reservationID);
                    table.ForeignKey(
                        name: "FK_Reservations_Flights_flightID",
                        column: x => x.flightID,
                        principalTable: "Flights",
                        principalColumn: "flightID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Seats_seatID",
                        column: x => x.seatID,
                        principalTable: "Seats",
                        principalColumn: "seatID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "PlaneTypes",
                columns: new[] { "planeTypeID", "planeType" },
                values: new object[,]
                {
                    { 1L, "Airbus A380" },
                    { 2L, "Boeing 777" },
                    { 3L, "Boeing 747" },
                    { 4L, "Antonov 124" }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "flightID", "businessID", "date", "departure", "destination", "freeSeats", "numberofSeats", "planeTypeID", "status" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2018, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Delhi", "Budapest", 2, 2, 1L, "ok" },
                    { 2L, 2L, new DateTime(2018, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Moscow", "London", 2, 2, 2L, "ok" }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "seatID", "IsReserved", "Xcord", "Ycord", "businessID", "flightID", "planeTypeID", "price", "seatType" },
                values: new object[,]
                {
                    { 1L, false, 1, 1, 1L, 1L, 1L, 15000, "fapados" },
                    { 2L, false, 20, 20, 2L, 1L, 1L, 200000, "1. osztályú" },
                    { 3L, false, 1, 1, 3L, 2L, 2L, 15000, "fapados" },
                    { 4L, false, 20, 20, 4L, 2L, 2L, 200000, "1. osztályú" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_planeTypeID",
                table: "Flights",
                column: "planeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_flightID",
                table: "Reservations",
                column: "flightID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_seatID",
                table: "Reservations",
                column: "seatID");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_planeTypeID",
                table: "Seats",
                column: "planeTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "PlaneTypes");
        }
    }
}
