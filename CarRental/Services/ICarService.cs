using Projekt_strona.Models;

namespace Projekt_strona.Services
{
    public interface ICarService
    {
        PaginatedList<Car> GetAllCars(int pageIndex, int pageSize);
        Car GetCarById(int id);
        void AddCar(Car car);
        void UpdateCar(Car car);
        void DeleteCar(int id);
    }
}