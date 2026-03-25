using System.Collections.Generic;
using Application.Dtos;
using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamResponseDto>> GetAllTeamsAsync();
        Task<TeamResponseDto?> GetTeamByNameAsync(string name);
        Task AddTeamAsync(TeamRequestDto teamRequest);
        Task UpdateTeamAsync(Guid id, TeamRequestDto teamRequest);
        Task DeleteTeamAsync(Guid id);
    }
}