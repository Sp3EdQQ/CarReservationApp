using Projekt_strona.Models;

namespace Projekt_strona.Models.ViewModels
{
    public class HomeViewModel
    {
        public PaginatedList<Car> Cars { get; set; }
        public PaginatedList<Customer> Customers { get; set; }
    }
}