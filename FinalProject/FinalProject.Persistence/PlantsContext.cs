using FinalProject.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Persistence.Database
{
    public class PlantsContext : DbContext
    {
        public virtual DbSet<Plant> Plants { get; set; }

        public virtual DbSet<Temperature> Temperatures { get; set; }

        public static DbContextOptions GetDbContextOptions(DbContextOptionsBuilder optionsBuilder)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var dbPath = Path.Join(path, "plants.db");

            return optionsBuilder
                .UseSqlite($"Data Source={dbPath}")
                .Options;
        }

        public PlantsContext(DbContextOptions options) : base(options)
        { }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        // protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={_dbPath}");
    }
}
