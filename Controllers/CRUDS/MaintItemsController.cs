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
    public class MaintItemsController : Controller
    {
        private readonly TagContext _context;

        public MaintItemsController(TagContext context)
        {
            _context = context;
        }

        // GET: MaintItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.MaintItem.ToListAsync());
        }

        // GET: MaintItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintItem = await _context.MaintItem
                .FirstOrDefaultAsync(m => m.MaintItemId == id);
            if (maintItem == null)
            {
                return NotFound();
            }

            return View(maintItem);
        }

        // GET: MaintItems/Create
        public async Task<IActionResult> Create(int? id, int? CloneId)
        {
            if (id != null)
            {
                var maintItem = await _context.MaintItem.FindAsync(id);
                return View(maintItem);
            }
            else if (CloneId != null)
            {
                // get MaintPlan into object, and return view.
                var obj = _context.MaintItem
                         .AsNoTracking() // ensures it isnt modifying existing entity. Will be saved as a new entity/record.
                         .Where(x => x.MaintItemId == CloneId).FirstOrDefault();

                // Get count of MaintPlan which start with this item name.
                var count = _context.MaintItem
                               .AsNoTracking() // ensures it isnt modifying existing entity. Will be saved as a new entity/record.
                               .Where(x => x.MaintItemNum.StartsWith(obj.MaintItemNum)).Count();

                obj.MaintItemNum = obj.MaintItemNum + "_CLONE " + count.ToString("00");

                return View(obj);
            }
            else 
            { 
                return View();
            }
        }

        // POST: MaintItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaintItemId,MaintPlanId,MaintItemNum,MaintItemShortText,FMaintItemHeaderFloc,MaintItemHeaderEquipId,MaintItemObjectListFloc,MaintItemObjectListEquip,MaintItemMainWorkCentre,MaintItemMainWorkCentrePlant,MaintItemOrderType,MaintPlannerGroupId,MaintItemActivityTypeId,MaintItemRevNo,MaintItemUserStatus,MaintItemSystemCondition,MaintItemConsequenceCategory,MaintItemConsequence,MaintItemLikelihood,MaintItemProposedPriority,MaintItemProposedTi,MaintItemLongText,MaintItemTasklistExecutionFactor,MaintItemDoNotRelImmed")] MaintItem maintItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maintItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(maintItem);
        }

        // GET: MaintItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintItem = await _context.MaintItem.FindAsync(id);
            if (maintItem == null)
            {
                return NotFound();
            }
            return View(maintItem);
        }

        // POST: MaintItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaintItemId,MaintPlanId,MaintItemNum,MaintItemShortText,FMaintItemHeaderFloc,MaintItemHeaderEquipId,MaintItemObjectListFloc,MaintItemObjectListEquip,MaintItemMainWorkCentre,MaintItemMainWorkCentrePlant,MaintItemOrderType,MaintPlannerGroupId,MaintItemActivityTypeId,MaintItemRevNo,MaintItemUserStatus,MaintItemSystemCondition,MaintItemConsequenceCategory,MaintItemConsequence,MaintItemLikelihood,MaintItemProposedPriority,MaintItemProposedTi,MaintItemLongText,MaintItemTasklistExecutionFactor,MaintItemDoNotRelImmed")] MaintItem maintItem)
        {
            if (id != maintItem.MaintItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maintItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintItemExists(maintItem.MaintItemId))
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
            return View(maintItem);
        }

        // GET: MaintItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintItem = await _context.MaintItem
                .FirstOrDefaultAsync(m => m.MaintItemId == id);
            if (maintItem == null)
            {
                return NotFound();
            }

            return View(maintItem);
        }

        // POST: MaintItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maintItem = await _context.MaintItem.FindAsync(id);
            _context.MaintItem.Remove(maintItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Parameter is injected from view. Must match the models Attribute Name you're testing...
        public IActionResult NameAvailable(string MaintItemNum)
        {
            var tagExists = _context.MaintItem
                                .Where(y => y.MaintItemNum == MaintItemNum).FirstOrDefault();

            if (tagExists == null)
                return Json(true);
            else
                return Json(false);
        }

        private bool MaintItemExists(int id)
        {
            return _context.MaintItem.Any(e => e.MaintItemId == id);
        }
    }
}
