using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperDuperPlannerWanner.Models
{
    
    public class MealIngredientLink
    {
        [Key]
        public int MealId { get; set; }
        [Key]
        public int IngredientId { get; set; }
    }
}
