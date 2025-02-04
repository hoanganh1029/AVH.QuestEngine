using AVH.QuestEngine.Application.ViewModels;

namespace AVH.QuestEngine.Application.Responses
{
    public class QuestProgress
    {
        public double QuestPointsEarned { get; set; }
        public double TotalQuestPercentCompleted { get; set; }
        public IEnumerable<MilestoneViewModel> MilestonesCompleted { get; set; } = [];
    }
}
