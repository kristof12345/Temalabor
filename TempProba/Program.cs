using System;
using DAL;

namespace TempProba
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new FlightContext())
            {

                var std = new Flight();
                std.departure = "Los Angeles";

                context.Flights.Add(std);
                context.SaveChanges();
            }
        }
    }
}
