using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperDuperPlannerWanner.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperDuperPlannerWanner.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SuperDuperPlannerWannerContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SuperDuperPlannerWannerContext>>()))
            {
                // Look for any movies.
                if (context.Meal.Any())
                {
                    return;   // DB has been seeded
                }

                context.Meal.AddRange(
                    new Meal
                    {
                        Name = "Beans on Toast",
                        Description = "",
                        MealTypeId = 1,
                        Notes = "Simple, quick, easy, cheap",
                        DateAdded = DateTime.Parse("1973-01-01"),
                        DateAmended = DateTime.Parse("1973-01-01")
                    }
                );
                context.SaveChanges();
    
            }
        }
    }
}
