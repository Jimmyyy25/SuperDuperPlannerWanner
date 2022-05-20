using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperDuperPlannerWanner.Models.ViewModels
{
    public class MealDetailsViewModel
    {
        public Meal Meal { get => this.Meal; set => this.Meal = value; }
        public List<MealIngredientBind> Ingredients { get => Ingredients; set => Ingredients = value; }
    }
}
