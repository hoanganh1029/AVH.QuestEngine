namespace AVH.QuestEngine.Application.Constants
{
    public static class Constant
    {
        public const string ConfigurationFileNameKey = "ConfigFileName";
        public const string DefaultConfigurationFileName = "QuestConfig.json";

        public const string IsUseConfigurationKey = "IsUseConfiguration";

        public const string ConfigurationCacheKey = "QuestsConfigurationData";

        public static readonly Guid DefaultPlayerId = new("0b5a9152-414a-41ff-b198-b8a707a4f90c");
        public static readonly Guid DefaultQuestId = new("bb2c2373-0b2f-4144-91d0-74e5ac905373");
    }
}
