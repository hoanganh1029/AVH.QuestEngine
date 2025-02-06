using AVH.QuestEngine.Domain.Entities.Base;

namespace AVH.QuestEngine.Domain.Entities
{
    public class Player : BaseEntity
    {
        public string? UserName { get; set; }
        public int Level { get; set; }
    }
}
