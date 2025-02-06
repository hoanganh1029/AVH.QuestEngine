using AVH.QuestEngine.Domain.Entities.Base;

namespace AVH.QuestEngine.Domain.Entities
{    
    public class PlayerQuestTurn : BaseEntity
    {
        public Guid PlayerId { get; set; }
        public Guid QuestId { get; set; }
        public int PlayerLevel { get; set; }
        public int ChipAmountBet { get; set; }
        public double EarnedPoints { get; set; }
    }
}
