using Application.Dtos;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
	public class MatchService : IMatchService
	{
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ILogger<MatchService> _logger;

        public MatchService(IMatchRepository matchRepository, ITeamRepository teamRepository, ILogger<MatchService> logger)
        {
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<MatchResponseDto>> GetAllMatchesAsync()
        {
            var matches = await _matchRepository.GetAllAsync();

            return matches.Select(m => new MatchResponseDto
            { 
                FirstTeamId = m.FirstTeamId,
                FirstTeamScore = m.FirstTeamScore,
                FirstTeamName = m.FirstTeam!.Name,
                SecondTeamId = m.SecondTeamId,
                SecondTeamScore = m.SecondTeamScore,
                SecondTeamName = m.SecondTeam!.Name,
                PlayedAt = m.PlayedAt
            });
        }

        public async Task<MatchResponseDto> GetMatchByIdAsync(Guid id)
        {
            var match = await _matchRepository.GetByIdAsync(id);

            if (match == null) throw new NotFoundException($"Match with id '{id}' not found.");

            return new MatchResponseDto
            {
                FirstTeamId = match.FirstTeamId,
                FirstTeamName = match.FirstTeam!.Name,
                FirstTeamScore = match.FirstTeamScore,
                SecondTeamId = match.SecondTeamId,
                SecondTeamName = match.SecondTeam!.Name,
                SecondTeamScore = match.SecondTeamScore,
                PlayedAt = match.PlayedAt
            };
        }

        public async Task CreateMatchAsync(MatchRequestDto matchRequest)
        {
            // Get teams
            var firstTeam = await _teamRepository.GetByIdAsync(matchRequest.FirstTeamId) ?? 
                throw new NotFoundException($"Team with id '{matchRequest.FirstTeamId}' not found.");
            var secondTeam = await _teamRepository.GetByIdAsync(matchRequest.SecondTeamId) ?? 
                throw new NotFoundException($"Team with id '{matchRequest.SecondTeamId}' not found.");

            // Check if match is already played
            var existing = (await _matchRepository.GetAllAsync())
                .FirstOrDefault(m => m.FirstTeamId == matchRequest.FirstTeamId && m.SecondTeamId == matchRequest.SecondTeamId);
            if (existing != null)
                throw new ConflictException($"Match between {firstTeam.Name} and {secondTeam.Name} is already played.");

            // Scoring logic
            if (matchRequest.FirstTeamScore > matchRequest.SecondTeamScore)
            {
                firstTeam.Points += 3;
                firstTeam.Wins++;
                secondTeam.Losses++;
                _logger.LogInformation(
                    "Match result: {Winner} wins against {Loser} with score {WinnerScore}:{LoserScore}",
                    firstTeam.Name, secondTeam.Name, matchRequest.FirstTeamScore, matchRequest.SecondTeamScore);
            }
            else if (matchRequest.FirstTeamScore < matchRequest.SecondTeamScore)
            {
                secondTeam.Points += 3;
                secondTeam.Wins++;
                firstTeam.Losses++;
                _logger.LogInformation(
                    "Match result: {Winner} wins against {Loser} with score {WinnerScore}:{LoserScore}",
                    secondTeam.Name, firstTeam.Name, matchRequest.SecondTeamScore, matchRequest.FirstTeamScore);
            }
            else // draw
            {
                firstTeam.Points += 1;
                secondTeam.Points += 1;
                firstTeam.Draws++;
                secondTeam.Draws++;
                _logger.LogInformation(
                    "Match result: Draw between {TeamA} and {TeamB} with score {ScoreA}:{ScoreB}",
                    firstTeam.Name, secondTeam.Name, matchRequest.FirstTeamScore, matchRequest.SecondTeamScore);
            }

            // Update teams info and save match
            await _teamRepository.UpdateAsync(firstTeam);
            await _teamRepository.UpdateAsync(secondTeam);

            var match = new Match
            {
                FirstTeamId = matchRequest.FirstTeamId,
                FirstTeamScore = matchRequest.FirstTeamScore,
                SecondTeamId = matchRequest.SecondTeamId,
                SecondTeamScore = matchRequest.SecondTeamScore,
                PlayedAt = matchRequest.PlayedAt
            };

            await _matchRepository.AddAsync(match);
        }

        public async Task DeleteMatchAsync(Guid id)
        {
            var isDeleted = await _matchRepository.DeleteAsync(id);
            if (!isDeleted)
            {
                throw new NotFoundException($"Match with id '{id}' not found.");
            }
        }
    }
}
