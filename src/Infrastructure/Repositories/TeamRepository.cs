using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly FootballLeagueContext _context;

        public TeamRepository(FootballLeagueContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAllAsync() => await _context.Teams.AsNoTracking().ToListAsync();

        public async Task<Team?> GetByIdAsync(Guid id)
        {
            var team = await _context.Teams.AsNoTracking().FirstOrDefaultAsync(team => team.Id == id);

            return team;
        }

        public async Task<Team?> GetByNameAsync(string name)
        {
            var team = await _context.Teams.AsNoTracking()
                .FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());

            return team;
        }

        public async Task AddAsync(Team team)
        {
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Team team)
        {
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);

            if (team == null) return false;

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> HasMatchesAsync(Guid teamId)
        {
            return await _context.Matches.AnyAsync(m => m.FirstTeamId == teamId || m.SecondTeamId == teamId);
        }
    }
}
