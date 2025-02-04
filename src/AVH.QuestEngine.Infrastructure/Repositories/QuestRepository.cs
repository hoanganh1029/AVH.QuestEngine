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
            return await _currentEntity.SingleOrDefaultAsync(x => x.IsActive);
        }
    }
}
