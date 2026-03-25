using Application.Dtos;

namespace Application.Services.Interfaces
{
	public interface IMatchService
	{
        Task AddMatchAsync(MatchRequestDto matchRequest);
    }
}
