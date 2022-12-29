using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProDat.Web2.Data;
using ProDat.Web2.Models;

namespace ProDat.Web2.Controllers.CRUDS
{
    public class MaintPlansController : Controller
    {
        private readonly TagContext _context;

        public MaintPlansController(TagContext context)
        {
            _context = context;
        }

        // GET: MaintPlans
        public async Task<IActionResult> Index()
        {
            var tagContext = _context.MaintPlan.Include(m => m.MaintSortProcess).Include(m => m.MaintStrategy).Include(m => m.MeasPoint).OrderBy(m=> m.MaintPlanName);
            return View(await tagContext.ToListAsync());
        }

        // GET: MaintPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintPlan = await _context.MaintPlan
                .Include(m => m.MaintSortProcess)
                .Include(m => m.MaintStrategy)
                .Include(m => m.MeasPoint)
                .FirstOrDefaultAsync(m => m.MaintPlanId == id);
            if (maintPlan == null)
            {
                return NotFound();
            }

            return View(maintPlan);
        }

        // GET: MaintPlans/Create
        public async Task<IActionResult> Create(int? id, int? CloneId)
        {
            ViewData["MaintSortProcessId"] = new SelectList(_context.MaintSortProcess, "MaintSortProcessId", "MaintSortProcessDesc");
            ViewData["MaintStrategyId"] = new SelectList(_context.MaintStrategy, "MaintStrategyId", "MaintStrategyName");
            ViewData["MeasPointId"] = new SelectList(_context.MeasPoint, "MeasPointId", "MeasPointData");

            if (id != null)
            {
                var maintPlan = await _context.MaintPlan.FindAsync(id);

                return View(maintPlan);
            }
            else if (CloneId != null)
            {
                // get MaintPlan into object, and return view.
                var mp = _context.MaintPlan
                         .AsNoTracking() // ensures it isnt modifying existing entity. Will be saved as a new entity/record.
                         .Where(x => x.MaintPlanId == CloneId).FirstOrDefault();

                // Get count of MaintPlan which start with this item name.
                var mp_count = _context.MaintPlan
                               .AsNoTracking() // ensures it isnt modifying existing entity. Will be saved as a new entity/record.
                               .Where(x => x.MaintPlanName.StartsWith(mp.MaintPlanName)).Count();

                mp.MaintPlanName = mp.MaintPlanName + "_CLONE " + mp_count.ToString("00");

                return View(mp);
            }
            else
            {
                return View();
            }
            
            
        }

        // POST: MaintPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaintPlanId,MaintPlanName,ShortText,MaintStrategyId,MaintSortProcessId,Sort,CycleModFactor,StartDate,MeasPointId,ChangeStatus,StartingInstructions,CallHorizon,SchedulingPeriodValue,SchedulingPeriodUom")] MaintPlan maintPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maintPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaintSortProcessId"] = new SelectList(_context.MaintSortProcess, "MaintSortProcessId", "MaintSortProcessDesc", maintPlan.MaintSortProcessId);
            ViewData["MaintStrategyId"] = new SelectList(_context.MaintStrategy, "MaintStrategyId", "MaintStrategyName", maintPlan.MaintStrategyId);
            ViewData["MeasPointId"] = new SelectList(_context.MeasPoint, "MeasPointId", "MeasPointData", maintPlan.MeasPointId);
            return View(maintPlan);
        }

        // GET: MaintPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintPlan = await _context.MaintPlan.FindAsync(id);
            if (maintPlan == null)
            {
                return NotFound();
            }
            ViewData["MaintSortProcessId"] = new SelectList(_context.MaintSortProcess, "MaintSortProcessId", "MaintSortProcessDesc", maintPlan.MaintSortProcessId);
            ViewData["MaintStrategyId"] = new SelectList(_context.MaintStrategy, "MaintStrategyId", "MaintStrategyName", maintPlan.MaintStrategyId);
            ViewData["MeasPointId"] = new SelectList(_context.MeasPoint, "MeasPointId", "MeasPointData", maintPlan.MeasPointId);
            return View(maintPlan);
        }

        // POST: MaintPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaintPlanId,MaintPlanName,ShortText,MaintStrategyId,MaintSortProcessId,Sort,CycleModFactor,StartDate,MeasPointId,ChangeStatus,StartingInstructions,CallHorizon,SchedulingPeriodValue,SchedulingPeriodUom")] MaintPlan maintPlan)
        {
            if (id != maintPlan.MaintPlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maintPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintPlanExists(maintPlan.MaintPlanId))
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
            ViewData["MaintSortProcessId"] = new SelectList(_context.MaintSortProcess, "MaintSortProcessId", "MaintSortProcessDesc", maintPlan.MaintSortProcessId);
            ViewData["MaintStrategyId"] = new SelectList(_context.MaintStrategy, "MaintStrategyId", "MaintStrategyName", maintPlan.MaintStrategyId);
            ViewData["MeasPointId"] = new SelectList(_context.MeasPoint, "MeasPointId", "MeasPointData", maintPlan.MeasPointId);
            return View(maintPlan);
        }

        // GET: MaintPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintPlan = await _context.MaintPlan
                .Include(m => m.MaintSortProcess)
                .Include(m => m.MaintStrategy)
                .Include(m => m.MeasPoint)
                .FirstOrDefaultAsync(m => m.MaintPlanId == id);
            if (maintPlan == null)
            {
                return NotFound();
            }

            return View(maintPlan);
        }

        // POST: MaintPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maintPlan = await _context.MaintPlan.FindAsync(id);
            _context.MaintPlan.Remove(maintPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaintPlanExists(int id)
        {
            return _context.MaintPlan.Any(e => e.MaintPlanId == id);
        }

        // Parameter is injected from view. Must match the models Attribute Name you're testing...
        public IActionResult NameAvailable(string MaintPlanName)
        {
            var tagExists = _context.MaintPlan
                                .Where(y => y.MaintPlanName == MaintPlanName).FirstOrDefault();

            if (tagExists == null)
                return Json(true);
            else
                return Json(false);
        }
    }
}
