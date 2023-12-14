using AutoMapper;
using Euroleague.DTO;
using Euroleague.Models;
using Euroleague.Repository;
using Microsoft.AspNetCore.SignalR;

namespace Euroleague.Hubs
{
    public class GameHubService:Hub
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        public GameHubService(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task GetStatistic(int gameId)  
        {
            var gameDetails=await _gameRepository.GameDetails(gameId);
            var gameDetailsDto= _mapper.Map<GameDetailsDTO>(gameDetails);
            await Clients.All.SendAsync("ReceiveStatistic", gameDetailsDto);
        }
    }
}
