using Application.Contracts;
using Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Text;

namespace Application.Persistance
{
    /*
     * Add-Migration InitialCreate -Verbose
     * 
     */
    public class ApplicationDbContext : DbContext
    {
        public string DbPath { get; private set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            DbPath = "C:\\SimulatedKeyStrokes\\SimulatedKeyStrokes.db";

            Database.EnsureDeleted();
            Database.Migrate();
        }

        public DbSet<KeyEntity> KeysEntities { get; set; }
        public DbSet<GameEntity> GameEntities { get; set; }
        public DbSet<GameKeyEntity> GameKeysEntities { get; set; }
        public DbSet<KeyModifierEntity> KeyModifierEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite(string.Format("Data Source={0}", DbPath));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
