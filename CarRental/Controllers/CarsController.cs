using Microsoft.AspNetCore.Mvc;
using Projekt_strona.Models;
using Projekt_strona.Repositories;

namespace Projekt_strona.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IRentalRepository _rentalRepository;

        public CarsController(ICarRepository carRepository, IRentalRepository rentalRepository)
        {
            _carRepository = carRepository;
            _rentalRepository = rentalRepository;
        }

        // GET: /Cars/Index
        public IActionResult Index()
        {
            var cars = _carRepository.GetAllCars();
            return View(cars);
        }

        // GET: /Cars/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                _carRepository.AddCar(car);
                TempData["Success"] = "Samochód został dodany!";
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: /Cars/Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var car = _carRepository.GetCarById(id);
            if (car == null)
            {
                TempData["Error"] = "Samochód nie istnieje.";
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // POST: /Cars/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                _carRepository.UpdateCar(car);
                TempData["Success"] = "Samochód został zaktualizowany!";
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: /Cars/Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var car = _carRepository.GetCarById(id);
            if (car == null)
            {
                TempData["Error"] = "Samochód nie istnieje.";
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // POST: /Cars/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Sprawdź, czy samochód nie ma aktywnych wypożyczeń
            var activeRentals = _rentalRepository.GetAllRentals().Any(r => r.CarId == id && r.ReturnDate == null);
            if (activeRentals)
            {
                TempData["Error"] = "Nie można usunąć samochodu z aktywnymi wypożyczeniami.";
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                _carRepository.DeleteCar(id);
                TempData["Success"] = "Samochód został usunięty!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Podczas usuwania samochodu wystąpił błąd.";
            return RedirectToAction("Index");

        }

    }
}