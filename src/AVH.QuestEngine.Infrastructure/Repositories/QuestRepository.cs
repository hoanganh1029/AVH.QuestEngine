using AVH.QuestEngine.Domain.Entities;
using AVH.QuestEngine.Domain.Repositories;
using AVH.QuestEngine.Infrastructure.Data;
using AVH.QuestEngine.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AVH.QuestEngine.Infrastructure.Repositories
{
    public class QuestRepository : BaseRepository<Quest>, IQuestRepository
    {
        public QuestRepository(QuestEngineDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Quest?> GetActiveQuest()
        {
            var activeQuest = await _currentEntity.SingleOrDefaultAsync(x => x.IsActive);
            if (activeQuest != null && activeQuest.Milestones.Any())
            {
                activeQuest.Milestones = [.. activeQuest.Milestones.OrderBy(x => x.Index)];
            }
            return activeQuest;
        }
    }
}
