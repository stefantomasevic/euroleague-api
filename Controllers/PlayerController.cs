using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Euroleague.Data;
using Euroleague.Models;
using Euroleague.Repository;
using Euroleague.DTO;
using AutoMapper;

namespace Euroleague.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPlayerRepository _repository;
        private readonly IMapper _mapper;

        public PlayerController(ApplicationDbContext context, IPlayerRepository repository, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Player
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerDTO>>> GetPlayers()
        {
            var players = await _repository.GetAllPlayers();

            if (players == null || !players.Any())
            {
                return NotFound();
            }

            var playersDTO = _mapper.Map<IEnumerable<PlayerDTO>>(players);

            return Ok(playersDTO);
        }

        // GET: api/Player/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
          if (_context.Players == null)
          {
              return NotFound();
          }
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // PUT: api/Player/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Player
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //mora da prima player obican jer ti treba file a ne imagepath
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer([FromForm] PlayerManipulationDTO playerDTO)
        {
           
            try
            {
                var player= _mapper.Map<Player>(playerDTO);

                if (playerDTO.Picture != null && playerDTO.Picture.Length > 0)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + playerDTO.Picture.FileName;

                    // path for picture
                    var filePath = Path.Combine("wwwroot", "images", "players", uniqueFileName);

                    // save picture on server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await playerDTO.Picture.CopyToAsync(fileStream);
                    }

                    // return path
                    player.ImagePath=Path.Combine("images", "players", uniqueFileName);
                }

                var createdPlayer = await _repository.CreatePlayer(player);

                var createdPlayerDTO = _mapper.Map<PlayerManipulationDTO>(createdPlayer);

                return Ok(createdPlayerDTO);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        // DELETE: api/Player/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            if (_context.Players == null)
            {
                return NotFound();
            }
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("team/{id}")]
        public async Task<ActionResult<PlayerDTO>> GetPlayerByTeamId(int id)
        {
            try
            {
                var players = await _repository.GetPlayersByTeamId(id);

                if (players == null)
                {
                    return NotFound(); // Tim nije pronađen
                }
                var playersDTO = _mapper.Map<IEnumerable<PlayerDTO>>(players);

                return Ok(playersDTO);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        private bool PlayerExists(int id)
        {
            return (_context.Players?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
