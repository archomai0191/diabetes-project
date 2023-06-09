﻿using Diabetes.Application.Interfaces;
using Diabetes.Domain;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Persistence.Contexts
{
    public class DataDbContext : DbContext, 
        IGlucoseLevelDbContext, 
        INoteInsulinDbContext,
        IFoodDbContext,
        ICarbohydrateNoteDbContext,
        IFoodPortionDbContext,
        IUsersFoodDbContext
    {
        
        public DbSet<Food> Foods { get; set; }
        public DbSet<InsulinNote> InsulinNotes { get; set; }
        public DbSet<GlucoseNote> GlucoseNotes  { get; set; }
        public DbSet<CarbohydrateNote> CarbohydrateNotes { get; set; }
        public DbSet<UsersFood> UsersFoods { get; set; }
        public DbSet<FoodPortion> FoodPortions { get; set; }

        public DataDbContext(DbContextOptions<DataDbContext> opt) : base(opt) {}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Food>()
                .HasMany(f=>f.CarbohydrateNotes)
                .WithMany(m=>m.Foods)
                .UsingEntity<FoodPortion>(
                    j => j
                        .HasOne(pt => pt.CarbohydrateNote)
                        .WithMany(t => t.Portions)
                        .HasForeignKey(pt => pt.CarbohydrateNoteId),
                    j => j
                        .HasOne(pt => pt.Food)
                        .WithMany(p => p.Portions)
                        .HasForeignKey(pt => pt.FoodId),
                    j =>
                    {
                        j.HasKey(t => new { t.FoodId, t.CarbohydrateNoteId });
                        j.ToTable("FoodPortions");
                    });
        }
        
    }
}