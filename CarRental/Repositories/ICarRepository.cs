using Projekt_strona.Models;

namespace Projekt_strona.Repositories
{
    public interface ICarRepository
    {
        IQueryable<Car> GetAllCars();
        Car GetCarById(int id);
        void AddCar(Car car);
        void UpdateCar(Car car);
        void DeleteCar(int id);
    }
}
