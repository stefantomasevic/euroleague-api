namespace Euroleague.DTO
{
    public class PlayerStatisticUpdateDTO
    {
        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public string Type { get; set; }

        public int Value { get; set; }

        public bool IsHomeTeam { get; set; }
    }

    
}
