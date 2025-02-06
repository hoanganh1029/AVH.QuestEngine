using AVH.QuestEngine.Application.Requests;
using AVH.QuestEngine.Application.Responses;
using AVH.QuestEngine.Application.Responses.General;
using AVH.QuestEngine.Application.Services;
using AVH.QuestEngine.WebAPI.Base;
using Microsoft.AspNetCore.Mvc;

namespace AVH.QuestEngine.WebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class QuestController : ApiBaseController
    {
        private readonly IQuestTrackingService _questTrackingService;
        public QuestController(IQuestTrackingService questTrackingService)
        {
            _questTrackingService = questTrackingService;
        }

        /// <summary>
        /// Tracking the progress of quest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("progress")]
        public async Task<IActionResult> TrackProgressAsync([FromBody] ProgressRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _questTrackingService.TrackProgressAsync(request) as Response<QuestProgress>;
            return HandleResponseAsActionResult(response);
        }

        /// <summary>
        /// Get state of quest of player
        /// </summary>
        /// <param name="playerId">The player id, e.g. 0b5a9152-414a-41ff-b198-b8a707a4f90c</param>
        /// <returns></returns>
        [HttpGet("state")]
        public async Task<IActionResult> GetStateAsync([FromQuery] Guid playerId)
        {
            if (playerId == Guid.Empty)
            {
                return BadRequest($"The param {nameof(playerId)} is invalid");
            }
            var response = await _questTrackingService.GetStateAsync(playerId) as Response<QuestState>;
            return HandleResponseAsActionResult(response);
        }

    }
}
