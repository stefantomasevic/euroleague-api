using Euroleague.Repository;
using Microsoft.AspNetCore.SignalR;

namespace Euroleague.Hubs
{
    public class GameHubService:Hub
    {
        private readonly IGameRepository _gameRepository;
        public GameHubService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task GetStatistic(int gameId)
        {
            var gameDetails=await _gameRepository.GameDetails(gameId);

            await Clients.All.SendAsync("ReceiveStatistic", gameDetails);
        }
    }
}
