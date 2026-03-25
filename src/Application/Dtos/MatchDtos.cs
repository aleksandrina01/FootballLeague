using System.Text.Json.Serialization;

namespace Application.Dtos
{
    public class MatchRequestDto
    {
        public Guid FirstTeamId { get; set; }
        public int FirstTeamScore { get; set; }
        public Guid SecondTeamId { get; set; }
        public int SecondTeamScore { get; set; }

        [JsonIgnore]
        public DateTime? PlayedAt { get; set; } = DateTime.UtcNow;
    }

    public class MatchResponseDto
    {
        public Guid FirstTeamId { get; set; }
        public string FirstTeamName { get; set; } = string.Empty;

        public Guid SecondTeamId { get; set; }
        public string SecondTeamName { get; set; } = string.Empty;

        public int? FirstTeamScore { get; set; }
        public int? SecondTeamScore { get; set; }

        public DateTime? PlayedAt { get; set; }
    }
}
