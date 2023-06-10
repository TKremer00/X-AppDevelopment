using FinalProject.Persistence.Database;
using FinalProject.Persistence.Models;

namespace FinalProject.Persistence.Repositories
{
    public class PlantRepository : BaseRepository<Plant>
    {
        public PlantRepository(PlantsContext context) : base(context)
        {
        }

        public Task<int> SaveAsync() => _context.SaveChangesAsync();
    }
}
