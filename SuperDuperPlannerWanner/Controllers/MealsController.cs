using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperDuperPlannerWanner.Data;
using SuperDuperPlannerWanner.Models;
using SuperDuperPlannerWanner.Models.ViewModels;

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
                return RedirectToAction(nameof(ViewMeals));
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

        // GET: Meals1/Details/5
        public async Task<IActionResult> MealDetails(int? id)
        {
            // Check for Meal
            if (id == null)
            {
                return NotFound();
            }

            // Get Meal
            MealDetailsViewModel vm = new MealDetailsViewModel();

            Meal meal = await _context.Meal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }

            vm.Meal = meal;

            /*
             * Currently getting full ingredients list and list that have been selected (bound)
             * Ideally need to get both these at the same time i.e. IsLinked = true if join is there or false if not but
             * can't figure that bit out easily - worth lookiung into though
             * Going to do Selected Items list and Unselected Items list but need to strip out the crossover duplicates first
             * Also need to think about how to handle quantities of items in stock and required for each meal - IMPORTANT
             * 
             */

            // Get Ingredients
            List<Ingredient> listIngredients;

            listIngredients = await _context.Ingredient.ToListAsync();

            IEnumerable<MealIngredientBind> MIBList = from mealIngredientLink in _context.Set<MealIngredientLink>()
                        join ingredient in _context.Set<Ingredient>()
                            on mealIngredientLink.IngredientId equals ingredient.Id
                        select new MealIngredientBind {
                            Ingredient = new Ingredient
                            {
                                Id = ingredient.Id,
                                Name = ingredient.Name,
                                Quantity = ingredient.Quantity,
                                Price = ingredient.Price,
                                IngredientTypeId = ingredient.IngredientTypeId
                            },
                            IsLinked = true
                        };

            vm.Ingredients = MIBList.ToList();

            return View(meal);
        }

        [HttpPost, ActionName("ChooseIngredients")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChooseIngredients(MealDetailsViewModel vm)
        {
            if (_iMealId == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Get Links + clear, then create new ones needed
                    // TODO: do in one proc
                    _context.MealIngredientLink.RemoveRange(_context.MealIngredientLink.Where<MealIngredientLink>(mil => mil.MealId == _iMealId));

                    List<MealIngredientBind> listSelectedIngredients = vm.Ingredients.Where(i => i.IsLinked == true).ToList();

                    foreach (MealIngredientBind item in vm.Ingredients)
                    {
                        MealIngredientLink mealIngredientLink = new MealIngredientLink
                        {
                            MealId = (int)_iMealId,
                            IngredientId = item.Ingredient.Id
                        };

                        _context.Add(mealIngredientLink);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealExists((int)_iMealId))
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

            return RedirectToAction(nameof(MealDetails), _iMealId);
        }
    }
}