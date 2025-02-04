using AVH.QuestEngine.Domain.Entities;
using AVH.QuestEngine.Domain.Repositories;
using AVH.QuestEngine.Infrastructure.Data;
using AVH.QuestEngine.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AVH.QuestEngine.Infrastructure.Repositories
{
    public class PlayerQuestRepository : BaseRepository<PlayerQuest>, IPlayerQuestRepository
    {
        public PlayerQuestRepository(QuestEngineDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PlayerQuest?> GetByPlayerAndQuest(Guid playerId, Guid questId)
        {
            return await _currentEntity.FirstOrDefaultAsync(x => x.PlayerId == playerId && x.QuestId == questId);
        }
    }
}
