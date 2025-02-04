using AVH.QuestEngine.Domain.Entities.Base;

namespace AVH.QuestEngine.Domain.Entities
{
    public class Milestone : BaseEntity
    {
        public int Index { get; set; }
        public Guid QuestId { get; set; }
        public int RequiredPoints { get; set; }
        public int ChipsAwarded { get; set; }
    }
}
