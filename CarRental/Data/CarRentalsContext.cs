using Microsoft.EntityFrameworkCore;
using Projekt_strona.Models;

namespace Projekt_strona.Data
{
    public class CarRentalsContext : DbContext
    {
        public CarRentalsContext(DbContextOptions<CarRentalsContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
    }
}