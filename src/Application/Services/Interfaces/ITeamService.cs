using System.Collections.Generic;
using Application.Dtos;

namespace Application.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamResponseDto>> GetAllTeamsAsync();
        Task<TeamResponseDto?> GetTeamByNameAsync(string name);
        Task<TeamResponseDto?> GetTeamByIdAsync(Guid id);
        Task<Guid> AddTeamAsync(TeamRequestDto teamRequest);
        Task UpdateTeamAsync(Guid id, TeamRequestDto teamRequest);
        Task DeleteTeamAsync(Guid id);
    }
}