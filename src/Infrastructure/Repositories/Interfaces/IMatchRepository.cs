using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
	public interface IMatchRepository
	{
        Task<IEnumerable<Match>> GetAllAsync();

        Task<Match?> GetByIdAsync(Guid id);

        Task AddAsync(Match match);

        Task<bool> DeleteAsync(Guid id);
    }
}
