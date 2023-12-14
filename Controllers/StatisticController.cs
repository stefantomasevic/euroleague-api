using AutoMapper;
using Euroleague.DTO;
using Euroleague.Hubs;
using Euroleague.Models;
using Euroleague.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Euroleague.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        // GET: api/<StatisticController>

        private readonly IHubContext<GameHubService> _hubContext;

        private readonly IStatisticRepository _statisticRepository;

        private readonly IGameRepository _repository;
        private readonly IMapper _mapper;

        public StatisticController(IHubContext<GameHubService> hubContext, IStatisticRepository statisticRepository, IGameRepository repository,
         IMapper mapper)
        {
            _hubContext = hubContext;
            _statisticRepository = statisticRepository;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };

        }

        // GET api/<StatisticController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StatisticController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StatisticController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StatisticController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("UpdateStatistic")]
        public async Task<IActionResult> UpdateStatistic(PlayerStatisticUpdateDTO playerStatisticUpdateDTO)
        {

            var playerUpdatedStatistic = _mapper.Map<PlayerStatisticUpdate>(playerStatisticUpdateDTO);
            await _statisticRepository.UpdateStatistic(playerUpdatedStatistic);

            // Obaveštavanje klijenata putem huba
            //zapravo on direktno poziva metodu i vraca podatke ovde, ovde na primer vraca broj 3 svaki put kad se odradi update, znaci
            //treba da se napravi metoda koja kad se update odradi, odmah pozove da vrati azuriranooo

            //ovde sad pozovi repositorijum koji ce odraditi post metodu za update



            var game = await _repository.GameDetails(playerStatisticUpdateDTO.GameId);
            var gameDetailsDTO = _mapper.Map<GameDetailsDTO>(game);


            await _hubContext.Clients.All.SendAsync("ReceiveStatistic", gameDetailsDTO);


            return Ok();
        }
    }
}
