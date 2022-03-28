using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperDuperPlannerWanner.Data;
using SuperDuperPlannerWanner.Models;

namespace SuperDuperPlannerWanner.Controllers
{
    public class MealsController : Controller
    {
        private readonly SuperDuperPlannerWannerContext _context;
        private int? _iMealId;

        public MealsController(SuperDuperPlannerWannerContext context,
                               IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _iMealId = httpContextAccessor.HttpContext.Session.GetInt32("MealId");
        }

        public IActionResult MealsMenu()
        {
            return View();
        }

        public IActionResult ViewMealPlans()
        {
            HttpContext.Session.SetInt32("PlanTypeId", 1);

            return RedirectToAction("ViewPlans", "Plans");
        }

        // GET: Meals1
        public async Task<IActionResult> ViewMeals()
        {
            return View(await _context.Meal.ToListAsync());
        }

        

        // GET: Meals1/Details/5
        public async Task<IActionResult> MealDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // GET: Meals1/Create
        public IActionResult CreateMeal()
        {
            return View();
        }

        // POST: Meals1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMeal([Bind("Id,Name,Description,MealTypeId,Notes,DateAdded,DateAmended")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meal);
                await _context.SaveChangesAsync();
                return RedirectToAction("Meals", "ViewIngredients");
            }

            return View(meal);
        }

        // GET: Meals1/Edit/5
        public async Task<IActionResult> EditMeal(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetInt32("MealId", meal.Id);

            return View(meal);
        }

        // POST: Meals1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMeal(int id, [Bind("Id,Name,Description,MealTypeId,Notes,DateAdded,DateAmended")] Meal meal)
        {
            if (id != meal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealExists(meal.Id))
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
            return View(meal);
        }

        // GET: Meals1/Delete/5
        public async Task<IActionResult> DeleteMeal(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // POST: Meals1/Delete/5
        [HttpPost, ActionName("DeleteMeal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meal = await _context.Meal.FindAsync(id);
            _context.Meal.Remove(meal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealExists(int id)
        {
            return _context.Meal.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ChooseIngredients()
        {
            List<Ingredient, bool> listIngredients;

            if (_iMealId == null)
            {
                return NotFound();
            }

            return View(await _context.Ingredient.ToListAsync());
        }

        [HttpPost, ActionName("ChooseIngredients")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChooseIngredients()
        {
            if (_iMealId == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: put in session or cache
                    List<Ingredient> listIngredients = await _context.Ingredient.ToListAsync()
                    _context.Update(meal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealExists(meal.Id))
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

            return View(meal);
        }
    }
}