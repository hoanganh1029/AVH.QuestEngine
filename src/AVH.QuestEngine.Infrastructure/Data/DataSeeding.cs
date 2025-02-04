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

            var context = serviceProvider.GetRequiredService<QuestEngineDbContext>();
            await context.Database.MigrateAsync();

            var isExistDefaultData = await context.PlayerQuests.AnyAsync();
            if (!isExistDefaultData)
            {
                var defaultPlayerId = new Guid("0b5a9152-414a-41ff-b198-b8a707a4f90c");
                var defaultQuestId = new Guid("bb2c2373-0b2f-4144-91d0-74e5ac905373");
                await context.Players.AddAsync(new Player
                {
                    Id = defaultPlayerId,
                    UserName = "AnhVH",
                    Level = 10,
                });

                await context.PlayerQuests.AddAsync(new PlayerQuest
                {
                    Id = Guid.NewGuid(),
                    QuestId = defaultQuestId,
                    PlayerId = defaultPlayerId,
                    TotalPoints = 0
                });
            }

        }
    }
}
