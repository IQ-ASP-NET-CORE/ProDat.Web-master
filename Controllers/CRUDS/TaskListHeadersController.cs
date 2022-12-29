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
    public class TaskListHeadersController : Controller
    {
        private readonly TagContext _context;

        public TaskListHeadersController(TagContext context)
        {
            _context = context;
        }

        // GET: TaskListHeaders
        public async Task<IActionResult> Index()
        {
            var tagContext = _context.TaskListHeader.Include(t => t.MaintPackage).Include(t => t.MaintStrategy).Include(t => t.MaintWorkCentre).Include(t => t.MaintenancePlant).Include(t => t.PerformanceStandard).Include(t => t.Pmassembly).Include(t => t.RegulatoryBody).Include(t => t.SysCond).Include(t => t.TaskListGroup).Include(t => t.TasklistCat);
            return View(await tagContext.ToListAsync());
        }

        // GET: TaskListHeaders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskListHeader = await _context.TaskListHeader
                .Include(t => t.MaintPackage)
                .Include(t => t.MaintStrategy)
                .Include(t => t.MaintWorkCentre)
                .Include(t => t.MaintenancePlant)
                .Include(t => t.PerformanceStandard)
                .Include(t => t.Pmassembly)
                .Include(t => t.RegulatoryBody)
                .Include(t => t.SysCond)
                .Include(t => t.TaskListGroup)
                .Include(t => t.TasklistCat)
                .FirstOrDefaultAsync(m => m.TaskListHeaderId == id);
            if (taskListHeader == null)
            {
                return NotFound();
            }

            return View(taskListHeader);
        }

        // GET: TaskListHeaders/Create
        public IActionResult Create(int? CloneId)
        {
            ViewData["MaintPackageId"] = new SelectList(_context.MaintPackage, "MaintPackageId", "MaintPackageName");
            ViewData["MaintStrategyId"] = new SelectList(_context.MaintStrategy, "MaintStrategyId", "MaintStrategyName");
            ViewData["MaintWorkCentreId"] = new SelectList(_context.MaintWorkCentre, "MaintWorkCentreId", "MaintWorkCentreName");
            ViewData["MaintenancePlantId"] = new SelectList(_context.MaintenancePlant, "MaintenancePlantId", "MaintenancePlantNum");
            ViewData["PerformanceStandardId"] = new SelectList(_context.PerformanceStandard, "PerformanceStandardId", "PerformanceStandardDesc");
            ViewData["PmassemblyId"] = new SelectList(_context.Pmassembly, "PmassemblyId", "PmassemblyName");
            ViewData["RegulatoryBodyId"] = new SelectList(_context.RegulatoryBody, "RegulatoryBodyId", "RegulatoryBodyName");
            ViewData["SysCondId"] = new SelectList(_context.SysCond, "SysCondId", "SyScondName");
            ViewData["TaskListGroupId"] = new SelectList(_context.TaskListGroup, "TaskListGroupId", "TaskListGroupName");
            ViewData["TasklistCatId"] = new SelectList(_context.TaskListCat, "TaskListCatId", "TaskListCatName");

            if (CloneId != null)
            {
                // get MaintPlan into object, and return view.
                var obj = _context.TaskListHeader
                         .AsNoTracking() // ensures it isnt modifying existing entity. Will be saved as a new entity/record.
                         .Where(x => x.TaskListHeaderId == CloneId).FirstOrDefault();

                var maxCounter = _context.TaskListHeader
                                 .Where(x => x.TaskListGroupId == obj.TaskListGroupId)
                                 .Max(x => x.Counter);


                obj.Counter = maxCounter + 1;
                obj.TaskListShortText = obj.TaskListShortText + "_CLONED";

                return View(obj);
            }
            return View();
        }

        // POST: TaskListHeaders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskListHeaderId,TaskListGroupId,Counter,TaskListShortText,MaintWorkCentreId,MaintenancePlantId,SysCondId,MaintStrategyId,MaintPackageId,PmassemblyId,TasklistCatId,PerformanceStandardId,PerfStdAppDel,ScePsReviewId,RegulatoryBodyId,RegBodyAppDel,ChangeRequired,TaskListClassId")] TaskListHeader taskListHeader)
        {
            if (ModelState.IsValid)
            {
                //taskListHeader.TaskListHeaderId = ;
                _context.Add(taskListHeader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaintPackageId"] = new SelectList(_context.MaintPackage, "MaintPackageId", "MaintPackageName", taskListHeader.MaintPackageId);
            ViewData["MaintStrategyId"] = new SelectList(_context.MaintStrategy, "MaintStrategyId", "MaintStrategyName", taskListHeader.MaintStrategyId);
            ViewData["MaintWorkCentreId"] = new SelectList(_context.MaintWorkCentre, "MaintWorkCentreId", "MaintWorkCentreName", taskListHeader.MaintWorkCentreId);
            ViewData["MaintenancePlantId"] = new SelectList(_context.MaintenancePlant, "MaintenancePlantId", "MaintenancePlantNum", taskListHeader.MaintenancePlantId);
            ViewData["PerformanceStandardId"] = new SelectList(_context.PerformanceStandard, "PerformanceStandardId", "PerformanceStandardDesc", taskListHeader.PerformanceStandardId);
            ViewData["PmassemblyId"] = new SelectList(_context.Pmassembly, "PmassemblyId", "PmassemblyName", taskListHeader.PmassemblyId);
            ViewData["RegulatoryBodyId"] = new SelectList(_context.RegulatoryBody, "RegulatoryBodyId", "RegulatoryBodyName", taskListHeader.RegulatoryBodyId);
            ViewData["SysCondId"] = new SelectList(_context.SysCond, "SysCondId", "SyScondName", taskListHeader.SysCondId);
            ViewData["TaskListGroupId"] = new SelectList(_context.TaskListGroup, "TaskListGroupId", "TaskListGroupName", taskListHeader.TaskListGroupId);
            ViewData["TasklistCatId"] = new SelectList(_context.TaskListCat, "TaskListCatId", "TaskListCatName", taskListHeader.TasklistCatId);
            return View(taskListHeader);
        }

        // GET: TaskListHeaders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskListHeader = await _context.TaskListHeader.FindAsync(id);
            if (taskListHeader == null)
            {
                return NotFound();
            }
            ViewData["MaintPackageId"] = new SelectList(_context.MaintPackage, "MaintPackageId", "MaintPackageName", taskListHeader.MaintPackageId);
            ViewData["MaintStrategyId"] = new SelectList(_context.MaintStrategy, "MaintStrategyId", "MaintStrategyName", taskListHeader.MaintStrategyId);
            ViewData["MaintWorkCentreId"] = new SelectList(_context.MaintWorkCentre, "MaintWorkCentreId", "MaintWorkCentreName", taskListHeader.MaintWorkCentreId);
            ViewData["MaintenancePlantId"] = new SelectList(_context.MaintenancePlant, "MaintenancePlantId", "MaintenancePlantNum", taskListHeader.MaintenancePlantId);
            ViewData["PerformanceStandardId"] = new SelectList(_context.PerformanceStandard, "PerformanceStandardId", "PerformanceStandardDesc", taskListHeader.PerformanceStandardId);
            ViewData["PmassemblyId"] = new SelectList(_context.Pmassembly, "PmassemblyId", "PmassemblyName", taskListHeader.PmassemblyId);
            ViewData["RegulatoryBodyId"] = new SelectList(_context.RegulatoryBody, "RegulatoryBodyId", "RegulatoryBodyName", taskListHeader.RegulatoryBodyId);
            ViewData["SysCondId"] = new SelectList(_context.SysCond, "SysCondId", "SyScondName", taskListHeader.SysCondId);
            ViewData["TaskListGroupId"] = new SelectList(_context.TaskListGroup, "TaskListGroupId", "TaskListGroupName", taskListHeader.TaskListGroupId);
            ViewData["TasklistCatId"] = new SelectList(_context.TaskListCat, "TaskListCatId", "TaskListCatName", taskListHeader.TasklistCatId);
            return View(taskListHeader);
        }

        // POST: TaskListHeaders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskListHeaderId,TaskListGroupId,Counter,TaskListShortText,MaintWorkCentreId,MaintenancePlantId,SysCondId,MaintStrategyId,MaintPackageId,PmassemblyId,TasklistCatId,PerformanceStandardId,PerfStdAppDel,ScePsReviewId,RegulatoryBodyId,RegBodyAppDel,ChangeRequired,TaskListClassId")] TaskListHeader taskListHeader)
        {
            if (id != taskListHeader.TaskListHeaderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskListHeader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskListHeaderExists(taskListHeader.TaskListHeaderId))
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
            ViewData["MaintPackageId"] = new SelectList(_context.MaintPackage, "MaintPackageId", "MaintPackageName", taskListHeader.MaintPackageId);
            ViewData["MaintStrategyId"] = new SelectList(_context.MaintStrategy, "MaintStrategyId", "MaintStrategyName", taskListHeader.MaintStrategyId);
            ViewData["MaintWorkCentreId"] = new SelectList(_context.MaintWorkCentre, "MaintWorkCentreId", "MaintWorkCentreName", taskListHeader.MaintWorkCentreId);
            ViewData["MaintenancePlantId"] = new SelectList(_context.MaintenancePlant, "MaintenancePlantId", "MaintenancePlantNum", taskListHeader.MaintenancePlantId);
            ViewData["PerformanceStandardId"] = new SelectList(_context.PerformanceStandard, "PerformanceStandardId", "PerformanceStandardDesc", taskListHeader.PerformanceStandardId);
            ViewData["PmassemblyId"] = new SelectList(_context.Pmassembly, "PmassemblyId", "PmassemblyName", taskListHeader.PmassemblyId);
            ViewData["RegulatoryBodyId"] = new SelectList(_context.RegulatoryBody, "RegulatoryBodyId", "RegulatoryBodyName", taskListHeader.RegulatoryBodyId);
            ViewData["SysCondId"] = new SelectList(_context.SysCond, "SysCondId", "SyScondName", taskListHeader.SysCondId);
            ViewData["TaskListGroupId"] = new SelectList(_context.TaskListGroup, "TaskListGroupId", "TaskListGroupName", taskListHeader.TaskListGroupId);
            ViewData["TasklistCatId"] = new SelectList(_context.TaskListCat, "TaskListCatId", "TaskListCatName", taskListHeader.TasklistCatId);
            return View(taskListHeader);
        }

        // GET: TaskListHeaders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskListHeader = await _context.TaskListHeader
                .Include(t => t.MaintPackage)
                .Include(t => t.MaintStrategy)
                .Include(t => t.MaintWorkCentre)
                .Include(t => t.MaintenancePlant)
                .Include(t => t.PerformanceStandard)
                .Include(t => t.Pmassembly)
                .Include(t => t.RegulatoryBody)
                .Include(t => t.SysCond)
                .Include(t => t.TaskListGroup)
                .Include(t => t.TasklistCat)
                .FirstOrDefaultAsync(m => m.TaskListHeaderId == id);
            if (taskListHeader == null)
            {
                return NotFound();
            }

            return View(taskListHeader);
        }

        // POST: TaskListHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskListHeader = await _context.TaskListHeader.FindAsync(id);
            _context.TaskListHeader.Remove(taskListHeader);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskListHeaderExists(int id)
        {
            return _context.TaskListHeader.Any(e => e.TaskListHeaderId == id);
        }
    }
}
