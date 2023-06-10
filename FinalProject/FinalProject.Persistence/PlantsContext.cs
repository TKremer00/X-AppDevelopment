﻿using FinalProject.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Persistence.Database
{
    public class PlantsContext : DbContext
    {
        private readonly string _dbPath;

        public virtual DbSet<Plant> Plants { get; set; }

        public PlantsContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            _dbPath = Path.Join(path, "friendsbook.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={_dbPath}");
    }
}
