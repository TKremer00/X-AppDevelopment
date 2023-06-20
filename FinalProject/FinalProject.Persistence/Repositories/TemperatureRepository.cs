using FinalProject.Persistence.Database;
using FinalProject.Persistence.Models;

namespace FinalProject.Persistence.Repositories
{
    public class TemperatureRepository : BaseRepository<Temperature>
    {
        public TemperatureRepository(PlantsContext context) : base(context)
        {
        }
    }
}
