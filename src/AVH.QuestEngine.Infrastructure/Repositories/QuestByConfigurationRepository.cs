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
            _configFileName = configuration.GetValue<string>(ConfigConstant.ConfigurationFileNameKey, ConfigConstant.DefaultConfigurationFileName);
            _memoryCache = memoryCache;
        }

        public async Task<Quest?> GetActiveQuest()
        {
            var quests = Enumerable.Empty<Quest>();

            if (!_memoryCache.TryGetValue(ConfigConstant.ConfigurationCacheKey, out quests))
            {
                if (!string.IsNullOrEmpty(_configFileName) && File.Exists(_configFileName))
                {
                    var json = await File.ReadAllTextAsync(_configFileName);
                    quests = JsonSerializer.Deserialize<IEnumerable<Quest>>(json);

                    _memoryCache.Set(ConfigConstant.ConfigurationCacheKey, quests);
                }
            }
            return quests?.FirstOrDefault(x => x.IsActive);
        }
    }
}
