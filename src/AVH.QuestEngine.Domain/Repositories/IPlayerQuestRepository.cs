using AVH.QuestEngine.Domain.Entities;
using AVH.QuestEngine.Domain.LifeTime;
using AVH.QuestEngine.Domain.Repositories.Base;

namespace AVH.QuestEngine.Domain.Repositories
{
    public interface IPlayerQuestRepository : IBaseRepository<PlayerQuest>, IScopedDependency
    {
        Task<PlayerQuest?> GetByPlayerAndQuest(Guid playerId, Guid questId);
    }
}
