using Projekt_strona.Data;
using Projekt_strona.Models;
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

        public IQueryable<Rental> GetAllRentals()
        {
            return _context.Rentals;
        }

        public Rental GetRentalById(int id)
        {
            return _context.Rentals.FirstOrDefault(r => r.Id == id);
        }

        public void AddRental(Rental rental)
        {
            _context.Rentals.Add(rental);
            _context.SaveChanges();

            var car = _context.Cars.FirstOrDefault(c => c.Id == rental.CarId);
            if (car != null)
            {
                car.IsAvailable = false;
                _context.SaveChanges();
            }
        }

        public void UpdateRental(Rental rental)
        {
            _context.Rentals.Update(rental);
            _context.SaveChanges();
        }

        public void DeleteRental(int id)
        {
            var rental = _context.Rentals.FirstOrDefault(r => r.Id == id);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
                _context.SaveChanges();

                var car = _context.Cars.FirstOrDefault(c => c.Id == rental.CarId);
                if (car != null)
                {
                    car.IsAvailable = true;
                    _context.SaveChanges();
                }
            }
        }
    }
}
