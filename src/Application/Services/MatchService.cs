using Application.Dtos;
using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
        public async Task AddMatchAsync(MatchRequestDto matchRequest)
        {
            var firstTeam = await _context.Teams.FindAsync(matchRequest.FirstTeamId);
            var secondTeam = await _context.Teams.FindAsync(matchRequest.SecondTeamId);

            if (firstTeam == null || secondTeam == null)
                throw new Exception("Team not found");

            var isPlayed = await _context.Matches.Where(m => m.FirstTeamId == matchRequest.FirstTeamId &&
                m.SecondTeamId == matchRequest.SecondTeamId).FirstOrDefaultAsync() ?? throw new Exception("Match already played");

            // scoring logic
            if (matchRequest.FirstTeamScore > matchRequest.SecondTeamScore)
            {
                firstTeam.Points += 3;
                firstTeam.Wins++;
                secondTeam.Losses++;
            }
            else if (matchRequest.FirstTeamScore < matchRequest.SecondTeamScore)
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

            var match = new Match
            {
                FirstTeamId = matchRequest.FirstTeamId,
                SecondTeamId = matchRequest.SecondTeamId,
                FirstTeamScore = matchRequest.FirstTeamScore,
                SecondTeamScore = matchRequest.SecondTeamScore,
                PlayedAt = matchRequest.PlayedAt
            };

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();
        }
    }
}
