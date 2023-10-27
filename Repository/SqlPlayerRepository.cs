using Euroleague.Data;
using Euroleague.Models;
using Microsoft.EntityFrameworkCore;

namespace Euroleague.Repository
{
    public class SqlPlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext _context;

        public SqlPlayerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Player CreatePlayer(Player player)
        {
            throw new NotImplementedException();
        }

       

        public void DeletePlayer(Player player)
        {
            throw new NotImplementedException();
        }

       

        public Player EditPlayer(Player player)
        {
            throw new NotImplementedException();
        }



        public async Task<IEnumerable<Player>> GetAllPlayers()
        {
            return await _context.Players.Include(t=>t.Team).ToListAsync();


        }

        public Player GetPlayerById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Player>> GetPlayersByTeamId(int teamId)
        {
            var players= await _context.Players.Include(t=>t.Team).Where(p=>p.TeamId == teamId).ToListAsync();

            return players;
        }
    }
}
