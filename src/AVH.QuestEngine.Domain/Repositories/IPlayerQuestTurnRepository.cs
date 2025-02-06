using AVH.QuestEngine.Domain.Entities;
using AVH.QuestEngine.Domain.LifeTime;
using AVH.QuestEngine.Domain.Repositories.Base;

namespace AVH.QuestEngine.Domain.Repositories
{
    public interface IPlayerQuestTurnRepository : IBaseRepository<PlayerQuestTurn>, IScopedDependency
    {
        Task<double> GetTotalPointsByPlayerQuest(Guid playerId, Guid questId);
    }
}
