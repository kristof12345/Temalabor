using DTO;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Models
{
    public class ReserveContext : DbContext
    {
        public ReserveContext(DbContextOptions<ReserveContext> options)
            : base(options)
        {
        }

        public DbSet<Flight_DTO> DTOFlights { get; set; }
    }
}
