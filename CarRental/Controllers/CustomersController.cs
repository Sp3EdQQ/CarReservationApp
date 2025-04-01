using Microsoft.AspNetCore.Mvc;
using Projekt_strona.Models;
using Projekt_strona.Repositories;

namespace Projekt_strona.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepository.AddCustomer(customer);
                return RedirectToAction("Index", "Home");
            }
            return View(customer);
        }
    }
}