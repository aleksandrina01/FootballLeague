using Application.Dtos;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<TeamResponseDto>> GetAllTeamsAsync()
        {
            var teams = await _teamRepository.GetAllAsync();

            return teams.Select(TeamResponseDto.ToDto);
        }

        public async Task<TeamResponseDto?> GetTeamByNameAsync(string name)
        {
            var team = await _teamRepository.GetByNameAsync(name);
            if (team == null) throw new NotFoundException($"Team with name '{name}' not found.");

            return TeamResponseDto.ToDto(team);
        }

        public async Task AddTeamAsync(TeamRequestDto teamRequest)
        {
            var team = new Team
            {
                Name = teamRequest.Name,
                Country = teamRequest.Country,
                City = teamRequest.City,
                Players = teamRequest.Players ?? 0
            };
            await _teamRepository.AddAsync(team);
        }

        public async Task UpdateTeamAsync(Guid id, TeamRequestDto teamRequest)
        {
            var existing = await _teamRepository.GetByIdAsync(id) ?? 
                throw new NotFoundException($"Team with id '{id}' not found.");

            if (!string.IsNullOrWhiteSpace(teamRequest.Name)) existing.Name = teamRequest.Name;
            if (teamRequest.Country != null) existing.Country = teamRequest.Country;
            if (teamRequest.City != null) existing.City = teamRequest.City;
            if (teamRequest.Players.HasValue) existing.Players = teamRequest.Players.Value;

            await _teamRepository.UpdateAsync(existing);
        }

        public async Task DeleteTeamAsync(Guid id)
        {
            if (await _teamRepository.HasMatchesAsync(id))
                throw new ConflictException("Team has played matches and cannot be deleted.");

            var isDeleted = await _teamRepository.DeleteAsync(id);

            if (!isDeleted) throw new NotFoundException($"Team with id '{id}' not found.");
        }
    }
}
