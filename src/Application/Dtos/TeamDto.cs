using Domain.Entities;

namespace Application.Dtos
{
    public class TeamDto
    {
        public string Name { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public string? Coach { get; set; }

        public int? FoundedYear { get; set; }

        public TeamStatsDto? Stats { get; set; }

        public List<MatchSummaryDto>? RecentMatches { get; set; }

        public static TeamDto ToDto(Team team)
        {
            return new TeamDto
            {
                Name = team.Name,
                Country = team.Country,
                City = team.City,
                Coach = team.Coach,
                FoundedYear = team.FoundedYear,
                Stats = null,
                RecentMatches = null
            };
        }
    }

    public class TeamStatsDto
    {
        public int Played { get; set; }

        public int Won { get; set; }

        public int Drawn { get; set; }

        public int Lost { get; set; }

        public int GoalsFor { get; set; }

        public int GoalsAgainst { get; set; }

        public int Points { get; set; }
    }
}
