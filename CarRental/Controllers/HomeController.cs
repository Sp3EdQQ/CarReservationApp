using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt_strona.Models;
using Projekt_strona.Models.ViewModels;
using Projekt_strona.Repositories;

namespace Projekt_strona.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;

        public HomeController(ICarRepository carRepository, ICustomerRepository customerRepository)
        {
            _carRepository = carRepository;
            _customerRepository = customerRepository;
        }
        [AllowAnonymous]
        public IActionResult Index(int carsPageIndex = 1, int carsPageSize = 5, int customerPageIndex = 1, int customerPageSize = 5)
        {
            var cars = _carRepository.GetAllCars();
            var paginatedCars = PaginatedList<Car>.Create(cars, carsPageIndex, carsPageSize);
            var customers = _customerRepository.GetAllCustomers();
            var paginatedCustomers = PaginatedList<Customer>.Create(customers, customerPageIndex, customerPageSize);

            var viewModel = new HomeViewModel
            {
                Cars = paginatedCars,
                Customers = paginatedCustomers
            };

            return View(viewModel);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}