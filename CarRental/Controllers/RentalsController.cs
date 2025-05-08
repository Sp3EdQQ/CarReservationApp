using Microsoft.AspNetCore.Mvc;
using Projekt_strona.Models;
using Projekt_strona.Repositories;
using System.Linq;

namespace Projekt_strona.Controllers
{
    public class RentalsController : Controller
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;

        public RentalsController(IRentalRepository rentalRepository, ICarRepository carRepository, ICustomerRepository customerRepository)
        {
            _rentalRepository = rentalRepository;
            _carRepository = carRepository;
            _customerRepository = customerRepository;
        }

        // GET: /Rentals/Index
        public IActionResult Index()
        {
            var rentals = _rentalRepository.GetAllRentals();
            ViewBag.AvailableCars = _carRepository.GetAllCars().ToList();
            ViewBag.Customers = _customerRepository.GetAllCustomers().ToList();
            return View(rentals);
        }

        // GET: /Rentals/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AvailableCars = _carRepository.GetAllCars().Where(c => c.IsAvailable).ToList();
            ViewBag.Customers = _customerRepository.GetAllCustomers().ToList();
            return View();
        }

        // POST: /Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Rental rental)
        {
            Console.WriteLine($"Rental Create POST: CarId={rental.CarId}, CustomerId={rental.CustomerId}, RentalDate={rental.RentalDate}");
            if (ModelState.IsValid)
            {
                var car = _carRepository.GetCarById(rental.CarId);
                if (car == null || !car.IsAvailable)
                {
                    ModelState.AddModelError("CarId", "Wybrany samochód nie jest dostępny.");
                    ViewBag.AvailableCars = _carRepository.GetAllCars().Where(c => c.IsAvailable).ToList();
                    ViewBag.Customers = _customerRepository.GetAllCustomers().ToList();
                    return View(rental);
                }

                car.IsAvailable = false;
                _carRepository.UpdateCar(car);
                _rentalRepository.AddRental(rental);
                TempData["Success"] = "Rezerwacja została dodana!";
                return RedirectToAction("Index");
            }

            // Debugowanie błędów walidacji
            foreach (var entry in ModelState)
            {
                Console.WriteLine($"Field: {entry.Key}");
                foreach (var error in entry.Value.Errors)
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
            }

            ViewBag.AvailableCars = _carRepository.GetAllCars().Where(c => c.IsAvailable).ToList();
            ViewBag.Customers = _customerRepository.GetAllCustomers().ToList();
            return View(rental);
        }

        // GET: /Rentals/Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var rental = _rentalRepository.GetRentalById(id);
            if (rental == null)
            {
                TempData["Error"] = "Rezerwacja nie istnieje.";
                return RedirectToAction("Index");
            }
            ViewBag.AvailableCars = _carRepository.GetAllCars().Where(c => c.IsAvailable || c.Id == rental.CarId).ToList();
            ViewBag.Customers = _customerRepository.GetAllCustomers().ToList();
            return View(rental);
        }

        // POST: /Rentals/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Rental rental)
        {
            if (ModelState.IsValid)
            {
                var car = _carRepository.GetCarById(rental.CarId);
                if (car == null)
                {
                    ModelState.AddModelError("CarId", "Wybrany samochód nie istnieje.");
                    ViewBag.AvailableCars = _carRepository.GetAllCars().Where(c => c.IsAvailable || c.Id == rental.CarId).ToList();
                    ViewBag.Customers = _customerRepository.GetAllCustomers().ToList();
                    return View(rental);
                }

                _rentalRepository.UpdateRental(rental);
                TempData["Success"] = "Rezerwacja została zaktualizowana!";
                return RedirectToAction("Index");
            }

            ViewBag.AvailableCars = _carRepository.GetAllCars().Where(c => c.IsAvailable || c.Id == rental.CarId).ToList();
            ViewBag.Customers = _customerRepository.GetAllCustomers().ToList();
            return View(rental);
        }
    }
}