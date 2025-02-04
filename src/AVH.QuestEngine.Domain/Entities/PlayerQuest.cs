using AVH.QuestEngine.Domain.Entities.Base;

namespace AVH.QuestEngine.Domain.Entities
{
    public class PlayerQuest : BaseEntity
    {
        public Guid PlayerId { get; set; }
        public Guid QuestId { get; set; }
        public double TotalPoints { get; set; }
    }
}
