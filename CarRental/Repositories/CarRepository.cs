using Microsoft.EntityFrameworkCore;
using Projekt_strona.Data;
using Projekt_strona.Models;

namespace Projekt_strona.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentalsContext _context;

        public CarRepository(CarRentalsContext context)
        {
            _context = context;
        }

        public IQueryable<Car> GetAllCars()
        {
            return _context.Cars.AsQueryable();
        }

        public Car GetCarById(int id)
        {
            return _context.Cars.Find(id);
        }

        public void AddCar(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
        }

        public void UpdateCar(Car car)
        {
            _context.Cars.Update(car);
            _context.SaveChanges();
        }

        public void DeleteCar(int id)
        {
            var car = _context.Cars.Find(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
            }
        }
    }
}