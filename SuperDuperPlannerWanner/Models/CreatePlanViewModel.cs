using Microsoft.AspNetCore.Mvc.Rendering;
using SuperDuperPlannerWanner.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperDuperPlannerWanner.Models
{
    public class CreatePlanViewModel
    {
        public Plan Plan { get; set; }
        public List<SelectListItem> PlanTypes {get; set;}
    }
}
