using Projekt_strona.Data;
using Projekt_strona.Models;
using System.Collections.Generic;
using System.Linq;

namespace Projekt_strona.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly CarRentalsContext _context;

        public RentalRepository(CarRentalsContext context)
        {
            _context = context;
        }

        public IEnumerable<Rental> GetAllRentals()
        {
            return _context.Rentals.ToList();
        }

        public Rental GetRentalById(int id)
        {
            return _context.Rentals.Find(id);
        }

        public void AddRental(Rental rental)
        {
            _context.Rentals.Add(rental);
            _context.SaveChanges();
        }

        public void UpdateRental(Rental rental)
        {
            _context.Rentals.Update(rental);
            _context.SaveChanges();
        }

        public void DeleteRental(int id)
        {
            var rental = _context.Rentals.Find(id);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
                _context.SaveChanges();
            }
        }
    }
}