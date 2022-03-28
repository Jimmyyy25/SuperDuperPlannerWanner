using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperDuperPlannerWanner.Data;
using SuperDuperPlannerWanner.Models;

namespace SuperDuperPlannerWanner.Controllers
{
    public class PlanItemsController : Controller
    {
        private readonly SuperDuperPlannerWannerContext _context;

        public PlanItemsController(SuperDuperPlannerWannerContext context)
        {
            _context = context;
        }

        // GET: PlanItems
        public async Task<IActionResult> ViewPlanItems()
        {
            return View(await _context.PlanItem.ToListAsync());
        }

        // GET: PlanItems/Details/5
        public async Task<IActionResult> PlanItemDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planItem = await _context.PlanItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planItem == null)
            {
                return NotFound();
            }

            return View(planItem);
        }

        // GET: PlanItems/Create
        public IActionResult CreatePlanItem()
        {
            return View();
        }

        // POST: PlanItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePlanItem([Bind("Id,Name,Description,Index,TypeId,ItemTypeId,ItemId,DateAdded,DateAmended")] PlanItem planItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(planItem);
        }

        // GET: PlanItems/Edit/5
        public async Task<IActionResult> EditPlanItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planItem = await _context.PlanItem.FindAsync(id);
            if (planItem == null)
            {
                return NotFound();
            }
            return View(planItem);
        }

        // POST: PlanItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPlanItem(int id, [Bind("Id,Name,Description,Index,TypeId,ItemTypeId,ItemId,DateAdded,DateAmended")] PlanItem planItem)
        {
            if (id != planItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanItemExists(planItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(planItem);
        }

        // GET: PlanItems/Delete/5
        public async Task<IActionResult> DeletePlanItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planItem = await _context.PlanItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planItem == null)
            {
                return NotFound();
            }

            return View(planItem);
        }

        // POST: PlanItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planItem = await _context.PlanItem.FindAsync(id);
            _context.PlanItem.Remove(planItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanItemExists(int id)
        {
            return _context.PlanItem.Any(e => e.Id == id);
        }
    }
}
