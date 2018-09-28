using System;
using DAL; //Ezt nem szabadna használni helyette DTO

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
