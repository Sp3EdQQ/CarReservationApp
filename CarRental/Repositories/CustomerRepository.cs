﻿using Projekt_strona.Data;
using Projekt_strona.Models;
using System.Linq;

namespace Projekt_strona.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CarRentalsContext _context;

        public CustomerRepository(CarRentalsContext context)
        {
            _context = context;
        }

        public IQueryable<Customer> GetAllCustomers()
        {
            return _context.Customers;
        }

        public Customer GetCustomerById(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }
    }
}
