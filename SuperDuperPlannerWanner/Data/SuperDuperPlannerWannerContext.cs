using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuperDuperPlannerWanner.Models;

namespace SuperDuperPlannerWanner.Data
{
    public class SuperDuperPlannerWannerContext : DbContext
    {
        public SuperDuperPlannerWannerContext (DbContextOptions<SuperDuperPlannerWannerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MealIngredientLink>()
                .HasKey(mik => new { mik.MealId, mik.IngredientId });
        }

        public DbSet<SuperDuperPlannerWanner.Models.Meal> Meal { get; set; }
        public DbSet<SuperDuperPlannerWanner.Models.Ingredient> Ingredient { get; set; }
        public DbSet<SuperDuperPlannerWanner.Models.MealIngredientLink> MealIngredientLink { get; set; }
        public DbSet<SuperDuperPlannerWanner.Models.Plan> Plan { get; set; }
        public DbSet<SuperDuperPlannerWanner.Models.PlanItem> PlanItem { get; set; }
        public DbSet<SuperDuperPlannerWanner.Models.PlanItemLink> PlanItemLink { get; set; }
    }
}
