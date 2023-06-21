using FinalProject.Data.Models;
using FinalProject.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Persistence.Repositories
{
    public class TemperatureRepository : BaseRepository<Temperature>
    {
        public TemperatureRepository(PlantsContext context) : base(context)
        {
        }

        public Task<List<Temperature>> GetLastAsync(int last) => _context.Temperatures.OrderByDescending(x => x.CreatedAt).Take(last).ToListAsync();
    }
}
