using AVH.QuestEngine.Domain.Entities.Base;

namespace AVH.QuestEngine.Domain.Entities
{
    public class Quest : BaseEntity
    {
        public int TotalQuestPointsRequired { get; set; }
        public double RateFromBet { get; set; }
        public double LevelBonusRate { get; set; }
        public bool IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual ICollection<Milestone> Milestones { get; set; } = [];
    }
}
