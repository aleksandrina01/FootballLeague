using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAllAsync();

        Task<Team?> GetByIdAsync(Guid id);

        Task<Team?> GetByNameAsync(string name);

        Task AddAsync(Team team);

        Task UpdateAsync(Team team);

        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Returns true if the team is referenced by any matches (either first or second team).
        /// </summary>
        Task<bool> HasMatchesAsync(Guid teamId);
    }
}
