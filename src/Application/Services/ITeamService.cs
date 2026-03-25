using System.Collections.Generic;
using Application.Dtos;
using Domain.Entities;

namespace Application.Services
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDto>> GetAllTeamsAsync();
        Task<TeamDto?> GetTeamByNameAsync(string name);
        Task AddTeamAsync(Team team);
        Task UpdateTeamAsync(Team team);
        Task DeleteTeamAsync(Guid id);
    }
}