using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt_strona.Models;
using Projekt_strona.Repositories;

namespace Projekt_strona.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly ICarRepository _carRepository;
        public AdminController(ICustomerRepository customerRepository, IRentalRepository rentalRepository, ICarRepository carRepository)
        {
            _customerRepository = customerRepository;
            _rentalRepository = rentalRepository;
            _carRepository = carRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminCustomers()
        {   
            var customers = _customerRepository.GetAllCustomers();
            return View(customers);
        }
        [HttpGet]
        public IActionResult AdminRentals()
        {
            var rentals = _rentalRepository.GetAllRentals();
            return View(rentals);
        }
        [HttpGet]
        public IActionResult AdminCars()
        {
            var cars = _carRepository.GetAllCars();
            return View(cars);
        }


    }
}

