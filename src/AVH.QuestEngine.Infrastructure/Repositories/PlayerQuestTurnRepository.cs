using AVH.QuestEngine.Domain.Entities;
using AVH.QuestEngine.Domain.Repositories;
using AVH.QuestEngine.Infrastructure.Data;
using AVH.QuestEngine.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AVH.QuestEngine.Infrastructure.Repositories
{
    public class PlayerQuestTurnRepository : BaseRepository<PlayerQuestTurn>, IPlayerQuestTurnRepository
    {
        public PlayerQuestTurnRepository(QuestEngineDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<double> GetTotalPointsByPlayerQuest(Guid playerId, Guid questId)
        {
            return await _currentEntity.Where(x => x.PlayerId == playerId && x.QuestId == questId).SumAsync(x => x.EarnedPoints);
        }
    }
}
