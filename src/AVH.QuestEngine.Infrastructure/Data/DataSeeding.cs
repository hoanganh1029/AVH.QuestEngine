using AVH.QuestEngine.Application.Constants;
using AVH.QuestEngine.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AVH.QuestEngine.Infrastructure.Data
{
    public static class DataSeeding
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var serviceProvider = scope.ServiceProvider;

            var dbContext = serviceProvider.GetRequiredService<QuestEngineDbContext>();
            await dbContext.Database.MigrateAsync();

            var isExistDefaultData = await dbContext.PlayerQuestTurns.AnyAsync();
            if (!isExistDefaultData)
            {
                await dbContext.Players.AddAsync(new Player
                {
                    Id = Constant.DefaultPlayerId,
                    UserName = "AnhVH",
                    Level = 1,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "DataSeeding"
                });

                /*
                await dbContext.PlayerQuestTurns.AddAsync(new PlayerQuestTurn
                {
                    Id = Guid.NewGuid(),
                    QuestId = Constant.DefaultQuestId,
                    PlayerId = Constant.DefaultPlayerId,
                    ChipAmountBet = 2,
                    PlayerLevel = 1,
                    EarnedPoints = 10,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "DataSeeding"
                });
                */

                await dbContext.Quests.AddAsync(new Quest
                {
                    Id = Constant.DefaultQuestId,
                    Code = "HPNY",
                    Name = "Happy New Year",
                    TotalQuestPointsRequired = 1000,
                    RateFromBet = 0.4,
                    LevelBonusRate = 2,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "DataSeeding"
                });

                await dbContext.Milestones.AddRangeAsync(new List<Milestone>
                {
                    new() {
                        Id = Guid.NewGuid(),
                        QuestId = Constant.DefaultQuestId,
                        Index = 1,
                        RequiredPoints = 200,
                        ChipsAwarded = 50
                    },
                    new() {
                        Id = Guid.NewGuid(),
                        QuestId = Constant.DefaultQuestId,
                        Index = 2,
                        RequiredPoints = 400,
                        ChipsAwarded = 150
                    },
                    new() {
                        Id = Guid.NewGuid(),
                        QuestId = Constant.DefaultQuestId,
                        Index = 3,
                        RequiredPoints = 600,
                        ChipsAwarded = 300
                    },
                    new() {
                        Id = Guid.NewGuid(),
                        QuestId = Constant.DefaultQuestId,
                        Index = 4,
                        RequiredPoints = 1000,
                        ChipsAwarded = 500
                    }
                });

                await dbContext.SaveChangesAsync();
            }

        }
    }
}
