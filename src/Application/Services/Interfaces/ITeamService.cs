using System.Collections.Generic;
using Application.Dtos;
using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDto>> GetAllTeamsAsync();
        Task<TeamDto?> GetTeamByNameAsync(string name);
        Task AddTeamAsync(TeamRequestDto teamRequest);
        Task UpdateTeamAsync(TeamRequestDto teamRequest);
        Task DeleteTeamAsync(Guid id);
    }
}