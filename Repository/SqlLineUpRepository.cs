using Euroleague.Data;
using Euroleague.DTO;
using Euroleague.Models;
using Microsoft.EntityFrameworkCore;

namespace Euroleague.Repository
{
    public class SqlLineUpRepository : ILineUpRepository
    {
        private readonly ApplicationDbContext _context;

        public SqlLineUpRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateLineUpForGame(IEnumerable<LineupPlayerDTO> homePlayers, IEnumerable<LineupPlayerDTO> guestPlayers, int gameId)
        {
            List<Statistic> statistic = new List<Statistic>();
            foreach (var player in homePlayers)
            {
                statistic.Add(new Statistic
                {
                    GameId = gameId,
                    PlayerId = player.Id,
                    Points = 0,
                    Fouls = 0,
                    Rebounds = 0
                    // Dodaj ostale vrednosti koje želiš postaviti na početne vrednosti
                });
            }

            // Dodaj statistiku za gostujuće igrače
            foreach (var player in guestPlayers)
            {
                statistic.Add(new Statistic
                {
                    GameId = gameId,
                    PlayerId = player.Id,
                    Points = 0,
                    Fouls = 0,
                    Rebounds = 0
                    // Dodaj ostale vrednosti koje želiš postaviti na početne vrednosti
                });
            }

            await _context.Statistics.AddRangeAsync(statistic);
            await _context.SaveChangesAsync();
            return gameId;
        }
    }
}
