using AutoMapper;
using Euroleague.DTO;
using Euroleague.Models;
using Euroleague.Repository;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Euroleague.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LineupController : Controller
    {

        private readonly IMapper _mapper;
        private readonly ILineUpRepository _repository;
        public LineupController( IMapper mapper, ILineUpRepository repository)
        {
           
            _mapper = mapper;
            _repository = repository;
        }
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    return 1;
        //}
        [HttpPost]
        public async Task<ActionResult> CreateLineUp(ManipulationLineUpDTO manLinDTO)
        {
            //dodavanje igraca odnosno postave koja igra utakmicu, stavicemo da u tabeli budu sve nulee kad admin odabere postavu
            try
            {
                var lineup = _mapper.Map<LineupDTO>(manLinDTO);
                var lineupId = await _repository.CreateLineUpForGame(lineup.HomePlayers, lineup.GuestPlayers, lineup.GameId);

                return Ok(lineupId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Došlo je do greške prilikom dodavanja igrača.");
            }
        }
    }
}
