using Domain.Entities;

namespace Application.Dtos
{
    public class TeamDto
    {
        public string Name { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public List<MatchSummaryDto>? MatchesPlayed { get; set; }

        public static TeamDto ToDto(Team team)
        {
            return new TeamDto
            {
                Name = team.Name,
                Country = team.Country,
                City = team.City,
                MatchesPlayed = team.MatchesPlayed?.Select(m => new MatchSummaryDto
                {
                    OpponentName = m.SecondTeam.Name,
                    GoalsFor = m.FirstTeamScore,
                    GoalsAgainst = m.SecondTeamScore,
                    Date = m.PlayedAt,
                    Result = m.FirstTeamScore > m.SecondTeamScore ? "Win" : 
                        m.FirstTeamScore < m.SecondTeamScore ? "Loss" : "Draw",
                }).ToList()
            };
        }
    }

    public class TeamStatsDto
    {
        public int Played { get; set; }

        public int Won { get; set; }

        public int Drawn { get; set; }

        public int Lost { get; set; }

        public int Points { get; set; }
    }
}
