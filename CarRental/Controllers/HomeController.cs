using Microsoft.AspNetCore.Mvc;
using Projekt_strona.Models;
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

        public IActionResult Index(int pageIndex = 1, int pageSize = 5)
        {
            var cars = _carRepository.GetAllCars();
            var paginatedCars = PaginatedList<Car>.Create(cars, pageIndex, pageSize);
            var customers = _customerRepository.GetAllCustomers().ToList();

            ViewBag.Customers = customers;
            return View(paginatedCars);
        }
    }
}