using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperDuperPlannerWanner.Models
{
    public class PlanItemLinkViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Display(Name = "Ingredient Type")]
        public int IngredientTypeId { get; set; }
        public string Notes { get; set; }

        [Display(Name = "Date Added")]
        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }
        [Display(Name = "Date Amended")]
        [DataType(DataType.DateTime)]
        public DateTime DateAmended { get; set; }

        public bool Selected { get; set; }
    }
}
