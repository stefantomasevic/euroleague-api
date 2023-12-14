using Euroleague.Models;

namespace Euroleague.Repository
{
    public interface IStatisticRepository
    {
        Task UpdateStatistic(PlayerStatisticUpdate playerStatisticUpdate);
    }
}
