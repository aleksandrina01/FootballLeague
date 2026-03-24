using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAllAsync();

        Task<Team> GetByIdAsync(Guid id);

        Task<Team> GetByNameAsync(string name);

        Task AddAsync(Team team);

        Task UpdateAsync(Team team);

        Task DeleteAsync(Guid id);
    }
}
