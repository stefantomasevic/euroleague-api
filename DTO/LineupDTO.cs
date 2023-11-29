namespace Euroleague.DTO
{
    public class LineupDTO
    {

        public int GameId { get; set; }

        public DateTime Date { get; set; }

        public int HomeId { get; set; }

        public int GuestId { get; set; }

        public string HomeTeam { get; set; }

        public string GuestTeam { get; set; }

        public string HomeLogo { get; set; }

        public string GuestLogo { get; set; }

        public IEnumerable<LineupPlayerDTO> HomePlayers { get; set; }
        public IEnumerable<LineupPlayerDTO> GuestPlayers { get; set; }
    }
    
}
