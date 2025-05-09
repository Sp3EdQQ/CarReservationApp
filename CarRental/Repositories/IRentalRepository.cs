﻿using Projekt_strona.Models;
using System.Collections.Generic;

namespace Projekt_strona.Repositories
{
    public interface IRentalRepository
    {
        IEnumerable<Rental> GetAllRentals();
        Rental GetRentalById(int id);
        void AddRental(Rental rental);
        void UpdateRental(Rental rental);
        void DeleteRental(int id);
    }
}