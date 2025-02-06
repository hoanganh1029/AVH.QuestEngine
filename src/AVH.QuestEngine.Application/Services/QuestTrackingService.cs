using AutoMapper;
using AVH.QuestEngine.Application.Constants;
using AVH.QuestEngine.Application.Requests;
using AVH.QuestEngine.Application.Responses;
using AVH.QuestEngine.Application.Responses.General;
using AVH.QuestEngine.Application.ViewModels;
using AVH.QuestEngine.Domain.Entities;
using AVH.QuestEngine.Domain.Repositories;

namespace AVH.QuestEngine.Application.Services
{
    public class QuestTrackingService : ResponseAttributes, IQuestTrackingService
    {
        private readonly IQuestRepository _questRepository;
        private readonly IPlayerQuestTurnRepository _playerQuestTurnRepository;
        private readonly IMapper _mapper;

        public QuestTrackingService(
            IQuestRepository questRepository,
            IPlayerQuestTurnRepository playerQuestTurnRepository,
            IMapper mapper)
        {
            _playerQuestTurnRepository = playerQuestTurnRepository;
            _questRepository = questRepository;
            _mapper = mapper;
        }
        public async Task<Response> TrackProgressAsync(ProgressRequest request)
        {
            var activeQuest = await _questRepository.GetActiveQuest();
            if (activeQuest == null)
            {
                return BadRequest<PlayerQuestTurn>(Message.ActiveQuestNotExist);
            }

            var earnedPoints = (request.ChipAmountBet * activeQuest.RateFromBet) + (request.PlayerLevel * activeQuest.LevelBonusRate);
            var playerQuestTurn = new PlayerQuestTurn
            {
                QuestId = activeQuest.Id,
                PlayerId = request.PlayerId,
                ChipAmountBet = request.ChipAmountBet,
                PlayerLevel = request.PlayerLevel,
                EarnedPoints = earnedPoints
            };
            await _playerQuestTurnRepository.AddAsync(playerQuestTurn);

            var totalPoints = await _playerQuestTurnRepository.GetTotalPointsByPlayerQuest(request.PlayerId, activeQuest.Id);
            var completedMilestones = activeQuest.Milestones.Where(x => x.RequiredPoints <= totalPoints && x.RequiredPoints > totalPoints - earnedPoints);

            return Success(new QuestProgress()
            {
                QuestPointsEarned = earnedPoints,
                TotalQuestPercentCompleted = Math.Round(totalPoints / activeQuest.TotalQuestPointsRequired * 100, 2),
                MilestonesCompleted = _mapper.Map<IEnumerable<MilestoneViewModel>>(completedMilestones)
            });
        }

        public async Task<Response> GetStateAsync(Guid playerId)
        {
            var activeQuest = await _questRepository.GetActiveQuest();
            if (activeQuest == null)
            {
                return BadRequest<QuestState>(Message.ActiveQuestNotExist);
            }
            var totalPoints = await _playerQuestTurnRepository.GetTotalPointsByPlayerQuest(playerId, activeQuest.Id);

            var lastMilestoneIndexCompleted = totalPoints == 0
                                            ? 0
                                            : (activeQuest.Milestones.LastOrDefault(x => x.RequiredPoints < totalPoints)?.Index ?? 0);
            return Success(new QuestState()
            {
                TotalQuestPercentCompleted = Math.Round(totalPoints / activeQuest.TotalQuestPointsRequired * 100, 2),
                LastMilestoneIndexCompleted = lastMilestoneIndexCompleted
            });
        }
    }
}
