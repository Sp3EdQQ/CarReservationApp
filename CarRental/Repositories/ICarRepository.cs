using Projekt_strona.Models;
using System.Collections.Generic;

namespace Projekt_strona.Repositories
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAllCars();
        Car GetCarById(int id);
        void AddCar(Car car);
        void UpdateCar(Car car);
        void DeleteCar(int id);
    }
}