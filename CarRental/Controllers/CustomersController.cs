﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt_strona.Models;
using Projekt_strona.Repositories;

namespace Projekt_strona.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IRentalRepository _rentalRepository;

        public CustomersController(ICustomerRepository customerRepository, IRentalRepository rentalRepository)
        {
            _customerRepository = customerRepository;
            _rentalRepository = rentalRepository;
        }

        // GET: /Customers/Index
        [AllowAnonymous]
        public IActionResult Index()
        {
            var customers = _customerRepository.GetAllCustomers();
            return View(customers);
        }

        // GET: /Customers/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepository.AddCustomer(customer);
                TempData["Success"] = "Klient został dodany!";
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        // GET: /Customers/Edit
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var customer = _customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                TempData["Error"] = "Klient nie istnieje.";
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // POST: /Customers/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepository.UpdateCustomer(customer);
                TempData["Success"] = "Klient został zaktualizowany!";
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: /Customers/Delete
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var customer = _customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                TempData["Error"] = "Klient nie istnieje.";
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // POST: /Customers/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                TempData["Error"] = "Klient nie istnieje.";
                return RedirectToAction("Index");
            }

            var activeRentals = _rentalRepository.GetAllRentals().Any(r => r.CustomerId == id && r.ReturnDate == null);
            if (activeRentals)
            {
                TempData["Error"] = "Nie można usunąć klienta z aktywnymi wypożyczeniami.";
                return RedirectToAction("Index");
            }

            _customerRepository.DeleteCustomer(id);
            TempData["Success"] = "Klient został usunięty!";
            return RedirectToAction("Index");
        }
    }
}