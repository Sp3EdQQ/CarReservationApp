namespace Projekt_strona.Models.ViewModels
{
    public class RentalViewModel
    {
        public int Id { get; set; }
        public string CarMake { get; set; }
        public string CustomerFirstName { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}