using System.ComponentModel.DataAnnotations;

namespace AVH.QuestEngine.Application.Requests
{    
    public class ProgressRequest
    {
        [Required]
        public required Guid PlayerId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]

        public int PlayerLevel { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ChipAmountBet { get; set; }
    }
}
