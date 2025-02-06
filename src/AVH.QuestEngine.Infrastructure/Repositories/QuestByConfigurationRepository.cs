using AVH.QuestEngine.Application.Constants;
using AVH.QuestEngine.Domain.Entities;
using AVH.QuestEngine.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace AVH.QuestEngine.Infrastructure.Repositories
{
    public class QuestByConfigurationRepository : IQuestRepository
    {
        private readonly string? _configFileName;
        private readonly IMemoryCache _memoryCache;
        public QuestByConfigurationRepository(IConfiguration configuration, IMemoryCache memoryCache)
        {
            _configFileName = configuration.GetValue<string>(Constant.ConfigurationFileNameKey, Constant.DefaultConfigurationFileName);
            _memoryCache = memoryCache;
        }

        public async Task<Quest?> GetActiveQuest()
        {            
            if (!_memoryCache.TryGetValue(Constant.ConfigurationCacheKey, out Quest? activeQuest))
            {
                if (!string.IsNullOrEmpty(_configFileName) && File.Exists(_configFileName))
                {
                    var json = await File.ReadAllTextAsync(_configFileName);
                    var quests = JsonSerializer.Deserialize<IEnumerable<Quest>>(json);

                    activeQuest = quests?.FirstOrDefault(x => x.IsActive);
                    if (activeQuest != null && activeQuest.Milestones.Any())
                    {
                        activeQuest.Milestones = [.. activeQuest.Milestones.OrderBy(x => x.Index)];
                    }
                    _memoryCache.Set(Constant.ConfigurationCacheKey, activeQuest);
                }
            }
            
            return activeQuest;
        }
    }
}
