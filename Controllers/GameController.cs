using AutoMapper;
using Euroleague.Data;
using Euroleague.DTO;
using Euroleague.Models;
using Euroleague.Repository;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using SQLitePCL;

namespace Euroleague.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : Controller
    {
        private readonly IGameRepository _repository;
        private readonly IMapper _mapper;
        public GameController(IGameRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
        {
            var gamesDTO = _mapper.Map<IEnumerable<GameDTO>>(await _repository.GetAllGames());


            return Ok(gamesDTO);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDetailsDTO>> GetGame(int id)
        {
            var game = await _repository.GameDetails(id);
            var gameDetailsDTO = _mapper.Map<GameDetailsDTO>(game);

            return Ok(gameDetailsDTO);

        }

        [HttpGet("{id}/players")]
        public async Task<ActionResult<LineupDTO>> GetGamePlayers(int id)
        {
            var game = await _repository.GamePlayers(id);
            var gameDetailsDTO = _mapper.Map<LineupDTO>(game);

            return Ok(gameDetailsDTO);




        }
    }
}
