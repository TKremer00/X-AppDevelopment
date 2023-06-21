using FinalProject.Data.Models;
using FinalProject.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Persistence.Repositories
{
    public class PlantRepository : BaseRepository<Plant>
    {
        public PlantRepository(PlantsContext context) : base(context)
        {
        }

        public Task<List<Plant>> GetMostRecentAsync(int count) => _context.Plants.OrderByDescending(x => x.CreatedAt).Take(count).ToListAsync();
    }
}
