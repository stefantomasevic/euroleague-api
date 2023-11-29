using Euroleague.DTO;
using Euroleague.Models;

namespace Euroleague.Repository
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllGames();
        Task<Game> GetGameById(int id);
        Task<Game> CreateGame(Game game);
        Task<Game> UpdateGame(Game game);
        Task DeleteGame(int id);
        Task<Game> GameDetails(int id);

        Task<Game> GamePlayers(int gameId);
    }
}
