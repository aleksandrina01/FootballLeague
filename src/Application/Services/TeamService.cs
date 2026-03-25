using Application.Dtos;
using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _repo;

        public TeamService(ITeamRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TeamDto>> GetAllTeamsAsync()
        {
            var teams = await _repo.GetAllAsync();

            return teams.Select(TeamDto.ToDto);
        }

        public async Task<TeamDto?> GetTeamByNameAsync(string name)
        {
            var team = await _repo.GetByNameAsync(name ?? string.Empty);

            return TeamDto.ToDto(team);
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
            await _repo.AddAsync(team);
        }

        public async Task UpdateTeamAsync(TeamRequestDto teamRequest)
        {
            var team = new Team
            {
                Name = teamRequest.Name,
                Country = teamRequest.Country,
                City = teamRequest.City,
                Players = teamRequest.Players ?? 0
            };
            await _repo.UpdateAsync(team);
        }

        public async Task DeleteTeamAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
