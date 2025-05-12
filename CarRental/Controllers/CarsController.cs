using Microsoft.AspNetCore.Mvc;
using Projekt_strona.Models;
using Projekt_strona.Repositories;

namespace Projekt_strona.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarRepository _carRepository;

        public CarsController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
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
            if (ModelState.IsValid)
            {
                _carRepository.DeleteCar(id);
                TempData["Success"] = "Samochód został usunięty!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Nie można usunąć samochodu.";
            return RedirectToAction("Index");
        }

    }
}