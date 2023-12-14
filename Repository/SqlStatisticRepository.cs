using Euroleague.Data;
using Euroleague.Models;
using Microsoft.EntityFrameworkCore;

namespace Euroleague.Repository
{
    public class SqlStatisticRepository : IStatisticRepository
    {
        private readonly ApplicationDbContext _context;

        public SqlStatisticRepository(ApplicationDbContext context)
        {
            _context = context;
        }
       

      

        public async Task UpdateStatistic(PlayerStatisticUpdate playerStatisticUpdate)
        {
            var statistic = await _context.Statistics
                    .FirstOrDefaultAsync(s => s.GameId == playerStatisticUpdate.GameId && s.PlayerId == playerStatisticUpdate.PlayerId);
            
           
            ///ostalo je samo da se posalje sa front dela, da li treba povecati statistiku za domacina ili gosta, moze preko enumeracije ili uobicajeno


            if (statistic != null)
            {
          
                switch (playerStatisticUpdate.Type)
                {
                    case StatisticType.fouls:
                        statistic.Fouls += playerStatisticUpdate.Value;
                        break;

                    case StatisticType.points:
                        statistic.Points += playerStatisticUpdate.Value;
                        var result = await _context.Games
                    .FirstOrDefaultAsync(s => s.Id == playerStatisticUpdate.GameId);
                        if (playerStatisticUpdate.IsHomeTeam == true)
                        {
                            result.HomeScore = result.HomeScore + playerStatisticUpdate.Value;
                        }
                        else
                        {
                            result.GuestScore = result.GuestScore + playerStatisticUpdate.Value;
                        }
                        
                        
                    
                        break;

                    case StatisticType.rebounds:
                        statistic.Rebounds += playerStatisticUpdate.Value;
                        break;

                    default:
                        // Ako je tip nepoznat, možeš dodati odgovarajuću logiku ili baciti izuzetak
                        throw new InvalidOperationException($"Nepoznat tip statistike: {playerStatisticUpdate.Type}");
                }

                // Sačuvaj promene u bazi podataka
                await _context.SaveChangesAsync();
            }
        }
    }
}
