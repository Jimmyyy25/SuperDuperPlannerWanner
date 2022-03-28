using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperDuperPlannerWanner.Models
{
    public class PlanItemLink
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public int PlanItemId { get; set; }

        [Display(Name = "Date Added")]
        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }
        [Display(Name = "Date Amended")]
        [DataType(DataType.DateTime)]
        public DateTime DateAmended { get; set; }
    }
}
