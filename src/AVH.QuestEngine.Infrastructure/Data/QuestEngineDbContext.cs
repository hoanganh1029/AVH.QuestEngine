using AVH.QuestEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AVH.QuestEngine.Infrastructure.Data
{
    public class QuestEngineDbContext(DbContextOptions<QuestEngineDbContext> options) : DbContext(options)
    {
        public DbSet<Quest> Quests { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerQuest> PlayerQuests { get; set; }

    }
}
