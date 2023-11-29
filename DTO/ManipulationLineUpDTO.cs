namespace Euroleague.DTO
{
    public class ManipulationLineUpDTO
    {
        public List<LineupPlayerDTO> HomePlayers { get; set; }
        public List<LineupPlayerDTO> GuestPlayers { get; set; }
        public int GameId { get; set; }
    }
}
