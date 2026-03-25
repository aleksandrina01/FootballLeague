using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
	internal interface IMatchRepository
	{
        Task<IEnumerable<Team>> GetAllAsync();

        Task<Team> GetByIdAsync(Guid id);

        Task AddAsync(Team team);

        Task DeleteAsync(Guid id);
    }
}
