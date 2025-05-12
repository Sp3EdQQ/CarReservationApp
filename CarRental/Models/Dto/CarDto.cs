namespace Projekt_strona.Models.Dto
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public bool IsAvailable { get; set; }
    }
}