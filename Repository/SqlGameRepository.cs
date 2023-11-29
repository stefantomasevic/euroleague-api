using Euroleague.Data;
using Euroleague.DTO;
using Euroleague.Models;
using Microsoft.EntityFrameworkCore;

namespace Euroleague.Repository
{
    public class SqlGameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _context;

        public SqlGameRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Game> CreateGame(Game game)
        {
           _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task DeleteGame(int id)
        {
            _context.Games.Remove(await _context.Games.FindAsync(id) ?? throw new InvalidOperationException("Game doesnt exist"));
            await _context.SaveChangesAsync();
        }

        public async Task<Game> GameDetails(int id)
        {
            var game= await _context.Games.Include(h => h.HomeTeam).Include(g => g.GuestTeam).Include(g => g.Statistics).ThenInclude(stat => stat.Player) .FirstOrDefaultAsync(g => g.Id == id)
                ?? throw new Exception($"Game with ID {id} not found.");

            return game;
           
        }

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            var games = await _context.Games.Include(h=>h.HomeTeam).Include(g=>g.GuestTeam).ToListAsync();
            return games;
        }

        public async Task<Game> GetGameById(int id)
        {
            return await _context.Games.Include(g => g.HomeTeam).Include(g => g.GuestTeam)
                .FirstOrDefaultAsync(g => g.Id == id) ?? throw new Exception($"Game with ID {id} not found."); ;

        }

        public async Task<Game> UpdateGame(Game game)
        {
            _context.Games.Update(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<Game> GamePlayers(int gameId)
        {
            var gamePlayers= await _context.Games.Include(h => h.HomeTeam).ThenInclude(p => p.Players).Include(g => g.GuestTeam).ThenInclude(p=>p.Players).FirstOrDefaultAsync(g => g.Id == gameId) ?? throw new Exception($"Game with ID {gameId} not found."); ;
            return gamePlayers ;

            //napravi u ponedeljak mapiranje, dobio si ovde sve igrace jednog i sve igrace drugog tima
        }
    }
}
