using Microsoft.EntityFrameworkCore;
using Projekt_strona.Models;

namespace Projekt_strona.Data
{
    public class CarRentalsContext : DbContext
    {
        public CarRentalsContext(DbContextOptions<CarRentalsContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Dane testowe
            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, Make = "Volkswagen", Model = "Golf", Year = 2020, IsAvailable = true },
                new Car { Id = 2, Make = "Toyota", Model = "Corolla", Year = 2019, IsAvailable = true }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, FirstName = "Jan", LastName = "Kowalski", PhoneNumber = "123456789" },
                new Customer { Id = 2, FirstName = "Anna", LastName = "Nowak", PhoneNumber = "987654321" }
            );

            modelBuilder.Entity<Rental>().HasData(
                new Rental { Id = 1, CarId = 1, CustomerId = 1, RentalDate = new DateTime(2025, 5, 1), ReturnDate = null },
                new Rental { Id = 2, CarId = 2, CustomerId = 2, RentalDate = new DateTime(2025, 5, 2), ReturnDate = new DateTime(2025, 5, 5) }
            );
        }
    }
}