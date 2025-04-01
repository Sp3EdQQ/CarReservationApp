using Projekt_strona.Models;
using System.Linq;

namespace Projekt_strona.Repositories
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
    }
}
