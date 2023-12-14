namespace Euroleague.Models
{
    public class PlayerStatisticUpdate
    {
        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public StatisticType Type { get; set; }

        public int Value { get; set; }

        public bool IsHomeTeam { get; set; }
    }

    public enum StatisticType
    {
        fouls,
        points,
        rebounds,

    }
}
