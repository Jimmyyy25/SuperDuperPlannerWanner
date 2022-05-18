using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperDuperPlannerWanner.Data;
using SuperDuperPlannerWanner.Enums;
using SuperDuperPlannerWanner.Models;

namespace SuperDuperPlannerWanner.Controllers
{
    public class PlansController : Controller
    {
        private readonly SuperDuperPlannerWannerContext _context;

        private int? _iPlanTypeId;

        public PlansController(SuperDuperPlannerWannerContext context
                               ,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;

            
            _iPlanTypeId = httpContextAccessor.HttpContext.Session.GetInt32("PlanTypeId");
        }

        // GET: Plans
        public async Task<IActionResult> ViewPlans()
        {
            List<Plan> Plans = new List<Plan>();

            if (_iPlanTypeId == null)
            {
                PlanType planType = (PlanType)_iPlanTypeId;

                // get selected plans from db
                Plans = await _context.Plan.Where(plan => plan.TypeId == planType).ToListAsync();
            }
            else
            {
                Plans = await _context.Plan.ToListAsync();
            }

            return View(Plans);
        }

        // GET: Plans/Details/5
        public async Task<IActionResult> PlanDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        // GET: Plans/Create
        public IActionResult CreatePlan()
        {
            CreatePlanViewModel vm = new CreatePlanViewModel();

            List<SelectListItem> planTypes = new List<SelectListItem>();

             Array james = Enum.GetValues(typeof(PlanType));

            foreach (PlanType item in Enum.GetValues(typeof(PlanType)))
            {
                planTypes.Add(
                    new SelectListItem { Text = item.ToString(), Value = ((int)item).ToString() }
                );
            }

            vm.PlanTypes = planTypes;

            return View(vm);
        }

        // POST: Plans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePlan([Bind("Id,Name,Description,TypeId,DateAdded,DateAmended")] Plan plan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ViewPlans));
            }
            return View(plan);
        }

        // GET: Plans/Edit/5
        public async Task<IActionResult> EditPlan(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan.FindAsync(id);
            if (plan == null)
            {
                return NotFound();
            }
            return View(plan);
        }

        // POST: Plans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPlan(int id, [Bind("Id,Name,Description,TypeId,DateAdded,DateAmended")] Plan plan)
        {
            if (id != plan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanExists(plan.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ViewPlans));
            }
            return View(plan);
        }

        // GET: Plans/Delete/5
        public async Task<IActionResult> DeletePlan(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        // POST: Plans/Delete/5
        [HttpPost, ActionName("DeletePlan")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plan = await _context.Plan.FindAsync(id);
            _context.Plan.Remove(plan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ViewPlans));
        }

        private bool PlanExists(int id)
        {
            return _context.Plan.Any(e => e.Id == id);
        }
    }
}
