using Euroleague.DTO;
using Euroleague.Models;

namespace Euroleague.Repository
{
    public interface ILineUpRepository
    {
        Task <int> CreateLineUpForGame(IEnumerable<LineupPlayerDTO> homePlayers, IEnumerable<LineupPlayerDTO> guestPlayers, int gameId);
    }
}
