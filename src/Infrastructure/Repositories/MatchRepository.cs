using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Match = Domain.Entities.Match;

namespace Infrastructure.Repositories
{
	public class MatchRepository : IMatchRepository
    {
        private readonly FootballLeagueContext _context;

        public MatchRepository(FootballLeagueContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Match>> GetAllAsync()
        {
            return await _context.Matches
                .Include(m => m.FirstTeam)
                .Include(m => m.SecondTeam)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Match?> GetByIdAsync(Guid id)
        {
            return await _context.Matches
                .Include(m => m.FirstTeam)
                .Include(m => m.SecondTeam)
                .AsNoTracking()
                .FirstOrDefaultAsync(match => match.Id == id);
        }

        public async Task AddAsync(Match match)
        {
            await _context.Matches.AddAsync(match);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var match = await _context.Matches.FirstOrDefaultAsync(m => m.Id == id);

            if (match == null) return false;

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
