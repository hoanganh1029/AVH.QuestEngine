using AVH.QuestEngine.Application.Requests;
using AVH.QuestEngine.Application.Responses.General;
using AVH.QuestEngine.Domain.LifeTime;

namespace AVH.QuestEngine.Application.Services
{
    public interface IQuestTrackingService : IScopedDependency
    {
        Task<Response> TrackProgressAsync(ProgressRequest request);
        Task<Response> GetStateAsync(Guid playerId);
    }
}
