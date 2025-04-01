using Projekt_strona.Models;
using System.Linq;

namespace Projekt_strona.Repositories
{
    public interface IRentalRepository
    {
        IQueryable<Rental> GetAllRentals();
        Rental GetRentalById(int id);
        void AddRental(Rental rental);
        void UpdateRental(Rental rental);
        void DeleteRental(int id);
    }
}
