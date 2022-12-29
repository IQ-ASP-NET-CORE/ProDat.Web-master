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
    public class SubSystemsControllerOld : Controller
    {
        private readonly TagContext _context;

        public SubSystemsControllerOld(TagContext context)
        {
            _context = context;
        }

        // GET: SubSystems
        public async Task<IActionResult> Index()
        {
            var tagContext = _context.SubSystem.Include(s => s.Systems);
            return View(await tagContext.ToListAsync());
        }

        // GET: SubSystems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subSystem = await _context.SubSystem
                .Include(s => s.Systems)
                .FirstOrDefaultAsync(m => m.SubSystemId == id);
            if (subSystem == null)
            {
                return NotFound();
            }

            return View(subSystem);
        }

        // GET: SubSystems/Create
        public IActionResult Create()
        {
            ViewData["SystemsId"] = new SelectList(_context.System, "SystemsId", "SystemNum");
            return View();
        }

        // POST: SubSystems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubSystemId,SubSystemNum,SubSystemName,SystemsId")] SubSystem subSystem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subSystem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SystemsId"] = new SelectList(_context.System, "SystemsId", "SystemNum", subSystem.SystemsId);
            return View(subSystem);
        }

        // GET: SubSystems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subSystem = await _context.SubSystem.FindAsync(id);
            if (subSystem == null)
            {
                return NotFound();
            }
            ViewData["SystemsId"] = new SelectList(_context.System, "SystemsId", "SystemNum", subSystem.SystemsId);
            return View(subSystem);
        }

        // POST: SubSystems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubSystemId,SubSystemNum,SubSystemName,SystemsId")] SubSystem subSystem)
        {
            if (id != subSystem.SubSystemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subSystem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubSystemExists(subSystem.SubSystemId))
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
            ViewData["SystemsId"] = new SelectList(_context.System, "SystemsId", "SystemNum", subSystem.SystemsId);
            return View(subSystem);
        }

        // GET: SubSystems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subSystem = await _context.SubSystem
                .Include(s => s.Systems)
                .FirstOrDefaultAsync(m => m.SubSystemId == id);
            if (subSystem == null)
            {
                return NotFound();
            }

            return View(subSystem);
        }

        // POST: SubSystems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subSystem = await _context.SubSystem.FindAsync(id);
            _context.SubSystem.Remove(subSystem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubSystemExists(int id)
        {
            return _context.SubSystem.Any(e => e.SubSystemId == id);
        }
    }
}
