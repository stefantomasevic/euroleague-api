using Euroleague.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Euroleague.DTO
{
    public class GameManipulationDTO
    {
        public DateTime Date { get; set; }
        public int HomeId { get; set; }
        public int GuestId { get; set; }
        public int HomeScore { get; set; }
        public int GuestScore { get; set; }
    }
}
