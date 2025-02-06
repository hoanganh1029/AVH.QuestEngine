using AVH.QuestEngine.Domain.Entities;

namespace AVH.QuestEngine.Domain.Repositories
{
    public interface IQuestRepository
    {
        Task<Quest?> GetActiveQuest();
    }
}
