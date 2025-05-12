using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projekt_strona.Models;
using Projekt_strona.Models.Identity;

namespace Projekt_strona.Data
{
    public class CarRentalsContext : IdentityDbContext<ApplicationUser>
    {
        public CarRentalsContext(DbContextOptions<CarRentalsContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Customer> Customers { get; set; }

    }
}