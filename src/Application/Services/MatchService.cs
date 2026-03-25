using Domain.Entities;
using Infrastructure.Data;

namespace Application.Services
{
	public class MatchService : IMatchService
	{
        private readonly FootballLeagueContext _context;

        public MatchService(FootballLeagueContext context)
        {
            _context = context;
        }

        // move to repository
        public async Task AddMatchAsync(Match match)
        {
            var firstTeam = await _context.Teams.FindAsync(match.FirstTeamId);
            var secondTeam = await _context.Teams.FindAsync(match.SecondTeamId);

            if (firstTeam == null || secondTeam == null)
                throw new Exception("Team not found");

            // scoring logic
            if (match.FirstTeamScore > match.SecondTeamScore)
            {
                firstTeam.Points += 3;
                firstTeam.Wins++;
                secondTeam.Losses++;
            }
            else if (match.FirstTeamScore < match.SecondTeamScore)
            {
                secondTeam.Points += 3;
                secondTeam.Wins++;
                firstTeam.Losses++;
            }
            else
            {
                firstTeam.Points += 1;
                secondTeam.Points += 1;
                firstTeam.Draws++;
                secondTeam.Draws++;
            }

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();
        }
    }
}
