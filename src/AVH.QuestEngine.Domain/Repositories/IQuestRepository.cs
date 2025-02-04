using AVH.QuestEngine.Domain.Entities;
using AVH.QuestEngine.Domain.LifeTime;

namespace AVH.QuestEngine.Domain.Repositories
{
    public interface IQuestRepository 
        //: IScopedDependency
    {
        Task<Quest?> GetActiveQuest();
    }
}
