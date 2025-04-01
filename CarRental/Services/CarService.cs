using Projekt_strona.Models;
using Projekt_strona.Repositories;

namespace Projekt_strona.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public PaginatedList<Car> GetAllCars(int pageIndex, int pageSize)
        {
            var cars = _carRepository.GetAllCars();
            return PaginatedList<Car>.Create(cars, pageIndex, pageSize);
        }

        public Car GetCarById(int id)
        {
            return _carRepository.GetCarById(id);
        }

        public void AddCar(Car car)
        {
            _carRepository.AddCar(car);
        }

        public void UpdateCar(Car car)
        {
            _carRepository.UpdateCar(car);
        }

        public void DeleteCar(int id)
        {
            _carRepository.DeleteCar(id);
        }
    }
}