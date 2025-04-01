namespace Projekt_strona.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime RentDate { get; set; }      // Zamiast RentalDate
        public DateTime? ReturnDate { get; set; }   // Nullable, bo zwrot jest opcjonalny
    }
}