using FluentValidation;
using Projekt_strona.Models;
using Projekt_strona.Repositories;
using System;

namespace Projekt_strona.Models.Validators
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator(ICarRepository carRepository, ICustomerRepository customerRepository)
        {
            RuleFor(r => r.CarId)
                .GreaterThan(0)
                .WithMessage("Wybierz samochód.")
                .Must(carId =>
                {
                    var car = carRepository.GetCarById(carId);
                    return car != null;
                })
                .WithMessage("Wybrany samochód nie istnieje.")
                .Must(carId =>
                {
                    var car = carRepository.GetCarById(carId);
                    return car != null && car.IsAvailable;
                })
                .WithMessage("Wybrany samochód nie jest dostępny.");

            RuleFor(r => r.CustomerId)
                .GreaterThan(0)
                .WithMessage("Wybierz klienta.")
                .Must(customerId => customerRepository.GetCustomerById(customerId) != null)
                .WithMessage("Wybrany klient nie istnieje.");

            RuleFor(r => r.RentalDate)
                .NotEmpty()
                .WithMessage("Data wypożyczenia jest wymagana.")
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Data wypożyczenia nie może być w przeszłości.");
        }
    }
}