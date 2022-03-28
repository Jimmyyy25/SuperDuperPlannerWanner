using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperDuperPlannerWanner.Models
{
    public class PlanItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Index { get; set; }
        public int TypeId { get; set; }
        // This this is supposed to be - an item is a meal or an activity or something else
        public int ItemTypeId { get; set; }
        public int ItemId { get; set; }

        [Display(Name = "Date Added")]
        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }
        [Display(Name = "Date Amended")]
        [DataType(DataType.DateTime)]
        public DateTime DateAmended { get; set; }
    }
}
