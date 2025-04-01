using Microsoft.AspNetCore.Mvc;
using Projekt_strona.Data;
using Projekt_strona.Models;
using Projekt_strona.Repositories;

namespace Projekt_strona.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly CarRentalsContext _context;

        public RentalsController(ICarRepository carRepository, ICustomerRepository customerRepository, CarRentalsContext context)
        {
            _carRepository = carRepository;
            _customerRepository = customerRepository;
            _context = context;
        }

        public IActionResult Index()
        {
            var rentals = _context.Rentals.ToList();
            return View(rentals);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AvailableCars = _carRepository.GetAllCars().Where(c => c.IsAvailable).ToList();
            ViewBag.Customers = _customerRepository.GetAllCustomers().ToList();
            return View(new Rental { RentDate = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Rental rental)
        {
            if (ModelState.IsValid)
            {
                var car = _carRepository.GetCarById(rental.CarId);
                if (car != null && car.IsAvailable)
                {
                    car.IsAvailable = false;
                    _carRepository.UpdateCar(car);
                    rental.RentDate = DateTime.Now;
                    _context.Rentals.Add(rental);
                    _context.SaveChanges();
                    TempData["Success"] = "Samochód został wypożyczony!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Wybrany samochód nie jest dostępny.");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            ViewBag.AvailableCars = _carRepository.GetAllCars().Where(c => c.IsAvailable).ToList();
            ViewBag.Customers = _customerRepository.GetAllCustomers().ToList();
            return View(rental);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Return(int rentalId)
        {
            var rental = _context.Rentals.FirstOrDefault(r => r.Id == rentalId);
            if (rental != null && rental.ReturnDate == null)
            {
                rental.ReturnDate = DateTime.Now;
                var car = _carRepository.GetCarById(rental.CarId);
                if (car != null)
                {
                    car.IsAvailable = true;
                    _carRepository.UpdateCar(car);
                }
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}   