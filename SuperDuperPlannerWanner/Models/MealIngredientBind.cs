using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperDuperPlannerWanner.Models
{
    public class MealIngredientBind
    {
        public Ingredient Ingredient { get; set; }
        public bool IsLinked { get; set; }
    }
}
