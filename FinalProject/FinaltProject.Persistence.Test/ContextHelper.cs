using FinalProject.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace FinaltProject.Persistence.Test
{
    internal static class ContextHelper
    {
        public static PlantsContext GenerateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PlantsContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());

            return new PlantsContext(optionsBuilder.Options);
        }
    }
}
