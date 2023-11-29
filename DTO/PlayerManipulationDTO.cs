namespace Euroleague.DTO
{
    public class PlayerManipulationDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public string Nationality { get; set; }
        public IFormFile? Picture { get; set; }
        public int TeamId { get; set; }
    }
}
