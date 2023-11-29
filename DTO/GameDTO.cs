using Euroleague.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Euroleague.DTO
{
    public class GameDTO
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string HomeTeam { get; set; }
        public string GuestTeam { get; set; }
        public int HomeScore { get; set; }
        public int GuestScore { get; set; }

        public string HomeLogo { get; set; }

        public string GuestLogo { get; set; }
    }
}
