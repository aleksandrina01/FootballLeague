using Domain.Entities;

namespace Application.Dtos
{
    public class TeamDto
    {
        public string Name { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public int Points { get; set; }

        public int Wins { get; set; }

        public int Draws { get; set; }

        public int Losses { get; set; }

        ////public List<MatchSummaryDto>? MatchesPlayed { get; set; }

        public static TeamDto ToDto(Team team)
        {
            return new TeamDto
            {
                Name = team.Name,
                Country = team.Country,
                City = team.City,
                Wins = team.Wins,
                Draws = team.Draws,
                Losses = team.Losses,
                Points = team.Points
            };
        }
    }
}
