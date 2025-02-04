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
        private readonly IPlayerQuestRepository _playerQuestRepository;
        private readonly IMapper _mapper;

        public QuestTrackingService(
            IQuestRepository questRepository,
            IPlayerQuestRepository playerQuestRepository,
            IMapper mapper)
        {
            _playerQuestRepository = playerQuestRepository;
            _questRepository = questRepository;
            _mapper = mapper;
        }
        public async Task<Response> TrackProgressAsync(ProgressRequest request)
        {
            var activeQuest = await _questRepository.GetActiveQuest();
            if (activeQuest == null)
            {
                return BadRequest<PlayerQuest>(Message.ActiveQuestNotExist);
            }
            var earnedPoint = (request.ChipAmountBet * activeQuest.RateFromBet) + (request.PlayerLevel * activeQuest.LevelBonusRate);
            var completedMilestones = Enumerable.Empty<MilestoneViewModel>();

            var playerQuest = await _playerQuestRepository.GetByPlayerAndQuest(request.PlayerId, activeQuest.Id);
            if (playerQuest == null)
            {
                playerQuest = new PlayerQuest
                {
                    Id = Guid.NewGuid(),
                    QuestId = activeQuest.Id,
                    PlayerId = request.PlayerId,
                    TotalPoints = 0
                };
                await _playerQuestRepository.AddAsync(playerQuest);
            }
            else
            {
                playerQuest.TotalPoints += earnedPoint;
                await _playerQuestRepository.UpdateAsync(playerQuest);
                completedMilestones = _mapper.Map<IEnumerable<MilestoneViewModel>>(activeQuest.Milestones.Where(x => x.RequiredPoints <= playerQuest.TotalPoints));
            }

            return Success(new QuestProgress()
            {
                QuestPointsEarned = earnedPoint,
                TotalQuestPercentCompleted = Math.Round(playerQuest.TotalPoints / activeQuest.TotalQuestPointsRequired * 100, 2),
                MilestonesCompleted = completedMilestones
            });
        }

        public async Task<Response> GetStateAsync(Guid playerId)
        {
            var activeQuest = await _questRepository.GetActiveQuest();
            if (activeQuest == null)
            {
                return BadRequest<QuestState>(Message.ActiveQuestNotExist);
            }
            var playerQuest = await _playerQuestRepository.GetByPlayerAndQuest(playerId, activeQuest.Id);
            if (playerQuest == null)
            {
                return Success(new QuestState());
            }
            var lastMilestone = activeQuest.Milestones.LastOrDefault(x => x.RequiredPoints < playerQuest.TotalPoints);
            return Success(new QuestState()
            {
                TotalQuestPercentCompleted = Math.Round(playerQuest.TotalPoints / activeQuest.TotalQuestPointsRequired * 100, 2),
                LastMilestoneIndexCompleted = lastMilestone?.Index ?? 0
            });
        }
    }
}
