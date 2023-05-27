﻿using Friendsbook.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Friendsbook.Persistence
{
    public class FriendsbookContext : DbContext
    {
        private readonly string _dbPath;

        public virtual DbSet<Friend> Friends { get; set; }

        public FriendsbookContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            _dbPath = Path.Join(path, "friendsbook.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={_dbPath}");
    }
}
