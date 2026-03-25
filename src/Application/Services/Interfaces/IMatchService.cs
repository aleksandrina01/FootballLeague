using Application.Dtos;

namespace Application.Services.Interfaces
{
	public interface IMatchService
	{
        Task<IEnumerable<MatchResponseDto>> GetAllMatchesAsync();
        Task<MatchResponseDto> GetMatchByIdAsync(Guid id);
        Task CreateMatchAsync(MatchRequestDto matchRequest);
        Task DeleteMatchAsync(Guid id);
    }
}
